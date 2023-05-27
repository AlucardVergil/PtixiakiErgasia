using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using Unity.Collections;
using UnityEngine.SceneManagement;

public class NetworkPlayerOwnership : NetworkBehaviour
{
    private GameObject currentNetworkGameObject;
    GameObject[] tempPlayers;
    bool flag = false;

    #region Just a template for NetworkVariable handling

    //private NetworkVariable<MyCustomData> netVar = new NetworkVariable<MyCustomData>(
    //    new MyCustomData
    //    {
    //        _int = 56,
    //        _bool = true,
    //    }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    //public struct MyCustomData : INetworkSerializable
    //{
    //    public int _int;
    //    public bool _bool;
    //    public FixedString128Bytes _fixedString128Bytes;

    //    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    //    {
    //        serializer.SerializeValue(ref _int);
    //        serializer.SerializeValue(ref _bool);
    //        serializer.SerializeValue(ref _fixedString128Bytes);
    //    }
    //}


    //public override void OnNetworkSpawn()
    //{
    //    netVar.OnValueChanged += (MyCustomData previousValue, MyCustomData newValue) =>
    //    {
    //        Debug.Log(OwnerClientId + "; " + newValue._int + "; " + newValue._bool + "; " + newValue._fixedString128Bytes);
    //    };
    //}


    //private void Update()
    //{
    //    if (!IsOwner) return;

    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        netVar.Value = new MyCustomData
    //        {
    //            _int = 10,
    //            _bool = false,
    //            _fixedString128Bytes = "String"
    //        };
    //    }
    //}
    #endregion

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene" && !flag)
        {
            Debug.Log("Client ID " + OwnerClientId);
            tempPlayers = GameObject.FindGameObjectsWithTag("Player");

            if (tempPlayers.Length > 1)
            {
                flag = true;
                //SpawnAllNetworkObjects();
            }
                
        }
    }



    public override void OnNetworkSpawn()
    {
        // NOTE: Temporary to disable HelpBtn
        GetComponentInParent<PlayerInput>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log(OwnerClientId + "NetworkPlayerOwn spawn IsHost " + IsHost);

        SceneManager.sceneLoaded += OnSceneLoaded;        
    }


    //Spawn all network objects in the scene when the main scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is the actual game scene
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            // This code will only execute when the actual game scene is loaded
            //SpawnAllNetworkObjects();            

            // Unsubscribe from the scene loaded event after performing the initialization
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }


    private void SpawnAllNetworkObjects()
    {
        if (!IsHost) return;

        // Get all the NetworkObjects in the scene
        NetworkObject[] networkObjects = FindObjectsOfType<NetworkObject>();

        // Register each NetworkObject as spawned
        foreach (NetworkObject networkObject in networkObjects)
        {
            if (!networkObject.IsSpawned)
                networkObject.Spawn(true);
        }
    }


    public bool GetPlayerOwnershipStatus()
    {
        return IsOwner;
    }

    public bool GetLocalPlayerStatus()
    {
        return IsLocalPlayer;
    }


    public bool GetHostStatus()
    {
        return IsHost;
    }


    [ServerRpc(RequireOwnership = false)]
    public void SpawnServerRPC()
    {
        //Debug.Log("NetworkPlayer " + networkObjectID);
        //Debug.Log("NetworkPlayerObject " + GetNetworkObject(networkObjectID));
        //GetNetworkObject(networkObjectID).Spawn(true);

        currentNetworkGameObject.GetComponent<NetworkObject>().Spawn(true);
    }


    [ServerRpc(RequireOwnership = false)]
    public void DestroyServerRPC()
    {
        //Destroy(GetNetworkObject(networkObjectId).gameObject);
        //Destroy(currentNetworkGameObject);
        currentNetworkGameObject.GetComponent<NetworkObject>().Despawn();
    }


    public void SetNetworkGameObject(GameObject netObj)
    {
        if (!IsHost) return;

        currentNetworkGameObject = netObj;
    }


    public void SpawnNetworkGameObject(GameObject netObj)
    {
        if (!IsHost) return;

        currentNetworkGameObject = netObj;

        SpawnServerRPC();
    }


    public void DestroyNetworkGameObject(GameObject netObj)
    {
        if (!IsHost) return;

        currentNetworkGameObject = netObj;

        DestroyServerRPC();
    }
}