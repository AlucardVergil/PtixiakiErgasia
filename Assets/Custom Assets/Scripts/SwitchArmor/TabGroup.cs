using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TabGroup : ButtonsGroupManager
{
    public List<GameObject> pagesList; //List for all the tab panels


    //Function to set the selected tab button sprite to the active sprite and enable the tab panel corresponding to that tab and
    //disable the rest of the tab panels. Also added armor grid functionality to switch armor pieces 
    public override void OnButtonSelected(ButtonsManager button)
    {
        base.OnButtonSelected(button);

        //loops through all tab panels and only enables the tab panel corresponding to the clicked tab button sibling index
        for (int i = 0; i < pagesList.Count; i++)
        {
            if (i == buttonIndex)
            {
                pagesList[i].SetActive(true);
            }
            else
            {
                pagesList[i].SetActive(false);
            }
        }
    }

}
