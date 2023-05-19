using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class InventoryItem : ItemDrop, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public int inventoryIndex = -1;

    private InventoryUI inventoryUI;
    private GameObject itemDetailsPanelInstance; // Instance of the item details panel
    private GameObject canvas;


    private void Awake()
    {
        inventoryUI = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryUI>();
        canvas = GameObject.FindGameObjectWithTag("PlayerCanvas");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");

        //to make sure no previous panel remains
        if (itemDetailsPanelInstance != null)
            Destroy(itemDetailsPanelInstance); 

        // Instantiate the item details panel next to the mouse cursor
        itemDetailsPanelInstance = Instantiate(inventoryUI.itemDetailsPanelPrefab, Input.mousePosition, Quaternion.identity);
        // Attach the panel to the canvas or UI parent (adjust as needed). It is set to true so that it won't change position when setting the parent
        itemDetailsPanelInstance.transform.SetParent(canvas.transform, true); //used canvas instead of the current inventoryItem because the panel wasn't showing right (it was hidden at some parts under the other slots)

        itemDetailsPanelInstance.GetComponentInChildren<TMP_Text>().text = itemName;

        // Check if the panel is outside the viewport and move it up if necessary
        MovePanelIfHidden();

        ShowItemDetails();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        // Mouse exit logic
        // Hide item details when the mouse exits the inventory item

        // Destroy the item details panel instance
        Destroy(itemDetailsPanelInstance);
    }

    private void MovePanelIfHidden()
    {
        // Get the panel's RectTransform component
        RectTransform panelRectTransform = itemDetailsPanelInstance.GetComponent<RectTransform>();

        // Check if the panel goes outside the bounds of the canvas and adjust its pivot if necessary
        Vector3[] panelCorners = new Vector3[4];
        panelRectTransform.GetWorldCorners(panelCorners);

        float canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        float canvasHeight = canvas.GetComponent<RectTransform>().rect.height;      


        if (panelCorners[3].x > canvasWidth)
        {
            Debug.Log("panelCorners[3].x " + panelCorners[3]);
            panelRectTransform.pivot = new Vector2(1f, panelRectTransform.pivot.y);
        }
        else if (panelCorners[1].x < 0f)
        {
            Debug.Log("panelCorners[1].x " + panelCorners[1]);
            panelRectTransform.pivot = new Vector2(0f, panelRectTransform.pivot.y);
        }

        if (panelCorners[3].y > canvasHeight)
        {
            Debug.Log("panelCorners[3].y " + panelCorners[3]);
            panelRectTransform.pivot = new Vector2(panelRectTransform.pivot.x, 1f);
        }
        else if (panelCorners[0].y < 0f)
        {
            Debug.Log("panelCorners[0].y " + panelCorners[0]);
            panelRectTransform.pivot = new Vector2(panelRectTransform.pivot.x, 0f);
        }

        // Set the position of the panel relative to the canvas or parent object
        panelRectTransform.position = Input.mousePosition;
    }


    public void ShowItemDetails()
    {
        
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

    public void ShowDescription()
    {

    }
}
