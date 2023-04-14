using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;



public class UpgradeManager : ButtonsGroupManager
{
    [Header("Set how long you need to hold down the \nmouse for upgrade to perform.")]
    public float mouseHoldActionTime = 3;
    [Header("Set the color of the image after it has \nupgraded at least once.")]
    public Color upgradedColor;
    [Header("Set the upgrade points available if you want. \nThis will change based on your level from another class.")]
    public int upgradePoints;
    [Header("Place here the text game object that will \ndisplay the upgrade points.")]
    public TMP_Text upgradePointsText;
    [Header("Set sprite that will display, when upgrade is locked.")]
    public Sprite buttonLocked;

    //The total points spent in all upgrades
    [HideInInspector] public int totalPointsSpent;



    void Start()
    {
        upgradePointsText.text = upgradePoints.ToString();
        UnlockSkills();        
    }


    private new void Update()
    {
        base.Update();        
    }


    public override void OnButtonSelected(ButtonsManager btn)
    {
        //empty just to not execute inherited method
    }    


    public override void OnButtonUp(ButtonsManager btn)
    {
        var button = (UpgradeSkillButton)btn;

        base.OnButtonUp(button);

        button.fillUpgradeImage.fillAmount = 0;
    }


    public override void OnButtonEnter(ButtonsManager btn)
    {
        var button = (UpgradeSkillButton)btn;

        base.OnButtonEnter(button);
        
        if (button.currentUpgradeNum <= button.upgradesNumber && button.upgradesNumMatchesArrayLength)
            button.upgradeInfoPanel[button.currentUpgradeNum].SetActive(true);
        else
        {
            if (button.currentUpgradeNum < button.upgradesNumber)
                button.upgradeInfoPanel[0].SetActive(true);
            else
                button.upgradeInfoPanel[1].SetActive(true);
        }
    }


    public override void OnButtonExit(ButtonsManager btn)
    {
        var button = (UpgradeSkillButton)btn;

        base.OnButtonExit(button);

        if (button.currentUpgradeNum <= button.upgradesNumber && button.upgradesNumMatchesArrayLength)
            button.upgradeInfoPanel[button.currentUpgradeNum].SetActive(false);
        else
        {
            if (button.currentUpgradeNum < button.upgradesNumber)
                button.upgradeInfoPanel[0].SetActive(false);
            else
                button.upgradeInfoPanel[1].SetActive(false);
        }

        //if (button.currentUpgradeNum <= button.upgradesNumber)
        //button.upgradeInfoPanel[button.currentUpgradeNum].SetActive(false);
    }


    //Not really hold but i made it act as hold
    public override void OnButtonHold(ButtonsManager btn)
    {
        var button = (UpgradeSkillButton)btn;

        base.OnButtonHold(button);

        StartCoroutine(OnClickHold(button));
    }


    IEnumerator OnClickHold(ButtonsManager btn)
    {
        while (mouseHoldBool)
        {
            yield return new WaitForEndOfFrame();

            var button = (UpgradeSkillButton)btn;

            if (button.currentUpgradeNum < button.upgradesNumber && upgradePoints > 0 
                && totalPointsSpent >= button.totalPointsSpentToUnlock)
            {
                if (mouseHoldTimer <= mouseHoldActionTime)
                {
                    button.fillUpgradeImage.fillAmount = mouseHoldTimer / mouseHoldActionTime;
                }
                else
                {
                    button.fillUpgradeImage.fillAmount = 0;
                    button.GetComponent<Image>().color = upgradedColor;

                    upgradePoints--;
                    upgradePointsText.text = upgradePoints.ToString();

                    totalPointsSpent++;

                    if (button.upgradesNumMatchesArrayLength)
                    {
                        button.upgradeInfoPanel[button.currentUpgradeNum].SetActive(false);
                    }                        
                    else if (button.upgradesNumber == button.currentUpgradeNum + 1)
                    {
                        button.upgradeInfoPanel[0].SetActive(false);
                    }
                                            
                    button.currentUpgradeNum++;

                    if (button.currentUpgradeNum <= button.upgradesNumber && button.upgradesNumMatchesArrayLength)
                    {
                        button.upgradeInfoPanel[button.currentUpgradeNum].SetActive(true);
                    }                        
                    else if (!button.upgradesNumMatchesArrayLength && button.currentUpgradeNum == button.upgradesNumber)
                    {
                        button.upgradeInfoPanel[1].SetActive(true);
                    }
                        

                    button.upgradeText.text = button.currentUpgradeNum + "/" + button.upgradesNumber;

                    selectedButton = button; //set clicked button as selected
                                             //New selected button is set above and it invokes the selected unity event, which calls the function
                                             //for each specific upgrade button action
                    selectedButton.Select();

                    mouseHoldBool = false;

                    UnlockSkills();
                }
            }
        }
    }


    void UnlockSkills()
    {
        foreach (UpgradeSkillButton button in buttonList)
        {
            //if selected is not empty and selected tab button equals the current tab button item from the loop
            //then continue to the next loop item without executing the rest of the code inside the loop
            if (totalPointsSpent < button.totalPointsSpentToUnlock)
            {
                button.upgradeIcon.sprite = buttonLocked;
                continue;
            }
            
            button.upgradeIcon.sprite = button.skillSprite; //sets every tab button except the selected one to the idle sprite
        }
    }
}