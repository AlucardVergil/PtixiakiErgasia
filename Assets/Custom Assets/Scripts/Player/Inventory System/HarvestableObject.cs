using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableObject : MonoBehaviour
{
    public Item item; // The item to be dropped when harvested
    [SerializeField] private float hp;

    private bool isHarvested = false;


    private void Harvest()
    {
        if (!isHarvested)
        {
            isHarvested = true;

            // Instantiate the item drop at the gatherable object's position
            GameObject itemDrop = Instantiate(item.itemDropPrefab, transform.position, Quaternion.identity);

            // Pass the item data to the item drop script
            ItemDrop itemDropScript = itemDrop.GetComponent<ItemDrop>();
            itemDropScript.Initialize(item);

            // Destroy the gatherable object
            Destroy(gameObject);
        }
        
    }


    public void DamageHarvestable(float damage)
    {
        hp -= damage;
        if (hp <= 0) 
        {
            Harvest();
        }
    }
}