using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class SpellsManager : MonoBehaviour
{
    [Header("SPELL SETTINGS")]
    public GameObject spell;
    public GameObject spellWarmUp;
    public float spellWarmUpDelay = 1;
    public Transform spellFirePoint;
    public Transform spellWarmUpPoint_Left;
    public Transform spellWarmUpPoint_Right;
    public float spellCooldown;
    public List<AudioClip> spellSFX;

    public GameObject spellIcon;

    protected float timeToFire = 0;
    protected AudioSource _playerAudioSource;
    protected Animator _playerAnim;
    protected StarterAssetsInputs _input;



    protected void Start()
    {
        _playerAudioSource = GetComponent<AudioSource>();
        _playerAnim = GetComponent<Animator>();
        _input = GetComponent<StarterAssetsInputs>();
    }



    protected virtual void InstantiateSpellWarmUp()
    {
        //create the spell warmup in hand and parent it to hand to move along with it and destroy
        //the moment the actual spell is created and fired
        if (spellWarmUp != null)
        {
            if (spellWarmUpPoint_Left != null)
            {
                var warmUpObjLeft = Instantiate(spellWarmUp, spellWarmUpPoint_Left.position, Quaternion.identity,
                spellWarmUpPoint_Left.transform);
                Destroy(warmUpObjLeft, spellWarmUpDelay);
            }

            if (spellWarmUpPoint_Right != null)
            {
                var warmUpObjRight = Instantiate(spellWarmUp, spellWarmUpPoint_Right.position, Quaternion.identity,
                spellWarmUpPoint_Right.transform);
                Destroy(warmUpObjRight, spellWarmUpDelay);
            }            
        }

        if (_playerAudioSource != null && spellSFX.Count > 0)
        {
            var index = Random.Range(0, spellSFX.Count);
            _playerAudioSource.PlayOneShot(spellSFX[index]);
        }
    }
}