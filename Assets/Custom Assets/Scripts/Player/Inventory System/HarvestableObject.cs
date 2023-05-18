using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableObject : ItemDrop
{
    [SerializeField] private float hp;

    private bool isHarvested = false;


    private void Harvest()
    {
        if (!isHarvested)
        {
            isHarvested = true;

            // Instantiate the item drop at the gatherable object's position
            GameObject itemDrop = Instantiate(itemDropPrefab, transform.position, Quaternion.identity);

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