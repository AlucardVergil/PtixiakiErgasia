using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    //public static ChangeScenes instance;

    //SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName("HumanRaceSelectionScene"));

    private GameObject characterCustomization;


    private void Awake()
    {
        if (characterCustomization == null)
        {
            characterCustomization = GameObject.Find("CharacterCustomization"); //Get CharacterCustomization object from scene
        }
    }

    public void LoadGameScene()
    {
        //Save appearance details and armor selection, chosen in the menu scenes, to the dictionaries before moving to the game scene
        //NOTE: for blendshapes the dictionary values are saved as soon as the sliders change values, in the ChangeBlendshapeValue
        //function in CharacterCustomization class
        characterCustomization.GetComponent<CustomizeCharacterParts>().SaveAppearance();
        characterCustomization.GetComponent<SwitchArmorEquipment>().SaveArmor();
        SceneManager.LoadScene("GameScene");
    }


    #region Functions to load Race Selection Scenes
    //Functions to load Race Selection Scenes
    public void LoadHumanRaceSelectionScene()
    {
        SceneManager.LoadScene("HumanRaceSelectionScene");  
    }


    public void LoadElfRaceSelectionScene()
    {
        SceneManager.LoadScene("ElfRaceSelectionScene");
    }


    public void LoadDemonRaceSelectionScene()
    {
        SceneManager.LoadScene("DemonRaceSelectionScene");
    }


    public void LoadBeastmenRaceSelectionScene()
    {
        SceneManager.LoadScene("BeastmenRaceSelectionScene");
    }


    public void LoadFishmenRaceSelectionScene()
    {
        SceneManager.LoadScene("FishmenRaceSelectionScene");
    }
    #endregion

    #region Functions to load Body Parts Customization Scenes and Keep Character Customization Game Object and Character Model to next scene
    //Functions to load Body Parts Customization Scenes and Keep Character Customization Game Object and Character Model to next scene
    public void LoadHumanCustomizationBodyPartsScene()
    {
        DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        DontDestroyOnLoad(GameObject.Find("human_male_model_14"));        
        SceneManager.LoadScene("HumanCustomizationBodyPartsScene");
    }


    public void LoadElfCustomizationBodyPartsScene()
    {
        DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        DontDestroyOnLoad(GameObject.Find("human_male_model_14"));
        SceneManager.LoadScene("ElfCustomizationBodyPartsScene");
    }


    public void LoadDemonCustomizationBodyPartsScene()
    {
        DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        DontDestroyOnLoad(GameObject.Find("human_male_model_14"));
        SceneManager.LoadScene("DemonCustomizationBodyPartsScene");
    }


    public void LoadBeastmenCustomizationBodyPartsScene()
    {
        DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        DontDestroyOnLoad(GameObject.Find("human_male_model_14"));
        SceneManager.LoadScene("BeastmenCustomizationBodyPartsScene");
    }


    public void LoadFishmenCustomizationBodyPartsScene()
    {
        DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        DontDestroyOnLoad(GameObject.Find("human_male_model_14"));
        SceneManager.LoadScene("FishmenCustomizationBodyPartsScene");
    }
    #endregion

    #region Functions to load Blendshapes Customization Scenes and Keep Character Customization Game Object and Character Model to next scene
    //Functions to load Blendshapes Customization Scenes and Keep Character Customization Game Object and Character Model to next scene
    //NOTE! no need to add DontDestroyOnLoad again here because the first time it is used on CustomizationBodyPartsScenes it stays on
    //next scenes from then on
    public void LoadHumanCustomizationBlendshapesScene()
    {
        //DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        //DontDestroyOnLoad(GameObject.Find("human_male_model_14"));
        SceneManager.LoadScene("HumanCustomizationBlendshapesScene");
    }


    public void LoadElfCustomizationBlendshapesScene()
    {
        //DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        //DontDestroyOnLoad(GameObject.Find("human_male_model_14"));
        SceneManager.LoadScene("ElfCustomizationBlendshapesScene");
    }


    public void LoadDemonCustomizationBlendshapesScene()
    {
        //DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        //DontDestroyOnLoad(GameObject.Find("human_male_model_14"));
        SceneManager.LoadScene("DemonCustomizationBlendshapesScene");
    }


    public void LoadBeastmenCustomizationBlendshapesScene()
    {
        //DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        //DontDestroyOnLoad(GameObject.Find("human_male_model_14"));
        SceneManager.LoadScene("BeastmenCustomizationBlendshapesScene");
    }


    public void LoadFishmenCustomizationBlendshapesScene()
    {
        //DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        //DontDestroyOnLoad(GameObject.Find("human_male_model_14"));
        SceneManager.LoadScene("FishmenCustomizationBlendshapesScene");
    }
    #endregion

    #region Functions to load Armor Equipment Customization Scenes and Keep Character Customization Game Object and Character Model to next scene
    //Functions to load Armor Equipment Customization Scenes and Keep Character Customization Game Object and Character Model to next scene
    //NOTE! no need to add DontDestroyOnLoad again here because the first time it is used on CustomizationBodyPartsScenes it stays on next scenes from then on
    public void LoadHumanArmorEquipmentScene()
    {
        //DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        //DontDestroyOnLoad(GameObject.Find("human_male_model_14"));
        SceneManager.LoadScene("HumanArmorEquipmentScene");
    }


    public void LoadElfArmorEquipmentScene()
    {
        //DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        //DontDestroyOnLoad(GameObject.Find("human_male_model_14"));
        SceneManager.LoadScene("ElfArmorEquipmentScene");
    }


    public void LoadDemonArmorEquipmentScene()
    {
        //DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        //DontDestroyOnLoad(GameObject.Find("human_male_model_14"));
        SceneManager.LoadScene("DemonArmorEquipmentScene");
    }


    public void LoadBeastmenArmorEquipmentScene()
    {
        //DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        //DontDestroyOnLoad(GameObject.Find("human_male_model_14"));
        SceneManager.LoadScene("BeastmenArmorEquipmentScene");
    }


    public void LoadFishmenArmorEquipmentScene()
    {
        //DontDestroyOnLoad(GameObject.Find("CharacterCustomization"));
        //DontDestroyOnLoad(GameObject.Find("human_male_model_14"));
        SceneManager.LoadScene("FishmenArmorEquipmentScene");
    }
    #endregion
}
