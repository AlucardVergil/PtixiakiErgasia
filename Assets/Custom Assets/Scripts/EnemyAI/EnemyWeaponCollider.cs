using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class EnemyWeaponCollider : NetworkBehaviour
{
    public int damageValue;


    //The collider of this weapon object is set to trigger from the inspector, to trigger this event function when another
    //collider, collides with this one 
    private void OnTriggerStay(Collider other)
    {
        //checks if the other collider's tag is "enemy" (which is a tag i created and assigned to objects that i want to act as
        //enemies and be able to take damage by the player) and also checks if an attack was initiated by the player, so that the
        //player won't damage the enemy if the weapon touches the enemy when the player hasn't attacked
        if (other.CompareTag("Player") && GetComponentInParent<Enemy>().isAttackingPlayer)
        {
            other.GetComponent<PlayerStats>().TakeDamage(damageValue);
            GetComponentInParent<Enemy>().isAttackingPlayer = false;
        }

    }

}
