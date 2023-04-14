using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


// NOTE these 2 scripts (TabGroup & TabButton) can work for armor grid menu too so i just added a single if statement for added
// functionality for this

[RequireComponent(typeof(Image))] //requires image component and if it doesn't exist it creates one

//Implements IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler interfaces in order to use the OnPointer mouse methods
public class ButtonsManager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public ButtonsGroupManager buttonsGroup; //tab group instance to register the tab button into.

    //Public variable hidden from inspector because i want it to change only via
    //script but still be accessed from TabGroup class. Public variables show up in inspector automatically.
    //NOTE: Could also create a get method to access it if i wanted it to be private, but i don't need it to be private.
    [HideInInspector] public Image background;

    //Unity events in case you want to add extra functionality to the buttons when you select or deselect a button
    //These show up inside the inspector just like a normal button and you can add use them just like you would a normal button,
    //to execute scripts
    public UnityEvent onButtonSelected;
    public UnityEvent onButtonDeselected;



    protected void Awake()
    {
        background = GetComponent<Image>();
        buttonsGroup.Register(this); //Register this tab button to the tab group instance
    }

    //Event for when you click the tab button
    public void OnPointerClick(PointerEventData eventData)
    {
        buttonsGroup.OnButtonSelected(this); //execute function from TabGroup class using this tab button
    }

    //Event for when you hover over a tab button
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonsGroup.OnButtonEnter(this); //execute function from TabGroup class using this tab button
    }

    //Event when you move the mouse out of the tab button
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonsGroup.OnButtonExit(this); //execute function from TabGroup class using this tab button
    }

    //Event when you hold the mouse down on the tab button
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonsGroup.OnButtonHold(this); //Not really hold but i made it act as hold
    }

    //Event when you release the mouse from the tab button
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonsGroup.OnButtonUp(this);
    }



    //These are function that invoke unity events. Unity events shows up in the inspector just like in
    //a normal button and you can add any script to them.
    public void Select()
    {
        if (onButtonSelected != null)
        {
            onButtonSelected.Invoke(); //Runs all registered scripts inside this unity event
        }
    }

    public void Deselect()
    {
        if (onButtonDeselected != null)
        {
            onButtonDeselected.Invoke(); //Runs all registered scripts inside this unity event
        }
    }


}