using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem : MonoBehaviour
{
    public string name;
    public ItemType itemType;
    public Sprite icon;
    public int quantity;
    public int maxStack;
    public List<Item> ingredients; // List of items required to craft this item

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
}
