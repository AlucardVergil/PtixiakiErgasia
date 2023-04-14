using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeCharacterParts : MonoBehaviour
{
    
    enum AppearanceDetails
    {
        HAIR_MODEL,
        BEARD_MODEL,
        EAR_MODEL,
        HORNS_MODEL,
        TAILS_MODEL,
        SKIN_COLOR,
        HAIR_COLOR,
        IRIS_COLOR
    }


    [SerializeField] private GameObject[] earModels;
    [SerializeField] private GameObject[] hornsModels;
    [SerializeField] private GameObject[] tailsModels;
    [SerializeField] private GameObject[] hairModels;
    [SerializeField] private GameObject[] beardModels;


    [SerializeField] private Transform face; //The bone that will be the parent to the models (ears, horns etc)
    [SerializeField] private Transform tailBone; //The bone that will be the parent to the models (tails)

    GameObject activeEars;
    GameObject activeHorns;
    GameObject activeTails;
    GameObject activeHair;
    GameObject activeBeard;


    int earsIndex = 0;
    int hornsIndex = 0;
    int tailsIndex = 0;
    int hairIndex = 0;
    int beardIndex = 0;

    //Dictionary that saves the model selection for use in gameScene. Saves enum for model type and int for selected index
    Dictionary<AppearanceDetails, int> savedAppearance; 

    GameObject newFace;
    GameObject newTailBone;


    void Start()
    {
        Randomize();
    }


    //Randomizes some default models for when the scene loads. Checks if models exist and chooses a random one
    public void Randomize()
    {
        earsIndex = Random.Range(0, earModels.Length);
        hornsIndex = Random.Range(0, hornsModels.Length);
        tailsIndex = Random.Range(0, tailsModels.Length);
        hairIndex = Random.Range(0, hairModels.Length);
        beardIndex = Random.Range(0, beardModels.Length);

        ChangeBodyPartsModel(AppearanceDetails.EAR_MODEL, earsIndex);

        if (hornsModels.Length != 0)
            ChangeBodyPartsModel(AppearanceDetails.HORNS_MODEL, hornsIndex);

        if (tailsModels.Length != 0)
            ChangeBodyPartsModel(AppearanceDetails.TAILS_MODEL, tailsIndex);

        if (hairModels.Length != 0)
            ChangeBodyPartsModel(AppearanceDetails.HAIR_MODEL, hairIndex);

        if (beardModels.Length != 0)
            ChangeBodyPartsModel(AppearanceDetails.BEARD_MODEL, beardIndex);

    }


    //Next And Previous Buttons For Changing Models
    #region Next And Previous Buttons For Changing Models
    public void NextEarsModel()
    {
        if (earsIndex < earModels.Length - 1)
            earsIndex++;
        else
            earsIndex = 0;

        ChangeBodyPartsModel(AppearanceDetails.EAR_MODEL, earsIndex);
    }


    public void PreviousEarsModel()
    {
        if (earsIndex > 0)
            earsIndex--;
        else
            earsIndex = earModels.Length - 1;

        ChangeBodyPartsModel(AppearanceDetails.EAR_MODEL, earsIndex);
    }


    public void NextHornsModel()
    {
        if (hornsIndex < hornsModels.Length - 1)
            hornsIndex++;
        else
            hornsIndex = 0;

        ChangeBodyPartsModel(AppearanceDetails.HORNS_MODEL, hornsIndex);
    }


    public void PreviousHornsModel()
    {
        if (hornsIndex > 0)
            hornsIndex--;
        else
            hornsIndex = hornsModels.Length - 1;

        ChangeBodyPartsModel(AppearanceDetails.HORNS_MODEL, hornsIndex);
    }



    public void NextTailsModel()
    {
        if (tailsIndex < tailsModels.Length - 1)
            tailsIndex++;
        else
            tailsIndex = 0;

        ChangeBodyPartsModel(AppearanceDetails.TAILS_MODEL, tailsIndex);
    }


    public void PreviousTailsModel()
    {
        if (tailsIndex > 0)
            tailsIndex--;
        else
            tailsIndex = tailsModels.Length - 1;

        ChangeBodyPartsModel(AppearanceDetails.TAILS_MODEL, tailsIndex);
    }


    public void NextHairModel()
    {
        if (hairIndex < hairModels.Length - 1)
            hairIndex++;
        else
            hairIndex = 0;

        ChangeBodyPartsModel(AppearanceDetails.HAIR_MODEL, hairIndex);
    }


    public void PreviousHairModel()
    {
        if (hairIndex > 0)
            hairIndex--;
        else
            hairIndex = hairModels.Length - 1;

        ChangeBodyPartsModel(AppearanceDetails.HAIR_MODEL, hairIndex);
    }


    public void NextBeardModel()
    {
        if (beardIndex < beardModels.Length - 1)
            beardIndex++;
        else
            beardIndex = 0;

        ChangeBodyPartsModel(AppearanceDetails.BEARD_MODEL, beardIndex);
    }


    public void PreviousBeardModel()
    {
        if (beardIndex > 0)
            beardIndex--;
        else
            beardIndex = beardModels.Length - 1;

        ChangeBodyPartsModel(AppearanceDetails.BEARD_MODEL, beardIndex);
    }
    #endregion



    void ChangeBodyPartsModel(AppearanceDetails detail, int id)
    { 
        switch (detail) //switch with enum variable for model types
        {
            case AppearanceDetails.EAR_MODEL:
                if (activeEars != null) //checks if there is an active object on the character and destroys it before setting a new model
                    GameObject.Destroy(activeEars);

                activeEars = GameObject.Instantiate(earModels[id]); //Creates a clone of the object inside the array and sets it as active
                activeEars.transform.SetParent(face); //set parent bone 
                activeEars.transform.ResetBodyPartsTransform(); //resets body parts model transform relative to the bone
                break;
           
            case AppearanceDetails.HORNS_MODEL:
                if (activeHorns != null)
                    GameObject.Destroy(activeHorns);

                activeHorns = GameObject.Instantiate(hornsModels[id]);
                activeHorns.transform.SetParent(face);
                activeHorns.transform.ResetBodyPartsTransform();
                break;
                
            case AppearanceDetails.TAILS_MODEL:
                if (activeTails != null)
                    GameObject.Destroy(activeTails);

                activeTails = GameObject.Instantiate(tailsModels[id]);
                activeTails.transform.SetParent(tailBone);
                activeTails.transform.ResetBodyPartsTransform();
                break;

            case AppearanceDetails.HAIR_MODEL:
                if (activeHair != null)
                    GameObject.Destroy(activeHair);

                activeHair = GameObject.Instantiate(hairModels[id]);
                activeHair.transform.SetParent(face);
                activeHair.transform.ResetBodyPartsTransform();
                break;

            case AppearanceDetails.BEARD_MODEL:
                if (activeBeard != null)
                    GameObject.Destroy(activeBeard);

                activeBeard = GameObject.Instantiate(beardModels[id]);
                activeBeard.transform.SetParent(face);
                activeBeard.transform.ResetBodyPartsTransform();
                break;

        }
    }

    //Function to save customized body parts appearance in dictionary for use in game scene
    public void SaveAppearance()
    {
        savedAppearance = new Dictionary<AppearanceDetails, int>();

        savedAppearance.Add(AppearanceDetails.EAR_MODEL, earsIndex);
        savedAppearance.Add(AppearanceDetails.HORNS_MODEL, hornsIndex);
        savedAppearance.Add(AppearanceDetails.TAILS_MODEL, tailsIndex);
        savedAppearance.Add(AppearanceDetails.HAIR_MODEL, hairIndex);
        savedAppearance.Add(AppearanceDetails.BEARD_MODEL, beardIndex);
    }


    //Function called from GameScene to load customizations in controller's model - Might need to change because it will only
    //work with already set mesh in controller, so i might need to find a way to change mesh for female model
    public void LoadAppearance()
    {
        //variable to set the saved appearance details in the game scene character because i use a different character model copy 
        //for the menu scenes (this one has generic rig to work with my animations) and a different copy for the gamescene (this one
        //has rig set to humanoid to work with 3rd person controller animations) and i don't want to mix up the face bone of the 2 copies.
        //The generic rig copy load into the next scenes by DontDestroyOnLoad() and eventually gets to the gamescene where after it saves
        //appearance details it destroys the generic rig copy and loads the saved details on the humanoid copy.
        //So the generic rig copy's face bone is Untagged and the humanoid rig copy's face bone has the GameSceneFaceBone tag that i created.
        //Same with Tail bone.
        newFace = GameObject.FindGameObjectWithTag("GameSceneFaceBone");
        newTailBone = GameObject.FindGameObjectWithTag("GameSceneTailBone");

        if (activeEars != null)
            GameObject.Destroy(activeEars);

        //Instantiate model using dictionary with saved details from the menu scenes
        activeEars = GameObject.Instantiate(earModels[savedAppearance[AppearanceDetails.EAR_MODEL]]);
        activeEars.transform.SetParent(newFace.transform);
        activeEars.transform.ResetBodyPartsTransform();


        if (activeHorns != null)
            GameObject.Destroy(activeHorns);
        if (hornsModels.Length != 0)
        {
            activeHorns = GameObject.Instantiate(hornsModels[savedAppearance[AppearanceDetails.HORNS_MODEL]]);
            activeHorns.transform.SetParent(newFace.transform);
            activeHorns.transform.ResetBodyPartsTransform();
        }


        if (activeTails != null)
            GameObject.Destroy(activeTails);
        if (tailsModels.Length != 0)
        {
            activeTails = GameObject.Instantiate(tailsModels[savedAppearance[AppearanceDetails.TAILS_MODEL]]);
            activeTails.transform.SetParent(newTailBone.transform);
            activeTails.transform.ResetBodyPartsTransform();
        }


        if (activeHair != null)
            GameObject.Destroy(activeHair);
        if (hairModels.Length != 0)
        {
            activeHair = GameObject.Instantiate(hairModels[savedAppearance[AppearanceDetails.HAIR_MODEL]]);
            activeHair.transform.SetParent(newFace.transform);
            activeHair.transform.ResetBodyPartsTransform();
        }


        if (activeBeard != null)
            GameObject.Destroy(activeBeard);
        if (beardModels.Length != 0)
        {
            activeBeard = GameObject.Instantiate(beardModels[savedAppearance[AppearanceDetails.BEARD_MODEL]]);
            activeBeard.transform.SetParent(newFace.transform);
            activeBeard.transform.ResetBodyPartsTransform();
        }

    }

    //function to hide hair when wearing helmet
    public void HideHairWhenWearingHelmet(int helmetID)
    {
        //if helmet ID is not 0, meaning that character wears helmet, hide helmet
        if (helmetID != 0)
            activeHair.SetActive(false);
        else
            activeHair.SetActive(true);
    }

}
