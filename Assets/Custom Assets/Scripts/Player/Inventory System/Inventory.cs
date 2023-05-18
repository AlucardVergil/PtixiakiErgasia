using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public ItemType itemType;
    public Sprite icon;
    public GameObject itemDropPrefab;
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

public class Inventory : MonoBehaviour
{
    public int baseCapacity = 20; // Initial capacity of the inventory
    public int maxCapacity = 50; // Maximum capacity of the inventory
    public List<Item> items = new List<Item>(); // List to store the items

    // Function to add an item to the inventory
    public bool AddItem(Item item)
    {
        // Check if the item can be stacked with an existing item
        foreach (Item existingItem in items)
        {
            if (existingItem.name == item.name && existingItem.quantity < existingItem.maxStack)
            {
                int remainingStackSpace = existingItem.maxStack - existingItem.quantity;
                int quantityToAdd = Mathf.Min(remainingStackSpace, item.quantity);
                existingItem.quantity += quantityToAdd;
                item.quantity -= quantityToAdd;

                if (item.quantity <= 0)
                    return false;
            }
        }

        // Check if the inventory has space for the item
        if (items.Count < GetTotalCapacity())
        {
            // Check if there is excess quantity that cannot be stacked
            int excessQuantity = Mathf.Max(0, item.quantity - item.maxStack);
            if (excessQuantity > 0)
            {
                item.quantity -= excessQuantity;

                // Create new item slots for the excess quantity
                while (excessQuantity > 0)
                {
                    Item newItem = new Item();
                    newItem.name = item.name;
                    newItem.icon = item.icon;
                    newItem.quantity = Mathf.Min(excessQuantity, item.maxStack);
                    newItem.maxStack = item.maxStack;
                    items.Add(newItem);
                    excessQuantity -= newItem.quantity;
                }
            }

            // Add the remaining quantity to a stack if there is space
            if (item.quantity > 0)
            {
                items.Add(item);
            }

            return true;
        }
        else
        {
            Debug.Log("Inventory is full!");
            return false;
        }
    }

    // Function to remove an item from the inventory
    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
    }

    // Function to upgrade the capacity of the inventory
    public void UpgradeCapacity()
    {
        if (GetTotalCapacity() < maxCapacity)
        {
            baseCapacity += 10; // Increase the base capacity by 10 or any desired amount
            Debug.Log("Inventory capacity upgraded!");
        }
        else
        {
            Debug.Log("Inventory capacity is already at the maximum!");
        }
    }

    // Function to get the total capacity of the inventory (base capacity + any upgrades)
    public int GetTotalCapacity()
    {
        return baseCapacity;
    }
}