using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheathWeapon : MonoBehaviour
{
    public Transform[] sheathBoneArray;
    public Transform[] drawBoneArray;
    public GameObject[] weaponSlot;
    [HideInInspector] public bool sheathBool = false;

    //these variables change from weaponCollider class every time you draw the weapon but still need to set them here just for default when game loads
    private string weaponType;
    private GameObject sheathWeapon;
    private GameObject drawWeapon;    
    private Transform sheathBone;
    private Transform drawBone;    

    private Animator anim;
    private GameObject equippedWeapon;
    private GameObject[] weaponSlotSheathed;
    private int slotIndex = 0;
    private int lastSlotIndex = 0;

    private float weaponIdleTime;

    // Start is called before the first frame update
    void Start()
    {
        //weaponIdleTime = 0;

        anim = GetComponent<Animator>();
        weaponSlotSheathed = new GameObject[weaponSlot.Length]; //array with for saving sheathed weapons to use later
                
        //instantiate all weapons with the first one equiped
        for (int i = 0; i < weaponSlot.Length; i++)
        {
            if (weaponSlot[i] != null)
            {
                if (weaponType == null) //checks if it is null so it knows it is the 1st item on the slot list, in order to equip it
                {
                    equippedWeapon = Instantiate(weaponSlot[i].GetComponent<WeaponCollider>().drawWeapon, drawBoneArray[weaponSlot[i].GetComponent<WeaponCollider>().drawBone]);
                    
                    weaponType = weaponSlot[i].GetComponent<WeaponCollider>().weaponType;
                    drawWeapon = weaponSlot[i].GetComponent<WeaponCollider>().drawWeapon;
                    drawBone = drawBoneArray[weaponSlot[i].GetComponent<WeaponCollider>().drawBone];
                }                    
                else
                    weaponSlotSheathed[i] = Instantiate(weaponSlot[i].GetComponent<WeaponCollider>().sheathWeapon, sheathBoneArray[weaponSlot[i].GetComponent<WeaponCollider>().sheathBone]);
            }                
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* //when 5 sec idle and not attacking with spells or in combat
        if (Input.GetMouseButtonDown(0))
            weaponIdleTime = 0;

        weaponIdleTime += Time.deltaTime;
        */


        if (Input.GetKeyDown(KeyCode.H))
        {
            lastSlotIndex = slotIndex; //set them as equals to work correctly when you just unequip all weapons
            if (!sheathBool)
            {
                anim.SetTrigger("sheath" + weaponType);
                sheathBool = true;
            }
            else
            {
                anim.SetTrigger("draw" + weaponType);
                sheathBool = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && weaponSlot[0] != null)
        {            
            StartCoroutine(ExecuteSheathDraw(0));                
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && weaponSlot[1] != null)
        {
            StartCoroutine(ExecuteSheathDraw(1));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && weaponSlot[2] != null)
        {
            StartCoroutine(ExecuteSheathDraw(2));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && weaponSlot[3] != null)
        {
            StartCoroutine(ExecuteSheathDraw(3));
        }
    }

    IEnumerator ExecuteSheathDraw(int index)
    {
        if (!sheathBool && GetComponent<ComboAttacks>().canTransitionAttack && anim.GetBool("Grounded"))
        {
            lastSlotIndex = slotIndex; //save last slot index before changing for use in the functions below which are called from anim events
            slotIndex = index;

            if (lastSlotIndex != slotIndex)
            {                
                anim.SetTrigger("sheath" + weaponType);

                //I use this because for some reason without it, it plays the draw first and then the sheath in some cases
                yield return new WaitForSeconds(0.05f); 

                //set weapon type and draw variables
                weaponType = weaponSlot[slotIndex].GetComponent<WeaponCollider>().weaponType;
                drawWeapon = weaponSlot[slotIndex].GetComponent<WeaponCollider>().drawWeapon;
                drawBone = drawBoneArray[weaponSlot[slotIndex].GetComponent<WeaponCollider>().drawBone];

                anim.SetTrigger("draw" + weaponType);
            }
        }
    }


    public void SheathWeapons()
    {
        Destroy(equippedWeapon); //destroy weapon at hand
        //save weapon that i just unequipped to the array with the corresponding index for that weapon slot
        weaponSlotSheathed[lastSlotIndex] = Instantiate(sheathWeapon, sheathBone); 
    }


    public void DrawWeapons()
    {
        Destroy(weaponSlotSheathed[slotIndex]);//destroy sheathed weapon for the weapon i am about to equip
        equippedWeapon = Instantiate(drawWeapon, drawBone);//save current equipped weapon 
    }


    //sets sheath variables from weaponCollider class only for drawn weapons. Sheathed prefabs don't have this class as component
    public void SetWeaponAndBoneVariables(GameObject sheathWpn, int sheathBn)
    {
        sheathWeapon = sheathWpn;
        sheathBone =  sheathBoneArray[sheathBn];
    }    
}