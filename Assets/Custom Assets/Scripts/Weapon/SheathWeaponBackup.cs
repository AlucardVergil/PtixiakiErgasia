using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheathWeaponBackup : MonoBehaviour
{
    public GameObject weapon;
    public GameObject sheathWeapon;
    public GameObject drawWeapon;
    public Transform sheathBone;
    public Transform drawBone;
    [HideInInspector] public bool sheathBool = false;

    private Animator anim;
    //private bool sheathBool = false;
    private GameObject weaponInstance;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        weaponInstance = weapon;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!sheathBool)
            {
                anim.SetTrigger("sheathSword");
                sheathBool = true;
            }
            else
            {
                anim.SetTrigger("drawSword");
                sheathBool = false;
            }
        }
    }


    public void SheathSword()
    {
        Destroy(weaponInstance);
        weaponInstance = Instantiate(sheathWeapon, sheathBone);
    }


    public void DrawSword()
    {
        Destroy(weaponInstance);
        weaponInstance = Instantiate(drawWeapon, drawBone);
    }


    public void SetWeaponAndBoneVariables(GameObject sheathWpn, GameObject drawWpn, Transform sheathBn, Transform drawBn)
    {
        sheathWeapon = sheathWpn;
        drawWeapon = drawWpn;
        sheathBone = sheathBn;
        drawBone = drawBn;
    }
}