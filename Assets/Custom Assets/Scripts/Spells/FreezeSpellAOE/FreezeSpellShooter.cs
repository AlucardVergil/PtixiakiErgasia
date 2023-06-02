using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;


public class FreezeSpellShooter : NetworkBehaviour
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


    private void Awake()
    {
        
    }


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
        if (!IsOwner) return; // For NetworkBehaviour

        if (Time.time >= timeToFire)
        {
            anim.SetTrigger("freezeSpell");
            timeToFire = Time.time + spellCooldown; //sets cooldown of spell

            if (freezeSpell != null)
            {
                SpawnFreezeSpellServerRPC();
                //StartCoroutine(InstantiateFreezeSpell(freezeFirePoint));
            }
        }

        spellIcon.GetComponent<CooldownTimer>().SetCooldownAndTimeToFire(spellCooldown, timeToFire);        
    }


    IEnumerator InstantiateFreezeSpell(ulong senderClientId)
    {
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(senderClientId, out NetworkClient targetClient))
        {
            DisablePlayerInputClientRpc(senderClientId, false);

            //create the spell warmup in hand and parent it to hand to move along with it and destroy
            //the moment the actual spell is created and fired
            if (freezeWarmUp != null)
            {
                SpellWarmUpClientRpc(targetClient.PlayerObject.NetworkObjectId);

                //var warmUpObjLeft = Instantiate(freezeWarmUp, targetClient.PlayerObject.GetComponent<FreezeSpellShooter>().freezeWarmUpPoint_Left.position, Quaternion.identity);
                //warmUpObjLeft.GetComponent<NetworkObject>().Spawn(true);
                ////warmUpObjLeft.transform.parent = targetClient.PlayerObject.GetComponent<FreezeSpellShooter>().freezeWarmUpPoint_Left.transform;
                //Destroy(warmUpObjLeft, freezeWarmUpDelay);

                //var warmUpObjRight = Instantiate(freezeWarmUp, targetClient.PlayerObject.GetComponent<FreezeSpellShooter>().freezeWarmUpPoint_Right.position, Quaternion.identity);
                //warmUpObjRight.GetComponent<NetworkObject>().Spawn(true);
                ////warmUpObjRight.transform.parent = targetClient.PlayerObject.GetComponent<FreezeSpellShooter>().freezeWarmUpPoint_Right.transform;                
                //Destroy(warmUpObjRight, freezeWarmUpDelay);
            }

            if (audioSource != null && freezeSFX.Count > 0)
            {
                var index = Random.Range(0, freezeSFX.Count);
                targetClient.PlayerObject.GetComponent<FreezeSpellShooter>().audioSource.PlayOneShot(freezeSFX[index]);
            }

            yield return new WaitForSeconds(freezeWarmUpDelay);

            if (!targetClient.PlayerObject.GetComponent<PlayerStats>().dead)
            {
                var freezeObj = Instantiate(freezeSpell, freezeFirePoint.position, Quaternion.identity);
                freezeObj.GetComponent<NetworkObject>().SpawnWithOwnership(senderClientId);

                yield return new WaitForSeconds(1); //wait 1 sec after firing spell to finish casting animation

                DisablePlayerInputClientRpc(senderClientId, true); //re-enable player inputs after firing spell
                freezeObj.GetComponent<Collider>().enabled = false;
            }
        }
    }


    [ServerRpc(RequireOwnership = false)]
    public void SpawnFreezeSpellServerRPC(ServerRpcParams serverRpcParams = default)
    {
        StartCoroutine(InstantiateFreezeSpell(serverRpcParams.Receive.SenderClientId));
    }


    // This a workaround that is used to spawn the spell warnUp to each client individually without netcode's Spawn() because i can't parent
    // the spell to the hand bones, so instead i Instantiate the spell to each client separately
    [ClientRpc]
    private void SpellWarmUpClientRpc(ulong targetNetPlayerObjId)
    {
        Transform senderFirepointLeft = GetNetworkObject(targetNetPlayerObjId).GetComponent<FreezeSpellShooter>().freezeWarmUpPoint_Left;
        Transform senderFirepointRight = GetNetworkObject(targetNetPlayerObjId).GetComponent<FreezeSpellShooter>().freezeWarmUpPoint_Right;

        var warmUpObjLeft = Instantiate(freezeWarmUp, senderFirepointLeft.position, Quaternion.identity);
        warmUpObjLeft.transform.parent = senderFirepointLeft;
        Destroy(warmUpObjLeft, freezeWarmUpDelay);

        var warmUpObjRight = Instantiate(freezeWarmUp, senderFirepointRight.position, Quaternion.identity);
        warmUpObjRight.transform.parent = senderFirepointRight;                
        Destroy(warmUpObjRight, freezeWarmUpDelay);
    }



    [ClientRpc]
    private void DisablePlayerInputClientRpc(ulong targetClientId, bool playerInputBool)
    {
        if (NetworkManager.Singleton.LocalClientId == targetClientId)
        {
            GetComponent<PlayerInput>().enabled = playerInputBool;
        }
    }
}