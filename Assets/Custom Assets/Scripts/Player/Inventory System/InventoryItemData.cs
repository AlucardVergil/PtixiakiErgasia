using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is to create an InventoryItem variable and add it to the items list and then later add that item to the inventoryUI as a component.
//If i didn't do it like this then the list wasn't working correctly
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
    public string itemDescription;

    // Enum to define the item type
    public enum ItemType
    {
        Resource,
        Consumable,
        Equipment,
        Craftable
    }

    // Additional properties specific to certain item types
    #region Equipment Variables
    [HideInInspector] public int attackPower = 0; // For weapons
    [HideInInspector] public int defensePower = 0; // For armor
    #endregion

    #region Consumable Variables
    [HideInInspector] public int effectsDropdownIndex = 0;
    [HideInInspector] public int effectsValue = 0;
    #endregion

    #region Resources Variables
    #endregion

    #region Craftable Variables
    #endregion


    public InventoryItemData() {}
}
