using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;


// NOTE these 2 scripts (TabGroup & TabButton) can work for armor grid menu too so i just added a single if statement for added
// functionality for this

[RequireComponent(typeof(Image))] //requires image component and if it doesn't exist it creates one

//Implements IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler interfaces in order to use the OnPointer mouse methods
public class UpgradeSkillButton : ButtonsManager
{
    [Header("Place the image for the upgrade fill effect.")]
    public Image fillUpgradeImage;
    [Header("Place the text game object that will display \nthe upgrade levels for this skill.")]
    public TMP_Text upgradeText;
    [Header("Set the total number of point you need to have spent \nin general, in order to unlock this upgrade.")]
    public int totalPointsSpentToUnlock;
    [Header("Place the image game object from inside the slot, \nthat will have the skill sprite or the lock sprite.")]
    public Image upgradeIcon;
    [Header("Place the sprite for the skill that will display when it is unlocked.")]
    public Sprite skillSprite;
    [Header("Set to true if upgrades number matches panels -1" +
        "\n(so that the last panel will be the end result to display \nfor player to be able to see, even if player upgraded to max).")]
    public bool upgradesNumMatchesArrayLength = true;

    //The max number of upgrades for this skill. This is automatically updated based on how
    //many upgradeInfoPanels you added in the array below
    [Header("The max number of upgrades for this skill if 'upgradesNumMatchesArrayLength' \nis false. " +
        "If it is true then this is automatically updated \nbased on how many upgradeInfoPanels you added in the array below.")]
    public int upgradesNumber = 1;

    //Current upgrade level for this skill
    [HideInInspector] public int currentUpgradeNum;

    [Header("Place the upgrade info panel that will display each \nskill's info when you hover over the upgrade button.\n " +
        "The last panel should be an extra for the end result so \nmax number of upgrades +1 for end result.")]
    public GameObject[] upgradeInfoPanel;


    new void Awake()
    {
        base.Awake();
        if (upgradesNumMatchesArrayLength)
            upgradesNumber = upgradeInfoPanel.Length - 1;
        upgradeText.text = currentUpgradeNum + "/" + upgradesNumber;
    }

}
