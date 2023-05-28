using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Unity.Netcode;


//Could add below 2 lines but i will need to also set the settings for these components
//[RequireComponent (typeof(Rigidbody))]
//[RequireComponent(typeof(BoxCollider))]
public class WeaponCollider : NetworkBehaviour
{
    GameObject playerObject;
    StarterAssetsInputs _input;

    public int weaponDamage;
    public string weaponType;
    public float weight;
    public GameObject sheathWeapon;
    public GameObject drawWeapon;
    [Header("0 = Left Waist, 1 = Right Waist, 2 = Back")]
    public int sheathBone;
    [Header("0 = Right Hand, 1 = Left Hand")]
    public int drawBone;
    


    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        _input = playerObject.GetComponent<StarterAssetsInputs>();
        
        playerObject.GetComponent<SheathWeapon>().SetWeaponAndBoneVariables(sheathWeapon, sheathBone);
        playerObject.GetComponent<ComboAttacks>().animationWeaponPrefix = weaponType;
        playerObject.GetComponent<ComboAttacks>().weaponWeight = weight;
    }



    //The collider of this weapon object is set to trigger from the inspector, to trigger this event function when another
    //collider, collides with this one 
    private void OnTriggerEnter(Collider other)
    {
        //checks if the other collider's tag is "enemy" (which is a tag i created and assigned to objects that i want to act as
        //enemies and be able to take damage by the player) and also checks if an attack was initiated by the player, so that the
        //player won't damage the enemy if the weapon touches the enemy when the player hasn't attacked
        if (other.CompareTag("Enemy") && _input.attackCanHit)
        {
            //call function to deal damage to the enemy
            //DamageValue is added inside float parameter in each attack's animation event. Not here!
            other.GetComponent<Enemy>().TakeDamage(_input.damageValue + weaponDamage);

            //make it false so it doesn't take damage more than once with only one swing if collider accidentally hits
            //the enemy multiple times with one swing
            //NOTE: This is also set to false with an event i set inside the SlashSword animation asset, which is set to trigger
            //at a specific frame (when player finishes his swing) and it calls a function called FinishAttack() from the 
            //StarterAssetsInputs class
            //_input.attackCanHit = false; 
        }
        else if (other.TryGetComponent<HarvestableObject>(out var harvestableObject) && _input.attackCanHit && weaponType == "Axe") //harvest item 
        {
            harvestableObject.DamageHarvestable(weaponDamage);

            // Highlight the harvestable resource or provide visual feedback

            // Perform any other actions or effects related to starting the interaction
        }

    }
}