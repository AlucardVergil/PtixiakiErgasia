using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;


public class NetworkPlayerOwnership : NetworkBehaviour
{   

    private void Start()
    {
        // NOTE: Temporary to disable HelpBtn
        GetComponentInParent<PlayerInput>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public bool GetPlayerOwnershipStatus()
    {
        return IsOwner;
    }

    public bool GetLocalPlayerStatus()
    {
        return IsLocalPlayer;
    }
}