using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Netcode;


public class InventoryUI : NetworkBehaviour
{
    private Inventory inventory;

    [Header("Item name panel prefab that displays the name of the inventory item.")]
    public GameObject itemNamePanelPrefab; // Reference to the item details panel prefab
    [Header("The panel that will display the item details.")]
    public GameObject itemDetailsPanel;
    [Header("The panel that will display the item options.")]
    public GameObject itemOptionsPanelPrefab;

    //public LayerMask panelLayerMask;

    private void Start()
    {
        
    }


    private void Update()
    {
        // To close inventory item options when clicking anywhere
        //NOTE: Maybe use HashSet if i want to check for more panels
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            bool clickedItemOptionsPanel = false;

            foreach (var raycastHit in results)
            {
                if (raycastHit.gameObject.name == itemOptionsPanelPrefab.name + "(Clone)")
                {
                    clickedItemOptionsPanel = true;
                    break;
                }                    
            }

            if (!clickedItemOptionsPanel)
                Destroy(InventoryItem.itemOptionsStatic);
        }                
    }


    private void OnEnable()
    {
        inventory = GetComponentInParent<Inventory>();


        for (int i = 0; i < inventory.items.Count; i++)
        {
            InventoryItem inventoryItem;

            if (inventory.items[i].inventoryIndex != -1)
            {
                inventoryItem = transform.GetChild(inventory.items[i].inventoryIndex).gameObject.GetComponent<InventoryItem>();                
            }
            else
            {
                GameObject emptyInventorySlot = FindFirstEmptyInventorySlot();
                inventoryItem = emptyInventorySlot.AddComponent<InventoryItem>();
                inventoryItem.SetInventoryIndex();
                inventory.items[i].inventoryIndex = inventoryItem.inventoryIndex;
            }

            inventoryItem.Initialize(inventory.items[i]);
            inventoryItem.DisplayIconInInventory();
            inventoryItem.DisplayQuantity(inventory.items[i].quantity);
        }
    }


    public GameObject FindFirstEmptyInventorySlot()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // Check if the child does not have InventoryItem component
            if (!child.TryGetComponent<InventoryItem>(out _))   // "out _" is used to indicate that i am not interested in the actual output. It is just to check if component exists. 
                return child.gameObject;
        }
        return null; //in case there is no empty slot, but that will never happen here because there is another if that checks that before this function executes
    }
}
