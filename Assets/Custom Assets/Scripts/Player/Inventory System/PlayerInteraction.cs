using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 2f;
    public LayerMask interactionLayer;
    public Transform crosshair;
    public Camera cam;

    private bool isInteracting = false;
    private HarvestableObject currentHarvestable;
    private ItemDrop currentItemDrop;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInteracting)
            {
                // Stop the interaction
                StopInteraction();
            }
            else
            {
                // Start the interaction
                StartInteraction();
            }
        }
    }

    private void StartInteraction()
    {
        // Raycast from the crosshair position forward
        //Ray ray = new Ray(crosshair.position, crosshair.forward);
        Ray ray = cam.ViewportPointToRay(new Vector3(0.51f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactionLayer))
        {
            Debug.Log("HIT");
            // Check if the raycast hit a harvestable resource            
            if (hit.collider.TryGetComponent<HarvestableObject>(out var harvestable))
            {
                currentHarvestable = harvestable;
                isInteracting = true;

                // Highlight the harvestable resource or provide visual feedback

                // Perform any other actions or effects related to starting the interaction
            }
            else
            {
                // Check if the raycast hit an item drop
                if (hit.collider.TryGetComponent<ItemDrop>(out var itemDrop))
                {
                    currentItemDrop = itemDrop;
                    isInteracting = true;

                    // Highlight the item drop or provide visual feedback

                    // Perform any other actions or effects related to starting the interaction
                }
            }
        }
    }

    private void StopInteraction()
    {
        if (currentHarvestable != null)
        {
            // Perform any actions or effects related to stopping the interaction

            // Harvest the resource. When harvestable object hp reaches 0 a gameobject spawns with ItemDrop component
            currentHarvestable.DamageHarvestable(30);

            isInteracting = false;
            currentHarvestable = null;
        }
        else if (currentItemDrop != null)
        {
            // Perform any actions or effects related to stopping the interaction

            // Pick up the item
            currentItemDrop.PickUp();

            isInteracting = false;
            currentItemDrop = null;
        }
    }
}
