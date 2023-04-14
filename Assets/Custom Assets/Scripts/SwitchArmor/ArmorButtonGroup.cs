using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//NOTE: If i want to use this script for item Grid Menu, rename all images that will act as button for
//switching equipment as 'Head Armor' or 'Chest Armor' etc for it to work
public class ArmorButtonGroup : ButtonsGroupManager
{
    SwitchArmorEquipment switchArmor;


    // Start is called before the first frame update
    void Start()
    {
        //SwitchArmorEquipment Class Object in order to call function from there for the added functionality of
        //switching armor from the armor grid menu
        switchArmor = GameObject.Find("CharacterCustomization").GetComponent<SwitchArmorEquipment>();
    }


    public override void OnButtonSelected(ButtonsManager button)
    {
        base.OnButtonSelected(button);

        //Added functionality for use with armor grid menu instead of tab menu! 
        //checks if the clicked tab button's name is any of the below names and if so, it knows to treat it as an armor grid menu button.
        //Tab buttons objects in the scene need to follow this name convention for armor grid functionality to work.
        if (button.name == "Head Armor" || button.name == "Chest Armor" || button.name == "Hands Armor"
            || button.name == "Legs Armor" || button.name == "Feet Armor")
        {
            //Based on the button type (head, chest etc), calls the corresponding function to switch armor and passes the armor grid
            //tab button's sibling index
            switch (button.name)
            {
                case "Head Armor":
                    switchArmor.SwitchHeadArmor(buttonIndex);
                    break;
                case "Chest Armor":
                    switchArmor.SwitchChestArmor(buttonIndex);
                    break;
                case "Hands Armor":
                    switchArmor.SwitchHandsArmor(buttonIndex);
                    break;
                case "Legs Armor":
                    switchArmor.SwitchLegsArmor(buttonIndex);
                    break;
                case "Feet Armor":
                    switchArmor.SwitchFeetArmor(buttonIndex);
                    break;
            }
        }
    }
    

}