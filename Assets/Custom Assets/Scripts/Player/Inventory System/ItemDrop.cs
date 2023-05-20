using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class ItemDrop : MonoBehaviour
{
    private GameObject player;
    private Inventory inventory;

    public InventoryItemData inventoryItemData;

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

    [HideInInspector] public UnityEvent onConsumeItem;


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


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        inventory = player.GetComponent<Inventory>();

        onConsumeItem = new UnityEvent();

        AddUnityEventListeners(); //the new editor saves the variables directly in the ItemDrop class public variables so i can be called at Start()
    }


    public void AddUnityEventListeners()
    {
        //effectsDropdownIndex == 0 is for restoring health
        if (itemType == ItemType.Consumable && effectsDropdownIndex == 0)
            onConsumeItem.AddListener(() => player.GetComponent<PlayerStats>().RestoreHealth(effectsValue));
    }


    public void Initialize(InventoryItemData item)
    {
        inventoryItemData = item; //this is to save the InventoryItemData object (NON-Monobehaviour) so i can use it to find or remove entries from the list

        itemName = item.itemName;
        itemType = (ItemType)item.itemType;
        icon = item.icon;
        itemDropPrefab = item.itemDropPrefab;
        quantity = item.quantity;
        maxStack = item.maxStack;
        ingredients = item.ingredients;
        itemDescription = item.itemDescription;

        attackPower = item.attackPower;
        defensePower = item.defensePower;

        effectsDropdownIndex = item.effectsDropdownIndex;
        effectsValue = item.effectsValue;        
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