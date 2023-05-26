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

    [SerializeField] NetworkPlayerOwnership player;
    private bool playerIsOwner = true;



    private void Start()
    {
        if (player != null) 
            playerIsOwner = player.GetPlayerOwnershipStatus();


        helpBtn = transform.parent.gameObject;
        if (helpBtn != null)
            helpPanel = helpBtn.transform.GetChild(0).gameObject;

        //if (GameObject.FindGameObjectWithTag("Player") != null)
            //Time.timeScale = 0f;
    }


    public void ShowHideManual()
    {
        if (!player.GetPlayerOwnershipStatus()) return; // For NetworkBehaviour

        helpPanel.SetActive(!helpPanel.activeSelf);
    }

    public void NextTip()
    {
        if (!player.GetPlayerOwnershipStatus()) return; // For NetworkBehaviour

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
        if (!player.GetPlayerOwnershipStatus()) return; // For NetworkBehaviour

        //Time.timeScale = 1f;

        helpPanel = transform.parent.GetChild(1).gameObject; //GameObject.Find("HelpPanel (1)");
        helpPanel.SetActive(false);
        GetComponentInParent<PlayerInput>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

}
