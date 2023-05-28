using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;

public class HarvestableObject : ItemDrop
{

    [Header("Harvastable Object Settings")]
    [SerializeField] private float health;

    private bool isHarvested = false;

    private NetworkVariable<float> hp = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private GameObject[] playersArray2;
    private GameObject player2;

    // I used start instead of OnNetworkSpawn() because this harvestable is already in the scene and does not spawn 
    private void Start() //public override void OnNetworkSpawn() 
    {
        hp = new NetworkVariable<float>(health, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        
        
        //if (!IsHost) return;

        // Get all player gameobjects in the scene to loop through
        playersArray2 = GameObject.FindGameObjectsWithTag("Player");

        // Assign the correct player gameobject for each player by checking if they are owner of the player gameobject
        foreach (GameObject p in playersArray2)
        {
            if (p.GetComponent<NetworkObject>().IsLocalPlayer)
                player2 = p;
        }


        hp.OnValueChanged += (float previousValue, float newValue) =>
        {
            Debug.Log(OwnerClientId + " old " + previousValue);
            Debug.Log(OwnerClientId + " new " + newValue);

            player2.GetComponentInChildren<Canvas>().transform.GetChild(0).GetComponent<TMP_Text>().text = newValue.ToString();

            hp.Value = newValue;
        };

        //if (!player.GetComponent<NetworkPlayerOwnership>().GetHostStatus()) return;

        //player.GetComponent<NetworkPlayerOwnership>().SetNetworkGameObject(gameObject);
        //player.GetComponent<NetworkPlayerOwnership>().SpawnServerRPC();
        //Debug.Log(OwnerClientId + " Player2 " + player2);

        //var temp = player2.GetComponent<NetworkPlayerOwnership>();
        //Debug.Log(OwnerClientId + " Temp " + temp);
        //temp.SpawnNetworkGameObject(gameObject);
    }



    private void Harvest()
    {
        //if (!IsLocalPlayer) return;       

        if (!isHarvested)
        {
            isHarvested = true;

            // Instantiate the item drop at the gatherable object's position
            player2.GetComponent<NetworkPlayerOwnership>().SpawnNetworkGameObject(itemDropPrefab.GetComponent<ItemDrop>().prefabID, transform.position + transform.up);

            // Destroy the gatherable object
            player2.GetComponent<NetworkPlayerOwnership>().DestroyNetworkGameObject(gameObject);
        }
        
    }


    public void DamageHarvestable(float damage)
    {
        hp.Value -= damage;
        if (hp.Value <= 0) 
        {
            Harvest();
        }
    }
}