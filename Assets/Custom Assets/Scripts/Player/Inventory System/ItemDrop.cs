using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemDrop : MonoBehaviour
{
    private Inventory inventory;

    public string name;
    public ItemType itemType;
    public Sprite icon;    
    public int quantity;
    public int maxStack;
    public List<Item> ingredients; // List of items required to craft this item
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

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }


    public void Initialize(InventoryItem item)
    {
        name = item.name;
        itemType = item.itemType;
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