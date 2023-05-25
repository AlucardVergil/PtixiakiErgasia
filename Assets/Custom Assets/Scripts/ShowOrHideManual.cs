using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;


public class ShowOrHideManual : NetworkBehaviour
{    
    GameObject helpBtn;
    GameObject helpPanel;
    int helpPanelIndex = 0;



    private void Start()
    {
        helpBtn = GameObject.Find("Help Button");
        if (helpBtn != null)
            helpPanel = helpBtn.transform.GetChild(0).gameObject;

        if (GameObject.FindGameObjectWithTag("Player") != null)
            Time.timeScale = 0f;
    }


    public void ShowHideManual()
    {       
        helpPanel.SetActive(!helpPanel.activeSelf);
    }

    public void NextTip()
    {
        helpPanelIndex = helpPanel.transform.GetSiblingIndex() + 1;
        helpPanel.SetActive(false);

        if (helpPanelIndex <= helpBtn.transform.childCount)
        {
            helpPanel = helpBtn.transform.GetChild(helpPanelIndex).gameObject;
            helpPanel.SetActive(true);
        }
    }

    public void GameSceneTip()
    {
        Time.timeScale = 1f;
        helpPanel = GameObject.Find("HelpPanel (1)");
        helpPanel.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

}
