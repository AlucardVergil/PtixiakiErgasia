using UnityEngine;
using UnityEngine.EventSystems;



public class InventoryItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform itemTransform;
    private CanvasGroup canvasGroup;
    private InventoryItem originalSlot;



    private void Awake()
    {
        itemTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Store the original slot before dragging
        originalSlot = transform.parent.GetComponent<InventoryItem>();

        // Make the item transparent during drag
        canvasGroup.alpha = 0.6f;

        // Set the item as the dragging object
        transform.SetParent(GetDragParent());
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the position of the dragged item based on the mouse cursor
        itemTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset the item's parent to the original slot
        transform.SetParent(originalSlot.transform);

        // Reset the position and transparency
        itemTransform.localPosition = Vector3.zero;
        canvasGroup.alpha = 1f;

        // Check if the drop target is the same as the original slot
        if (eventData.pointerEnter == originalSlot.gameObject)
        {
            // The item was dropped back into the same slot, no action needed
            return;
        }

        // Perform drop logic based on the drop target, if any
        HandleDrop(eventData.pointerEnter);
    }

    private void HandleDrop(GameObject dropTarget)
    {
        // Check if the drop target is a valid inventory slot
        InventoryItem targetSlot = dropTarget?.GetComponent<InventoryItem>();
        if (targetSlot != null)
        {
            // Get the item references from the original and target slots
            InventoryItem draggedItem = originalSlot;
            InventoryItem targetItem = targetSlot;

            if (draggedItem != null)
            {
                if (targetItem != null)
                {
                    // Swap items between slots
                    originalSlot = targetItem;
                    targetSlot = draggedItem;
                }
                else
                {
                    // Move item to the empty target slot
                    originalSlot = null;
                    targetSlot = draggedItem;
                }
            }
        }
    }

    private Transform GetDragParent()
    {
        // Determine the parent object for dragging (e.g., a dedicated dragging canvas)
        // This is where the dragged item will be moved during dragging
        // You can create an empty GameObject as the parent or use a separate canvas

        // Example: Use a dedicated dragging canvas
        return GameObject.FindGameObjectWithTag("PlayerCanvas").transform;
    }
}