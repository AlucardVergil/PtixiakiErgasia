using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreezeAOE : MonoBehaviour
{
    public List<AudioClip> impactSFX;
    public int freezeDuration = 10;


    private void OnTriggerEnter(Collider collision)
    {        
        if (collision.CompareTag("Enemy")) //check if collided with enemy and take damage
        {
            StartCoroutine(FreezeEnemies(collision));         
        }
    }

    IEnumerator FreezeEnemies(Collider collision)
    {
        //set destination to current position in order to stop enemy from moving
        collision.GetComponent<NavMeshAgent>().SetDestination(collision.GetComponent<NavMeshAgent>().transform.position);
        collision.gameObject.GetComponent<Animator>().enabled = false; //disable animator of enemy to freeze them

        var num = Random.Range(0, impactSFX.Count);
        var audioSource = GetComponent<AudioSource>();

        if (audioSource != null && impactSFX.Count > 0)
            audioSource.PlayOneShot(impactSFX[num]);

        yield return new WaitForSeconds(freezeDuration);
     
        if (collision != null)
            collision.gameObject.GetComponent<Animator>().enabled = true; //re-enable animator after duration of freeze spell

        Destroy(gameObject);
    }
}