using StarterAssets;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int baseCapacity = 20; // Initial capacity of the inventory
    public int maxCapacity = 50; // Maximum capacity of the inventory
    public List<InventoryItemData> items = new List<InventoryItemData>(); // List to store the items
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
            inventoryItem.Initialize(items[j]);

            inventoryItem.DisplayIconInInventory();
        }        
    }
    


    // Function to add an item to the inventory
    public bool AddItem(ItemDrop item)
    {
        // Check if the item can be stacked with an existing item
        foreach (InventoryItemData existingItem in items)
        {
            if (existingItem.itemName == item.itemName && existingItem.quantity < existingItem.maxStack)
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
            InventoryItemData newItem = new InventoryItemData
            {
                itemName = item.itemName,
                itemType = (InventoryItemData.ItemType)item.itemType,
                icon = item.icon,
                itemDropPrefab = item.itemDropPrefab,
                quantity = Mathf.Min(item.quantity, item.maxStack),
                maxStack = item.maxStack,
                ingredients = item.ingredients,
                attackPower = item.attackPower,
                defensePower = item.defensePower,
                isConsumable = item.isConsumable
            };

            items.Add(newItem);

            item.quantity -= Mathf.Min(item.quantity, item.maxStack);
        }
        Debug.Log("Item Added " + items[0]);
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
        if (items.Contains(item.inventoryItemData))
        {
            GameObject itemDrop = Instantiate(item.itemDropPrefab, transform.position, Quaternion.identity);
            itemDrop.GetComponent<ItemDrop>().Initialize(item.inventoryItemData);

            item.transform.GetChild(0).gameObject.SetActive(false);
            item.transform.GetChild(1).GetComponent<TMP_Text>().text = "";
            items.Remove(item.inventoryItemData);
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


    public void MoveItemInInventory()
    {

    }



}