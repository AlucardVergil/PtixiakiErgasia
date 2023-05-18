using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int baseCapacity = 20; // Initial capacity of the inventory
    public int maxCapacity = 50; // Maximum capacity of the inventory
    public List<InventoryItem> items = new List<InventoryItem>(); // List to store the items
    public GameObject inventorySlotPrefab;

    private GameObject inventoryUI;


    private void Awake()
    {
        inventoryUI = GameObject.FindGameObjectWithTag("Inventory");

        for (int i = 0; i < baseCapacity; i++)
        {
            GameObject inventorySlot = Instantiate(inventorySlotPrefab, inventoryUI.transform);
            inventorySlot.name = "Slot " + i;
            inventorySlot.transform.GetChild(0).gameObject.SetActive(false);
        }

        for (int j = 0; j < items.Count; j++)
        {
            InventoryItem inventoryItem = inventoryUI.transform.GetChild(items[j].inventoryIndex).gameObject.AddComponent<InventoryItem>();
            inventoryItem = items[j];

            inventoryItem.DisplayIconInInventory();
        }        
    }



    // Function to add an item to the inventory
    public bool AddItem(ItemDrop item)
    {
        // Check if the item can be stacked with an existing item
        foreach (InventoryItem existingItem in items)
        {
            if (existingItem.name == item.name && existingItem.quantity < existingItem.maxStack)
            {
                int remainingStackSpace = existingItem.maxStack - existingItem.quantity;
                //checks which is lower, the quantity that was picked up or the remaining space so that it won't exceed the MaxStack size
                int quantityToAdd = Mathf.Min(remainingStackSpace, item.quantity);
                existingItem.quantity += quantityToAdd;
                item.quantity -= quantityToAdd;

                if (item.quantity <= 0) //if picked up the whole loot (in case there is more of that item) end function
                    return true;
            }
        }

        // Check if the inventory has space for the item
        while (items.Count < GetTotalCapacity() && item.quantity > 0)
        {
            GameObject emptyInventorySlot = FindFirstEmptyInventorySlot();
            InventoryItem newItem = emptyInventorySlot.AddComponent<InventoryItem>();

            newItem.name = item.name;
            newItem.itemType = item.itemType;
            newItem.icon = item.icon;
            newItem.itemDropPrefab = item.itemDropPrefab;
            newItem.quantity = Mathf.Min(item.quantity, item.maxStack);
            newItem.maxStack = item.maxStack;
            newItem.ingredients = item.ingredients;
            newItem.attackPower = item.attackPower;
            newItem.defensePower = item.defensePower;
            newItem.isConsumable = item.isConsumable;

            newItem.SetInventoryIndex();
            newItem.DisplayIconInInventory();

            items.Add(newItem);

            item.quantity -= Mathf.Min(item.quantity, item.maxStack);
        }

        if (item.quantity <= 0)
        {
            return true;
        }
        else
        {
            Debug.Log("Inventory is full!");
            return false;
        }
    }


    // Function to remove an item from the inventory
    public void RemoveItem(InventoryItem item)
    {
        if (items.Contains(item))
        {
            GameObject itemDrop = Instantiate(item.itemDropPrefab, transform.position, Quaternion.identity);
            itemDrop.GetComponent<ItemDrop>().Initialize(item);

            items.Remove(item);
            Destroy(item); //remove only the component not the whole gameobject            
        }
    }


    // Function to upgrade the capacity of the inventory
    public void UpgradeCapacity(int addedCapacity)
    {
        if (GetTotalCapacity() < maxCapacity)
        {            
            for (int i = 0; i < addedCapacity; i++)
            {
                GameObject inventorySlot = Instantiate(inventorySlotPrefab, transform);
                inventorySlot.name = "Slot " + (baseCapacity + i);
                inventorySlot.transform.GetChild(0).gameObject.SetActive(false);
            }

            baseCapacity += addedCapacity; // Increase the base capacity by 10 or any desired amount
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


    private GameObject FindFirstEmptyInventorySlot()
    {
        for (int i = 0; i < inventoryUI.transform.childCount; i++)
        {
            Transform child = inventoryUI.transform.GetChild(i);

            // Check if the child does not have InventoryItem component
            if (!child.TryGetComponent<InventoryItem>(out _))   // "out _" is used to indicate that i am not interested in the actual output. It is just to check if component exists. 
                return child.gameObject;            
        }
        return null; //in case there is no empty slot, but that will never happen here because there is another if that checks that before this function executes
    }


    public void MoveItemInInventory()
    {

    }



}