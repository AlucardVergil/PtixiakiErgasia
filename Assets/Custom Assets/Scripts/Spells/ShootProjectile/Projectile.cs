using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class Projectile : NetworkBehaviour
{
    public int spellDamage;
    public GameObject impactVFX;
    public List<AudioClip> impactSFX;

    private bool collided;

    private PlayerStats playerStats;



    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Spell") && !collision.gameObject.CompareTag("Player") 
            && !collision.gameObject.CompareTag("PlayerWeapon") && !collided)
        {
            ulong netObjID = 0; // Initialize to 0 so that if the object hit is not a network object then leave the ID to 0 meaning that it hit no network object

            if (collision.gameObject.TryGetComponent<NetworkObject>(out var netObj))
                netObjID = netObj.NetworkObjectId;

            ContactPoint contact = collision.GetContact(0); //get contact point of spell
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;


            ProjectileCollisionServerRpc(netObjID, pos, rot);
        }
    }



    [ServerRpc(RequireOwnership = false)]
    private void ProjectileCollisionServerRpc(ulong networkObjID, Vector3 pos, Quaternion rot, ServerRpcParams serverRpcParams = default)
    {
        //if the projectile hit a network object
        if (networkObjID > 0)
        {
            NetworkObject collision = GetNetworkObject(networkObjID);

            if (collision.gameObject.CompareTag("Enemy")) //check if collided with enemy and take damage
            {
                //Get client player object
                if (NetworkManager.Singleton.ConnectedClients.TryGetValue(serverRpcParams.Receive.SenderClientId, out NetworkClient targetClient))
                    playerStats = targetClient.PlayerObject.GetComponent<PlayerStats>();


                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.TakeDamageServerRpc(spellDamage, playerStats.critDamage, playerStats.critChance);
            }
        }
        

        collided = true;

        if (impactVFX != null)
        {
            var hitVFX = Instantiate(impactVFX, pos, rot); //create visual effect at hit point
            hitVFX.GetComponent<NetworkObject>().Spawn();
            var num = Random.Range(0, impactSFX.Count);
            var audioSource = hitVFX.GetComponent<AudioSource>();

            if (audioSource != null && impactSFX.Count > 0)
                audioSource.PlayOneShot(impactSFX[num]);

            Destroy(hitVFX, 2);
        }
        Destroy(gameObject);
    }
}