using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceHPSliderAtPlayer : MonoBehaviour
{
    private Transform playerCamera;

    // LateUpdate is called once per frame after the update method, so that it changes the HP Bar position
    // after we first change the camera position by looking around with the mouse.

    private void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void LateUpdate()
    {
        transform.LookAt(playerCamera); //Rotate enemyCanvas which contains the enemy healthbar to face the player camera
    }
}
