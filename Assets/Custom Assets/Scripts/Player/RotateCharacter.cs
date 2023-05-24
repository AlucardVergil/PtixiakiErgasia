using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharacter : MonoBehaviour
{
    
    Vector3 mouseLastPosition = Vector3.zero; 
    Vector3 mousePositionDelta = Vector3.zero;
    [Header("Add character rotation speed. Values between 0.1 to 1.0")]
    [SerializeField] float rotateSpeed = 0.3f; //Variable to adjust the character rotate speed. Default is 0.3f
    bool BtnPressedOutsideMenu = false; //Variable to check if mouse was first clicked when outside the menu panel
    Canvas canvas;

  
    //void Update()
    //{
    //    if (canvas == null)
    //        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

    //    //checks where was the mouse in the first frame when you initially clicked and if it was outside the menu screen
    //    //then returns true and keeps it like that until you release the pressed button, so that you can't rotate the character
    //    //if the mouse was first pressed on top of the menu screen and then moved outside of the menu (while it was held down).
    //    //You can only rotate him if the moment you click the mouse and hold it down, the mouse was outside the menu panel
    //    if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > canvas.renderingDisplaySize.x * 0.3206)        
    //        BtnPressedOutsideMenu = true;         
    //    else if(!Input.GetMouseButton(0)) //set bool to false only when you release the pressed button
    //        BtnPressedOutsideMenu = false;

    //    //Returns true if you pressed the left mouse button and since it updates every frame, it is basically true as long
    //    //as you keep it pressed
    //    if (BtnPressedOutsideMenu) 
    //    {    
    //        //Restricts the rotateSpeed value between 0.1 and 1.0 values. If it is less than the min it returns the min
    //        //and if more than max returns the max, otherwise it returns the given value
    //        rotateSpeed = Mathf.Clamp(rotateSpeed, 0.1f, 1);
            
    //        //Difference between the current mouse position and the last frame's mouse position
    //        mousePositionDelta = Input.mousePosition - mouseLastPosition;

    //        //Rotate character transform around the 'up' axis which is the green axis, by the number given as degrees by the dot product(
    //        //the value returned by the Vector3.Dot is used as the degrees by which the character rotates and it 
    //        //is multiplied by the rotateSpeed float to increase or decrease the angle and thus adjust the rotation speed if you want),
    //        //relative to the world space            
    //        transform.Rotate(transform.up, Vector3.Dot(mousePositionDelta, Camera.main.transform.right) * rotateSpeed, Space.World);
    //    }
        

    //    mouseLastPosition = Input.mousePosition; //Update the mouse's last position for use in the next frame as a reference point
    //}
}
