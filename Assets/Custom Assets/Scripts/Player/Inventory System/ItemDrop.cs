using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class ItemDrop : MonoBehaviour
{
    private GameObject player;
    private Inventory inventory;

    public InventoryItemData inventoryItemData;

    public string itemName;
    public ItemType itemType;
    public Sprite icon;    
    public int quantity;
    public int maxStack;
    public List<InventoryItem> ingredients; // List of items required to craft this item
    public GameObject itemDropPrefab;

    // Additional properties specific to certain item types
    public int attackPower; // For weapons
    public int defensePower; // For armor
    public bool isConsumable; // For consumable items

    // Enum to define the item type
    public enum ItemType
    {
        Resource,
        Consumable,
        Equipment,
        Craftable
    }

    public UnityEvent onConsumeItem;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        inventory = player.GetComponent<Inventory>();

        onConsumeItem = new UnityEvent();

        if (itemType == ItemType.Consumable)                 
            onConsumeItem.AddListener(() => player.GetComponent<PlayerStats>().RestoreHealth(20));
        
        
    }


    public void Initialize(InventoryItemData item)
    {
        inventoryItemData = item; //this is to save the InventoryItemData object (NON-Monobehaviour) so i can use it to find or remove entries from the list

        itemName = item.itemName;
        itemType = (ItemType)item.itemType;
        icon = item.icon;
        itemDropPrefab = item.itemDropPrefab;
        quantity = item.quantity;
        maxStack = item.maxStack;
        ingredients = item.ingredients;
        attackPower = item.attackPower;
        defensePower = item.defensePower;
        isConsumable = item.isConsumable;
    }


    public void PickUp()
    {
        // Handle the pickup logic here, such as adding the item to the player's inventory        
        if (inventory != null)
        {
            bool itemAdded = inventory.AddItem(this);
            if (itemAdded)
            {
                // Item added to inventory, destroy the item drop
                Destroy(gameObject);
            }
        }
    }

}