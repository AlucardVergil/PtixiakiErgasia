using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonsGroupManager : MonoBehaviour
{    
    public Sprite buttonIdle;//Sprite for when a button is idle
    public Sprite buttonActive; //Sprite for when a button is clicked
    public Sprite buttonHover; //Sprite for when a button is hovered with the mouse

    [Header("OPTIONAL: You can add a tab button as default \nwhen the scene loads. " +
        "This variable is still used to \nswitch selected tab buttons at run time, \neven if left empty")]
    public ButtonsManager selectedButton; //selected TabButton    

    [HideInInspector] public List<ButtonsManager> buttonList; //List of all the buttons in the tab group
    protected int buttonIndex;
    protected float mouseHoldTimer;
    protected bool mouseHoldBool;


    protected void Update()
    {
        if (mouseHoldBool)
            mouseHoldTimer += Time.unscaledDeltaTime;
    }


    //Function used to register the tab buttons from the TabButton class to the tabButton list associated with a specific tabGroup instance
    public virtual void Register(ButtonsManager button)
    {        
        if (buttonList == null) //if tabButtons list is null, create an empty one
        {
            buttonList = new List<ButtonsManager>();
        }
        
        buttonList.Add(button); //add button to list
    }


    //Function called from TabButton class when mouse hovers over a tab button
    public virtual void OnButtonEnter(ButtonsManager button)
    {
        ResetButtons(); //Reset all tab buttons except the selected one to the idle sprite

        //check if the tab button you hovered over is the selected one and if not it sets it to the hover sprite
        if (selectedButton == null || selectedButton != button) 
        {
            button.background.sprite = buttonHover;
        }        
    }

    //Function to reset all tab buttons except the selected one, to the idle sprite when you move the mouse out of a button
    public virtual void OnButtonExit(ButtonsManager button)
    {
        ResetButtons();        
    }

    //Function to set the selected tab button sprite to the active sprite and enable the tab panel corresponding to that tab and
    //disable the rest of the tab panels. Also added armor grid functionality to switch armor pieces 
    public virtual void OnButtonSelected(ButtonsManager button)
    {
        //When a button is clicked, if there was already a selected tab button, it runs the Deselect() function which invokes 
        //the unity event that is set in the TabButton class. This unity event shows up in the inspector just like in
        //a normal button and you can add any script to it.
        if (selectedButton != null)
        {
            selectedButton.Deselect();
        }

        selectedButton = button; //set clicked button as selected

        selectedButton.Select(); //New selected button is set above and it invokes the selected unity event

        ResetButtons(); //Reset all tab buttons except the selected one to the idle sprite
        button.background.sprite = buttonActive; //Set sprite for the selected tab button to the active sprite
        
        //Get the index of the tab button among all the objects with the same parent. Every child of the same parent object (siblings)
        //in the scene, have an index based on their position in the hierarchy. This gets that index.
        buttonIndex = button.transform.GetSiblingIndex();
    }


    //Not really hold but i made it act as hold
    public virtual void OnButtonHold(ButtonsManager button)
    {
        mouseHoldBool = true;
    }


    public virtual void OnButtonUp(ButtonsManager button)
    {
        mouseHoldTimer = 0;
        mouseHoldBool = false;
    }


    public virtual void ResetButtons()
    {
        //Loops through all tab buttons from the list of a specific tab group instance
        foreach (ButtonsManager button in buttonList)
        {
            //if selected is not empty and selected tab button equals the current tab button item from the loop
            //then continue to the next loop item without executing the rest of the code inside the loop
            if (selectedButton != null && selectedButton == button) 
            { 
                continue; 
            }
            
            button.background.sprite = buttonIdle; //sets every tab button except the selected one to the idle sprite
        }
    }

}