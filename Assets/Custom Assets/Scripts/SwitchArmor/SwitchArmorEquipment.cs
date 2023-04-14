using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchArmorEquipment : MonoBehaviour
{
    //Emum to use in switch case in change armor parts function
    enum ArmorParts
    {
        HEAD_ARMOR,
        CHEST_ARMOR,
        HANDS_ARMOR,
        LEGS_ARMOR,
        FEET_ARMOR
    }

    //Arrays that hold all armor model parts. Index 0 will be the default naked mesh without armor
    [Header("Index 0 is reserved for default naked mesh")] //display an instruction message in the inspector

    //SerializeField is to serialize non-public fields. Public fields automatically serialize. Serialization saves its state to the disk ,
    //instead of the ram for future use. Here i mainly use it to keep those variables private but still display them in the inspector.
    //Public variables also show up since they are auto seriallized but in this case i also want them to be private.
    [SerializeField] private SkinnedMeshRenderer[] heads; 
    [SerializeField] private SkinnedMeshRenderer[] chests;
    [SerializeField] private SkinnedMeshRenderer[] hands;
    [SerializeField] private SkinnedMeshRenderer[] legs;
    [SerializeField] private SkinnedMeshRenderer[] feet;


    //Indexes to switch between armor and also used to save the selections in the dictionary in order to load them into the game scene
    int headsIndex = 0;
    int chestsIndex = 0;
    int handsIndex = 0;
    int legsIndex = 0;
    int feetIndex = 0;

    //GameObjects to store the separate parts from the modular character i created
    GameObject headObj;
    GameObject chestObj;
    GameObject handsObj;
    GameObject legsObj;
    GameObject feetObj;

    //SkinnedMeshRenderer variables to store the SkinnedMeshRenderers from the above objects
    SkinnedMeshRenderer headSkmr;
    SkinnedMeshRenderer chestSkmr;
    SkinnedMeshRenderer handsSkmr;
    SkinnedMeshRenderer legsSkmr;
    SkinnedMeshRenderer feetSkmr;

    //Dictionary to save the armor selections in order to load them in the game scene
    Dictionary<ArmorParts, int> savedArmor;

    //Placed find methods and GetComponentInChildren methods in Awake(), so that they are called once when script is
    //loaded instead of each time changeArmorParts function is called
    private void Awake()
    {
        //Get objects from scene
        headObj = GameObject.Find("Male_model_head");
        chestObj = GameObject.Find("Male_model_chest");
        handsObj = GameObject.Find("Male_model_hands");
        legsObj = GameObject.Find("Male_model_legs");
        feetObj = GameObject.Find("Male_model_feet");

        //Get component from game objects
        headSkmr = headObj.GetComponent<SkinnedMeshRenderer>();
        chestSkmr = chestObj.GetComponent<SkinnedMeshRenderer>();
        handsSkmr = handsObj.GetComponent<SkinnedMeshRenderer>();
        legsSkmr = legsObj.GetComponent<SkinnedMeshRenderer>();
        feetSkmr = feetObj.GetComponent<SkinnedMeshRenderer>();
    }


    #region Next And Previous Buttons For Switching Between Armor Parts In Order
    public void NextHeadArmor()
    {
        if (headsIndex < heads.Length - 1)
            headsIndex++;
        else
            headsIndex = 0;

        ChangeArmorPart(ArmorParts.HEAD_ARMOR, headsIndex);
    }


    public void PreviousHeadArmor()
    {
        if (headsIndex > 0)
            headsIndex--;
        else
            headsIndex = heads.Length - 1;

        ChangeArmorPart(ArmorParts.HEAD_ARMOR, headsIndex);
    }


    public void NextChestArmor()
    {
        if (chestsIndex < chests.Length - 1)
            chestsIndex++;
        else
            chestsIndex = 0;

        ChangeArmorPart(ArmorParts.CHEST_ARMOR, chestsIndex);
    }


    public void PreviousChestArmor()
    {
        if (chestsIndex > 0)
            chestsIndex--;
        else
            chestsIndex = chests.Length - 1;

        ChangeArmorPart(ArmorParts.CHEST_ARMOR, chestsIndex);
    }


    public void NextHandsArmor()
    {
        if (handsIndex < hands.Length - 1)
            handsIndex++;
        else
            handsIndex = 0;

        ChangeArmorPart(ArmorParts.HANDS_ARMOR, handsIndex);
    }


    public void PreviousHandsArmor()
    {
        if (handsIndex > 0)
            handsIndex--;
        else
            handsIndex = hands.Length - 1;

        ChangeArmorPart(ArmorParts.HANDS_ARMOR, handsIndex);
    }

    public void NextLegsArmor()
    {
        if (legsIndex < legs.Length - 1)
            legsIndex++;
        else
            legsIndex = 0;

        ChangeArmorPart(ArmorParts.LEGS_ARMOR, legsIndex);
    }


    public void PreviousLegsArmor()
    {
        if (legsIndex > 0)
            legsIndex--;
        else
            legsIndex = legs.Length - 1;

        ChangeArmorPart(ArmorParts.LEGS_ARMOR, legsIndex);
    }

    public void NextFeetArmor()
    {
        if (feetIndex < feet.Length - 1)
            feetIndex++;
        else
            feetIndex = 0;

        ChangeArmorPart(ArmorParts.FEET_ARMOR, feetIndex);
    }


    public void PreviousFeetArmor()
    {
        if (feetIndex > 0)
            feetIndex--;
        else
            feetIndex = feet.Length - 1;

        ChangeArmorPart(ArmorParts.FEET_ARMOR, feetIndex);
    }
    #endregion

    #region Switch Between Armor Parts By Selecting Specific Armor Part Icon Button In The Grid Menu
    public void SwitchHeadArmor(int i)
    {
        headsIndex = i; //save value in this variable for use in SaveArmor()
        ChangeArmorPart(ArmorParts.HEAD_ARMOR, i);
    }


    public void SwitchChestArmor(int i)
    {
        chestsIndex = i; //save value in this variable for use in SaveArmor()
        ChangeArmorPart(ArmorParts.CHEST_ARMOR, i);
    }


    public void SwitchHandsArmor(int i)
    {
        handsIndex = i; //save value in this variable for use in SaveArmor()
        ChangeArmorPart(ArmorParts.HANDS_ARMOR, i);
    }


    public void SwitchLegsArmor(int i)
    {
        legsIndex = i; //save value in this variable for use in SaveArmor()
        ChangeArmorPart(ArmorParts.LEGS_ARMOR, i);
    }


    public void SwitchFeetArmor(int i)
    {
        feetIndex = i; //save value in this variable for use in SaveArmor()
        ChangeArmorPart(ArmorParts.FEET_ARMOR, i);
    }
    #endregion

    //Place the armor in the armor parts array
    void ChangeArmorPart(ArmorParts armor, int id)
    {   
        switch (armor)
        {
            case ArmorParts.HEAD_ARMOR:
                headSkmr.sharedMesh = heads[id].sharedMesh; //change the mesh with the one from the array
                headSkmr.sharedMaterials = heads[id].sharedMaterials; //change the materials of the mesh
                GetComponent<CustomizeCharacterParts>().HideHairWhenWearingHelmet(id); // hide hair when wearing helmet
                break;
            case ArmorParts.CHEST_ARMOR:
                chestSkmr.sharedMesh = chests[id].sharedMesh;
                chestSkmr.sharedMaterials = chests[id].sharedMaterials;
                break;
            case ArmorParts.HANDS_ARMOR:
                handsSkmr.sharedMesh = hands[id].sharedMesh;
                handsSkmr.sharedMaterials = hands[id].sharedMaterials;
                break;
            case ArmorParts.LEGS_ARMOR:
                legsSkmr.sharedMesh = legs[id].sharedMesh;
                legsSkmr.sharedMaterials = legs[id].sharedMaterials;
                break;
            case ArmorParts.FEET_ARMOR:
                feetSkmr.sharedMesh = feet[id].sharedMesh;
                feetSkmr.sharedMaterials = feet[id].sharedMaterials;
                break;
        }
    }


    //Function to save armor pieces in dictionary for use in game scene
    public void SaveArmor()
    {
        savedArmor = new Dictionary<ArmorParts, int>();

        savedArmor.Add(ArmorParts.HEAD_ARMOR, headsIndex);
        savedArmor.Add(ArmorParts.CHEST_ARMOR, chestsIndex);
        savedArmor.Add(ArmorParts.HANDS_ARMOR, handsIndex);
        savedArmor.Add(ArmorParts.LEGS_ARMOR, legsIndex);
        savedArmor.Add(ArmorParts.FEET_ARMOR, feetIndex);
    }


    //Function called from GameScene to load armor pieces in controller's model
    public void LoadArmor()
    {
        //I renamed the meshes of the character like that in GameScene to prevent mix-up with find. The character from menu
        //scene was transfered to the game scene and then deleted and i just kept the armor piece choices in a dictionary. The
        //menu character was generic rig in order to play the animations i created in blender. There is another copy of the character,
        //set to humanoid rig in order to play the third person controller animations. So i just used the dictionary to set the same
        //armor pieces that i had set in the menu scene and because there were 2 copies of the same character(just before the menu
        //character was destroyed) the find method was getting mixed-up and it was choosing the menu character when i wanted to select
        //the game scene character in order to load the saved armor pieces from the dictionary.
        //So i renamed the game scene character with "_2" at the end to fix that.
        GameObject gameSceneHeadObj = GameObject.Find("Male_model_head_2");
        GameObject gameSceneChestObj = GameObject.Find("Male_model_chest_2");
        GameObject gameSceneHandsObj = GameObject.Find("Male_model_hands_2");
        GameObject gameSceneLegsObj = GameObject.Find("Male_model_legs_2");
        GameObject gameSceneFeetObj = GameObject.Find("Male_model_feet_2");

        SkinnedMeshRenderer gameSceneHeadSkmr = gameSceneHeadObj.GetComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer gameSceneChestSkmr = gameSceneChestObj.GetComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer gameSceneHandsSkmr = gameSceneHandsObj.GetComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer gameSceneLegsSkmr = gameSceneLegsObj.GetComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer gameSceneFeetSkmr = gameSceneFeetObj.GetComponent<SkinnedMeshRenderer>();

        
        gameSceneHeadSkmr.sharedMesh = heads[savedArmor[ArmorParts.HEAD_ARMOR]].sharedMesh;
        gameSceneHeadSkmr.sharedMaterials = heads[savedArmor[ArmorParts.HEAD_ARMOR]].sharedMaterials;
        GetComponent<CustomizeCharacterParts>().HideHairWhenWearingHelmet(savedArmor[ArmorParts.HEAD_ARMOR]); //Hide hair if character has helmet

        gameSceneChestSkmr.sharedMesh = chests[savedArmor[ArmorParts.CHEST_ARMOR]].sharedMesh;
        gameSceneChestSkmr.sharedMaterials = chests[savedArmor[ArmorParts.CHEST_ARMOR]].sharedMaterials;
        
        gameSceneHandsSkmr.sharedMesh = hands[savedArmor[ArmorParts.HANDS_ARMOR]].sharedMesh;
        gameSceneHandsSkmr.sharedMaterials = hands[savedArmor[ArmorParts.HANDS_ARMOR]].sharedMaterials;

        gameSceneLegsSkmr.sharedMesh = legs[savedArmor[ArmorParts.LEGS_ARMOR]].sharedMesh;
        gameSceneLegsSkmr.sharedMaterials = legs[savedArmor[ArmorParts.LEGS_ARMOR]].sharedMaterials;

        gameSceneFeetSkmr.sharedMesh = feet[savedArmor[ArmorParts.FEET_ARMOR]].sharedMesh;
        gameSceneFeetSkmr.sharedMaterials = feet[savedArmor[ArmorParts.FEET_ARMOR]].sharedMaterials;

    }


}
