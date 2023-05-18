using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[System.Serializable]
public class InventoryItem : ItemDrop
{
    [HideInInspector] public int inventoryIndex;
    //public string name;
    //public ItemType itemType;
    //public Sprite icon;
    //public GameObject itemDropPrefab;
    //public int quantity;
    //public int maxStack;
    //public List<Item> ingredients; // List of items required to craft this item

    //// Additional properties specific to certain item types
    //public int attackPower; // For weapons
    //public int defensePower; // For armor
    //public bool isConsumable; // For consumable items

    //// Enum to define the item type
    //public enum ItemType
    //{
    //    Resource,
    //    Consumable,
    //    Equipment,
    //    Craftable
    //}


    public void SetInventoryIndex()
    {
        inventoryIndex = transform.GetSiblingIndex();
    }


    public void DisplayIconInInventory()
    {        
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<Image>().sprite = icon;        
    }

    public void ShowDescription()
    {

    }
}
