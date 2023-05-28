using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;


public class NetworkPlayerCamera : NetworkBehaviour
{
    [SerializeField] private NetworkPlayerOwnership player; // Prefab for the player object
    [SerializeField] private CinemachineVirtualCamera followCamera; // Prefab for the player object
    [SerializeField] private CinemachineVirtualCamera aimCamera; // Prefab for the player object

    [SerializeField] private GameObject playerCanvas; // Prefab for the player object

    //static int playerIndex = 0;


    public override void OnNetworkSpawn()
    {
        SetMainCameraForNetworkPlayer();

        DisableOtherPlayerCanvas();
    }


    public void SetMainCameraForNetworkPlayer()
    {
        // Assign the camera component of the player object as the main camera for this player
        if (IsLocalPlayer)
        {
            GetComponent<Camera>().tag = "MainCamera";

            // Set the priorities of the player's cinemachine cameras high so that it won't change when new player enters
            followCamera.Priority = 11;
            aimCamera.Priority = 12;
        }
        else
        {
            followCamera.Priority = 0;
            aimCamera.Priority = 1;
        }


        // The players who are NOT the owner set their follow camera cinemachine to true
        //because when a new cinemachine is enabled all the previous are disabled
        if (!IsOwner)
        {
            followCamera.enabled = false;
            followCamera.enabled = true;
        }
    }


    public void DisableOtherPlayerCanvas()
    {
        if (!IsOwner)
        {
            playerCanvas.SetActive(false);
        }
    }
}
