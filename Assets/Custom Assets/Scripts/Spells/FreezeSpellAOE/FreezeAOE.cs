using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.Netcode;


public class FreezeAOE : NetworkBehaviour
{
    public List<AudioClip> impactSFX;
    public int freezeDuration = 10;


    private void OnTriggerEnter(Collider collision)
    {        
        if (collision.CompareTag("Enemy")) //check if collided with enemy and take damage
        {
            ulong netObjID = collision.GetComponent<NetworkObject>().NetworkObjectId;

            FreezeEnemiesServerRpc(netObjID);
        }
    }

    IEnumerator FreezeEnemies(Collider collision)
    {
        
        collision.gameObject.GetComponent<Animator>().enabled = false; //disable animator of enemy to freeze them

        var num = Random.Range(0, impactSFX.Count);
        var audioSource = GetComponent<AudioSource>();

        if (audioSource != null && impactSFX.Count > 0)
            audioSource.PlayOneShot(impactSFX[num]);

        yield return new WaitForSeconds(freezeDuration);
     
        if (collision != null)
            collision.gameObject.GetComponent<Animator>().enabled = true; //re-enable animator after duration of freeze spell

        if (IsOwner)
        {
            GetComponent<NetworkObject>().Despawn();
            Destroy(gameObject);
        }        
    }


    [ServerRpc(RequireOwnership = false)]
    private void FreezeEnemiesServerRpc(ulong networkObjID)
    {
        Collider collision = GetNetworkObject(networkObjID).GetComponent<Collider>();

        //set destination to current position in order to stop enemy from moving
        collision.GetComponent<NavMeshAgent>().SetDestination(collision.GetComponent<NavMeshAgent>().transform.position);

        FreezeEnemiesClientRpc(networkObjID);
    }


    [ClientRpc]
    private void FreezeEnemiesClientRpc(ulong networkObjID)
    {
        Collider collision = GetNetworkObject(networkObjID).GetComponent<Collider>();

        StartCoroutine(FreezeEnemies(collision));
    }
}