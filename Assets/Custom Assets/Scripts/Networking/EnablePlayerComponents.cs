using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePlayerComponents : MonoBehaviour
{
    private GameObject player;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        MonoBehaviour[] components = player.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour comp in components)
        {
            comp.enabled = true;
        }
    }

}
