using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;


public class Fracture_Pillar : NetworkBehaviour
{
    public GameObject fractured;

    private GameObject player;
    private Animator anim;
    private bool destroyWall = false;

    private void Start()
    {
        GameObject[] playersArray = GameObject.FindGameObjectsWithTag("Player");

        // Assign the correct player gameobject for each player by checking if they are owner of the player gameobject
        foreach (GameObject p in playersArray)
        {
            if (p.GetComponent<NetworkObject>().IsLocalPlayer)
                player = p;
        }

        anim = player.GetComponent<Animator>();        
    }


    public void SetDestroyWallBool(bool destroy)
    {
        destroyWall = destroy;
    }


    void FixedUpdate()
    {        
        if (destroyWall)            
            StartCoroutine(KickWall());                                        
    }


    IEnumerator KickWall()
    {
        destroyWall = false;

        anim.SetTrigger("kickWall");
        player.GetComponent<PlayerInput>().enabled = false;

        yield return new WaitForSeconds(0.55f);

        player.GetComponent<PlayerInput>().enabled = true;

        Vector3 unfracturedMeshPosition = transform.position;

        var fracturedPillar = Instantiate(fractured, unfracturedMeshPosition, Quaternion.identity);
        Destroy(gameObject);

        foreach (Rigidbody rigidbody in fracturedPillar.GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.AddForce(player.transform.forward); //NOTE doesn't do anything need to fix
        }

        Destroy(fracturedPillar, 5);
    }
}
