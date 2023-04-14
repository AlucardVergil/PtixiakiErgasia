using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEProjectile : MonoBehaviour
{
    public GameObject impactVFX;
    public List<AudioClip> impactSFX;

    private bool collided;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Spell") && !collision.gameObject.CompareTag("Player") 
            && !collision.gameObject.CompareTag("PlayerWeapon") && !collided)
        {
            if (collision.gameObject.CompareTag("Enemy")) //check if collided with enemy and take damage
            {                
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(100);
            }

            collided = true;

            if (impactVFX != null)
            {
                ContactPoint contact = collision.GetContact(0); //get contact point of spell
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point;

                var hitVFX = Instantiate(impactVFX, pos, rot);
                var num = Random.Range(0, impactSFX.Count);
                var audioSource = hitVFX.GetComponent<AudioSource>();
                
                if (audioSource != null && impactSFX.Count > 0)                
                    audioSource.PlayOneShot(impactSFX[num]);
                
                Destroy(hitVFX, 2);
            }
            Destroy(gameObject);
        }
    }
}