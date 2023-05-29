using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using Unity.Collections;
using UnityEngine.SceneManagement;

public class NetworkPlayerOwnership : NetworkBehaviour
{
    [SerializeField] private GameObject[] prefabsArray;
    public Dictionary<int, GameObject> prefabsDictionary = new Dictionary<int, GameObject>();

    //GameObject[] tempPlayers;
    //bool flag = false;


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


    //private void Update()
    //{
    //    if (SceneManager.GetActiveScene().name == "GameScene" && !flag)
    //    {
    //        tempPlayers = GameObject.FindGameObjectsWithTag("Player");

    //        if (tempPlayers.Length > 1)
    //        {
    //            flag = true;
    //            //SpawnAllNetworkObjects();
    //        }
                
    //    }
    //}



    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // NOTE: Temporary to disable HelpBtn
        GetComponentInParent<PlayerInput>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //SceneManager.sceneLoaded += OnSceneLoaded;


        foreach (var prefab in prefabsArray)
        {
            // Add the prefab to the dictionary using its prefabID as the key
            prefabsDictionary[prefab.GetComponent<ItemDrop>().prefabID] = prefab;
        }
    }


    //Spawn all network objects in the scene when the main scene is loaded
    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    // Check if the loaded scene is the actual game scene
    //    if (SceneManager.GetActiveScene().name == "GameScene")
    //    {
    //        // This code will only execute when the actual game scene is loaded
    //        //SpawnAllNetworkObjects();            

    //        // Unsubscribe from the scene loaded event after performing the initialization
    //        SceneManager.sceneLoaded -= OnSceneLoaded;
    //    }
    //}


    //private void SpawnAllNetworkObjects()
    //{
    //    if (!IsHost) return;

    //    // Get all the NetworkObjects in the scene
    //    NetworkObject[] networkObjects = FindObjectsOfType<NetworkObject>();

    //    // Register each NetworkObject as spawned
    //    foreach (NetworkObject networkObject in networkObjects)
    //    {
    //        if (!networkObject.IsSpawned)
    //            networkObject.Spawn(true);
    //    }
    //}


    public bool GetPlayerOwnershipStatus()
    {
        return IsOwner;
    }


    [ServerRpc(RequireOwnership = false)]
    public void SpawnServerRPC(int prefabID, Vector3 position, ServerRpcParams serverRpcParams = default)
    {
        //if (!GetNetworkObject(netObjID).IsOwner)
        //{
        //    GetNetworkObject(netObjID).ChangeOwnership(GetComponent<NetworkObject>().OwnerClientId);
        //}
        //Debug.Log("NetworkPlayer " + networkObjectID);
        //Debug.Log("NetworkPlayerObject " + GetNetworkObject(networkObjectID));
        //GetNetworkObject(networkObjectID).Spawn(true);
        //Debug.Log("NetID " + netObjID);
        //GetNetworkObject(netObjID).Spawn(true);



        //if (!currentNetObj.GetComponent<NetworkObject>().IsOwner)
        //{
        //    currentNetObj.GetComponent<NetworkObject>().ChangeOwnership(GetComponent<NetworkObject>().OwnerClientId);
        //}
        //Debug.Log("NetworkPlayer " + networkObjectID);
        //Debug.Log("NetworkPlayerObject " + GetNetworkObject(networkObjectID));
        //GetNetworkObject(networkObjectID).Spawn(true);


        if (prefabsDictionary.TryGetValue(prefabID, out var prefab))
        {
            // Instantiate the item drop at the gatherable object's position
            GameObject itemDrop = Instantiate(prefab, position, Quaternion.identity);

            itemDrop.GetComponent<NetworkObject>().Spawn(true);
        }        
    }




    //[ServerRpc(RequireOwnership = false)]
    //public void ItemDropSpawnServerRPC(int prefabID, Vector3 position, Quaternion rotation, ServerRpcParams serverRpcParams = default)
    //{
    //    if (prefabsDictionary.TryGetValue(prefabID, out var prefab))
    //    {
    //        // Instantiate the item drop at the gatherable object's position
    //        GameObject itemDrop = Instantiate(prefab, position, rotation);

    //        itemDrop.GetComponent<NetworkObject>().Spawn(true);

    //        ulong netObjID = itemDrop.GetComponent<NetworkObject>().NetworkObjectId;
    //        ulong senderID = serverRpcParams.Receive.SenderClientId;

    //        ResponseClientRpc(netObjID, senderID);
    //    }
    //}


    //[ClientRpc]
    //private void ResponseClientRpc(ulong netObjID, ulong targetClientId)
    //{
    //    //// Handle the response on the target client
    //    //if (NetworkManager.Singleton.LocalClientId == targetClientId)
    //    //{
    //    //    // The current client is the target client, so handle the response here
    //    //    if (NetworkManager.Singleton.ConnectedClients.TryGetValue(targetClientId, out NetworkClient targetClient))
    //    //    {
    //    //        // Access the player GameObject associated with the targetClientId
    //    //        targetClient.PlayerObject.GetComponent<Inventory>().netObjID = netObjID;

    //    //    }
    //    //}
        
    //    // The current client is the target client, so handle the response here
    //    if (NetworkManager.Singleton.ConnectedClients.TryGetValue(targetClientId, out NetworkClient targetClient))
    //    {
    //        // Access the player GameObject associated with the targetClientId
    //        targetClient.PlayerObject.GetComponent<Inventory>().netObjID = netObjID;
    //    }
    //}

    

    [ServerRpc(RequireOwnership = false)]
    public void DestroyServerRPC(ulong netObjID, ServerRpcParams serverRpcParams = default)
    {
        //Destroy(GetNetworkObject(networkObjectId).gameObject);
        //Destroy(currentNetworkGameObject);
        GetNetworkObject(netObjID).Despawn();
    }



    public void SpawnNetworkGameObject(int prefabID, Vector3 position)
    {
        //if (!netObj.GetComponent<NetworkObject>().IsOwner)
        //{
        //    netObj.GetComponent<NetworkObject>().ChangeOwnership(GetComponent<NetworkObject>().OwnerClientId);
        //}
        

        SpawnServerRPC(prefabID, position);//currentNetworkGameObject.GetComponent<NetworkObject>().NetworkObjectId);
    }


    public void DestroyNetworkGameObject(GameObject netObj)
    {
        DestroyServerRPC(netObj.GetComponent<NetworkObject>().NetworkObjectId);
    }
}