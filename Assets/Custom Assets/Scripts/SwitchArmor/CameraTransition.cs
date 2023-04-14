using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker; //Start marker will be the main camera's current position

    //Markers to set the different cam locations. Pass empty game objects and set them to the position and rotation you want
    public Transform fullBodyMarker; 
    public Transform headMarker;
    public Transform chestMarker;
    public Transform handsMarker;
    public Transform legsMarker;
    public Transform feetMarker;

    private Transform endMarker;// End marker that takes as value the above markers

    // Movement speed of the camera transition in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    //enum variable to save the last camera view so that it will load that if you switch to fullbody cam and then back to focused cams
    enum CameraViews
    {
        HeadView,
        ChestView,
        HandsView,
        LegsView,
        FeetView
    }

    CameraViews camera;

    bool focusedViewToggle = false; // Toggle for switching between zoomed out fullbody camera and focus body parts cameras

    void Start() //Switch to fullbody cam view by default when loading scene
    {
        // Keep a note of the time the movement started. The Time.time returns how much time passed from the moment the game started.
        startTime = Time.time;
        
        endMarker = fullBodyMarker; //Set endmarker to the fullbodymarker position for when you load the scene

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Move to the target end position.
    void Update()
    {
        // Distance moved equals elapsed time (current time minus time at the start) multiplied by speed. 
        float distanceCovered = (Time.time - startTime) * speed;

        // Percentage of journey completed equals current distance divided by total distance.
        float percentageOfJourneyCovered = distanceCovered / journeyLength;

        // Set our position and rotation as a percentage of the distance between the markers. Moves transform from starter point to
        // endpoint based on the value of percentageOfJourneyCovered, which takes values from 0.0 to 1.0. For example if
        // percentageOfJourneyCovered is 0.5 then if moves it to the middle position between the 2 points
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, percentageOfJourneyCovered);
        transform.rotation = Quaternion.Lerp(startMarker.rotation, endMarker.rotation, percentageOfJourneyCovered);
    }


    #region Switch Between Camera Views
    public void SwitchToHeadCamera()
    {
        camera = CameraViews.HeadView; //save camera view for use in the toggle_changed switch case

        //checks if starting point is the same and ending point so that it will not do anything if you are already there
        //and also check if focused view is enabled
        if (endMarker != headMarker && focusedViewToggle) 
        {
            // Keep a note of the time the movement started.
            startTime = Time.time;
            //Set the endmarker position to this camera view
            endMarker = headMarker;

            // Calculate the journey length.
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        }
    }


    public void SwitchToChestCamera()
    {
        camera = CameraViews.ChestView; //save camera view for use in the toggle_changed switch case

        //checks if starting point is the same and ending point so that it will not do anything if you are already there
        //and also check if focused view is enabled
        if (endMarker != chestMarker && focusedViewToggle)
        {
            // Keep a note of the time the movement started.
            startTime = Time.time;
            //Set the endmarker position to this camera view
            endMarker = chestMarker;

            // Calculate the journey length.
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        }
    }


    public void SwitchToHandsCamera()
    {
        camera = CameraViews.HandsView; //save camera view for use in the toggle_changed switch case

        //checks if starting point is the same and ending point so that it will not do anything if you are already there
        //and also check if focused view is enabled
        if (endMarker != handsMarker && focusedViewToggle)
        {
            // Keep a note of the time the movement started.
            startTime = Time.time;
            //Set the endmarker position to this camera view
            endMarker = handsMarker;

            // Calculate the journey length.
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        }
    }


    public void SwitchToLegsCamera()
    {
        camera = CameraViews.LegsView; //save camera view for use in the toggle_changed switch case

        //checks if starting point is the same and ending point so that it will not do anything if you are already there
        //and also check if focused view is enabled
        if (endMarker != legsMarker && focusedViewToggle)
        {
            // Keep a note of the time the movement started.
            startTime = Time.time;
            //Set the endmarker position to this camera view
            endMarker = legsMarker;

            // Calculate the journey length.
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        }
    }


    public void SwitchToFeetCamera()
    {
        camera = CameraViews.FeetView; //save camera view for use in the toggle_changed switch case

        //checks if starting point is the same and ending point so that it will not do anything if you are already there
        //and also check if focused view is enabled
        if (endMarker != feetMarker && focusedViewToggle)
        {
            // Keep a note of the time the movement started.
            startTime = Time.time;
            //Set the endmarker position to this camera view
            endMarker = feetMarker;

            // Calculate the journey length.
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        }
    }


    public void SwitchToFullBodyCamera()
    {
        //checks if starting point is the same and ending point so that it will not do anything if you are already there
        if (endMarker != fullBodyMarker)
        {           
            // Keep a note of the time the movement started.
            startTime = Time.time;
            //Set the endmarker position to this camera view
            endMarker = fullBodyMarker;

            // Calculate the journey length.
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        }        
    }
    #endregion

    //Function called when you change value of the "Full/FocusedViewToggle" toggle in the scene and passes the value of the toggle
    public void Toggle_Changed(bool value)
    {
        focusedViewToggle = value;

        if (!focusedViewToggle) //if focused view is not enabled the switch to fullbody cam else switch to one of the focused cams
        {
            SwitchToFullBodyCamera();
        }
        else
        {
            switch (camera)
            {
                case CameraViews.HeadView:
                    SwitchToHeadCamera();
                    break;
                case CameraViews.ChestView:
                    SwitchToChestCamera();
                    break;
                case CameraViews.HandsView:
                    SwitchToHandsCamera();
                    break;
                case CameraViews.LegsView:
                    SwitchToLegsCamera();
                    break;
                case CameraViews.FeetView:
                    SwitchToFeetCamera();
                    break;
            }
        }
    }

}
