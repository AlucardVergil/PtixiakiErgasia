using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemData
{
    [HideInInspector] public int inventoryIndex = -1;
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


    public InventoryItemData() {}
}
