using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[System.Serializable]
public class InventoryItem : ItemDrop, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector] public int inventoryIndex = -1;

    private InventoryUI inventoryUI;
    private static GameObject itemDetailsPanelStatic; // static variable of the item details panel in order to be the same for all class instances
    public static GameObject itemOptionsStatic; // static variable of the item options panel in order to be the same for all class instances
    private GameObject canvas;

    private Inventory inventory;

    private new GameObject player;

    private void Awake()
    {
        // Get all player gameobjects in the scene to loop through
        GameObject[] playersArray = GameObject.FindGameObjectsWithTag("Player");

        // Assign the correct player gameobject for each player by checking if they are owner of the player gameobject
        foreach (GameObject p in playersArray)
        {
            if (p.GetComponent<NetworkObject>().IsLocalPlayer)
                player = p;
        }

        inventory = player.GetComponent<Inventory>();
        inventoryUI = player.GetComponentInChildren<InventoryUI>();
        canvas = player.GetComponentInChildren<Canvas>().gameObject;
    }


    public void DestroyOptionsMenuAndItemNamePanel()
    {
        if (itemOptionsStatic != null)
            Destroy(itemOptionsStatic);
        if (itemDetailsPanelStatic != null)
            Destroy(itemDetailsPanelStatic);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemOptionsStatic != null)
            Destroy(itemOptionsStatic);

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Instantiate the item details panel next to the mouse cursor
            itemOptionsStatic = Instantiate(inventoryUI.itemOptionsPanelPrefab, Input.mousePosition, Quaternion.identity);
            // Attach the panel to the canvas or UI parent (adjust as needed). It is set to true so that it won't change position when setting the parent
            itemOptionsStatic.transform.SetParent(canvas.transform, true); //used canvas instead of the current inventoryItem because the panel wasn't showing right (it was hidden at some parts under the other slots)

            // Get the panel's RectTransform component
            RectTransform panelRectTransform = GetComponent<RectTransform>();

            // Get the corners of the item slot icon to set the options panel to start from one of its corners
            Vector3[] itemSlotCorners = new Vector3[4];
            panelRectTransform.GetWorldCorners(itemSlotCorners);

            // Check if the panel is outside the viewport and move it if necessary
            if (itemDetailsPanelStatic != null)            
                MovePanelIfHidden(itemSlotCorners[1]);

            ButtonListener();
            
            // Display different buttons in the item options menu based on item type
            switch (itemType)
            {
                case ItemType.Equipment:
                    itemOptionsStatic.transform.GetChild(0).gameObject.SetActive(false);
                    itemOptionsStatic.transform.GetChild(1).gameObject.SetActive(true);
                    itemOptionsStatic.transform.GetChild(2).gameObject.SetActive(true);
                    break;
                case ItemType.Consumable:
                    itemOptionsStatic.transform.GetChild(0).gameObject.SetActive(true);
                    itemOptionsStatic.transform.GetChild(1).gameObject.SetActive(false);
                    itemOptionsStatic.transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case ItemType.Resource:
                    itemOptionsStatic.transform.GetChild(0).gameObject.SetActive(false);
                    itemOptionsStatic.transform.GetChild(1).gameObject.SetActive(false);
                    itemOptionsStatic.transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case ItemType.Craftable:
                    itemOptionsStatic.transform.GetChild(0).gameObject.SetActive(false);
                    itemOptionsStatic.transform.GetChild(1).gameObject.SetActive(false);
                    itemOptionsStatic.transform.GetChild(2).gameObject.SetActive(false);
                    break;
                default:
                    Debug.Log("Invalid option selected.");
                    break;

            }
        }
    }


    // Function to add unity event listeners for the item options menu buttons
    public void ButtonListener()
    {
        Button btn;

        btn = itemOptionsStatic.transform.GetChild(0).GetComponent<Button>();
        btn.onClick.AddListener(() => inventory.ConsumeItem(this));

        //btn = itemOptionsStatic.transform.GetChild(1).GetComponent<Button>();
        //btn.onClick.AddListener(() => inventory.EquipItem(this));

        //btn = itemOptionsStatic.transform.GetChild(2).GetComponent<Button>();
        //btn.onClick.AddListener(() => inventory.RepairItem(this));

        btn = itemOptionsStatic.transform.GetChild(3).GetComponent<Button>();
        btn.onClick.AddListener(() => inventory.DropItem(this));

        btn = itemOptionsStatic.transform.GetChild(4).GetComponent<Button>();
        btn.onClick.AddListener(() => inventory.TrashItem(this));
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //to make sure no previous panel remains
        if (itemDetailsPanelStatic != null)
            Destroy(itemDetailsPanelStatic); 

        // Instantiate the item details panel next to the mouse cursor
        itemDetailsPanelStatic = Instantiate(inventoryUI.itemNamePanelPrefab, Input.mousePosition, Quaternion.identity);
        // Attach the panel to the canvas or UI parent (adjust as needed). It is set to true so that it won't change position when setting the parent
        itemDetailsPanelStatic.transform.SetParent(canvas.transform, true); //used canvas instead of the current inventoryItem because the panel wasn't showing right (it was hidden at some parts under the other slots)

        itemDetailsPanelStatic.GetComponentInChildren<TMP_Text>().text = itemName;

        // Check if the panel is outside the viewport and move it up if necessary
        if (itemDetailsPanelStatic != null)
            MovePanelIfHidden(Input.mousePosition);

        ShowItemDetails();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryUI.itemDetailsPanel.SetActive(false);

        // Destroy the item details panel Static
        Destroy(itemDetailsPanelStatic);
    }


    // Function to move panel if the panel goes outside of the canvas bounds
    private void MovePanelIfHidden(Vector3 targetPosition)
    {
        // Get the panel's RectTransform component
        if (itemDetailsPanelStatic.TryGetComponent<RectTransform>(out var panelRectTransform))
        {
            // Check if the panel goes outside the bounds of the canvas and adjust its pivot if necessary
            Vector3[] panelCorners = new Vector3[4];
            panelRectTransform.GetWorldCorners(panelCorners);

            float canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
            float canvasHeight = canvas.GetComponent<RectTransform>().rect.height;


            if (panelCorners[3].x > canvasWidth)
            {
                panelRectTransform.pivot = new Vector2(1f, panelRectTransform.pivot.y);
            }
            else if (panelCorners[1].x < 0f)
            {
                panelRectTransform.pivot = new Vector2(0f, panelRectTransform.pivot.y);
            }

            if (panelCorners[3].y > canvasHeight)
            {
                panelRectTransform.pivot = new Vector2(panelRectTransform.pivot.x, 1f);
            }
            else if (panelCorners[0].y < 0f)
            {
                panelRectTransform.pivot = new Vector2(panelRectTransform.pivot.x, 0f);
            }

            // Set the position of the panel relative to the canvas or parent object
            panelRectTransform.position = targetPosition;
        }        
    }


    public void ShowItemDetails()
    {
        inventoryUI.itemDetailsPanel.SetActive(true);

        inventoryUI.itemDetailsPanel.transform.GetChild(0).GetComponent<Image>().sprite = icon;
        inventoryUI.itemDetailsPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = itemName;
        inventoryUI.itemDetailsPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = itemDescription.ToString();
        inventoryUI.itemDetailsPanel.transform.GetChild(3).GetComponent<TMP_Text>().text = "";
    }


    public void SetInventoryIndex()
    {
        inventoryIndex = transform.GetSiblingIndex();
    }


    public void DisplayIconInInventory()
    {        
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<Image>().sprite = icon;        
    }


    public void DisplayQuantity(int quantity)
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetComponent<TMP_Text>().text = quantity.ToString();
    }
}
