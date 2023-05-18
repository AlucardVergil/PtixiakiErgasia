using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public Item item; // The item associated with this item drop

    public void Initialize(Item newItem)
    {
        item = newItem;
    }


    public void PickUp()
    {
        // Handle the pickup logic here, such as adding the item to the player's inventory
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            bool itemAdded = inventory.AddItem(item);
            if (itemAdded)
            {
                // Item added to inventory, destroy the item drop
                Destroy(gameObject);
            }
        }
    }
}