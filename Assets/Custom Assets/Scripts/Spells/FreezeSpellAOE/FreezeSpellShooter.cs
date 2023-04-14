using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreezeSpellShooter : MonoBehaviour
{
    [Header("FREEZE SPELL")]
    public GameObject freezeSpell;
    public GameObject freezeWarmUp;
    public float freezeWarmUpDelay = 1;
    public Transform freezeFirePoint;
    public Transform freezeWarmUpPoint_Left;
    public Transform freezeWarmUpPoint_Right;
    public float spellCooldown;    
    public List<AudioClip> freezeSFX;

    public GameObject spellIcon;

    private float timeToFire = 0;
    private AudioSource audioSource;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        spellIcon.GetComponent<CooldownTimer>().SetCooldownAndTimeToFire(spellCooldown, timeToFire);
    }

    // Update is called once per frame
    public void ExecuteFreezeSpell()
    {
        if (Time.time >= timeToFire)
        {
            anim.SetTrigger("freezeSpell");
            timeToFire = Time.time + spellCooldown; //sets cooldown of spell

            if (freezeSpell != null)
            {
                StartCoroutine(InstantiateFreezeSpell(freezeFirePoint));
            }
        }

        spellIcon.GetComponent<CooldownTimer>().SetCooldownAndTimeToFire(spellCooldown, timeToFire);        
    }


    IEnumerator InstantiateFreezeSpell(Transform firepoint)
    {
        GetComponent<PlayerInput>().enabled = false;

        //create the spell warmup in hand and parent it to hand to move along with it and destroy
        //the moment the actual spell is created and fired
        if (freezeWarmUp != null)
        {
            var warmUpObjLeft = Instantiate(freezeWarmUp, freezeWarmUpPoint_Left.position, Quaternion.identity);
            warmUpObjLeft.transform.parent = freezeWarmUpPoint_Left.transform;
            Destroy(warmUpObjLeft, freezeWarmUpDelay);

            var warmUpObjRight = Instantiate(freezeWarmUp, freezeWarmUpPoint_Right.position, Quaternion.identity);
            warmUpObjRight.transform.parent = freezeWarmUpPoint_Right.transform;
            Destroy(warmUpObjRight, freezeWarmUpDelay);
        }

        if (audioSource != null && freezeSFX.Count > 0)
        {
            var index = Random.Range(0, freezeSFX.Count);
            audioSource.PlayOneShot(freezeSFX[index]);
        }

        yield return new WaitForSeconds(freezeWarmUpDelay);

        if (!GetComponent<PlayerStats>().dead)
        {
            var freezeObj = Instantiate(freezeSpell, firepoint.position, Quaternion.identity); //create spell in hand       
            
            yield return new WaitForSeconds(1); //wait 1 sec after firing spell to finish casting animation

            GetComponent<PlayerInput>().enabled = true; //re-enable player inputs after firing spell
            freezeObj.GetComponent<Collider>().enabled = false;
        }
    }
}