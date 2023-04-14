using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondaryCustomizationHelper : MonoBehaviour
{
    //This is a helper script inserted into the secondaryCustomizationHelper game object, in order to call functions from
    //the main characterCustomization, because the main required to insert objects in the fields and i couldn't do that in
    //the next scenes because the object (ie character model), is added in the scene later via script so there were no
    //objects to add in the fields yet.

    private GameObject characterCustomization;


    private void Awake()
    {
        if (characterCustomization == null)
        {
            characterCustomization = GameObject.Find("CharacterCustomization"); //Get CharacterCustomization object from scene
        }
    }


    #region CustomizeCharacterParts Functions
    public void NextEarsModel()
    { 
        characterCustomization.GetComponent<CustomizeCharacterParts>().NextEarsModel();
    }

    public void PreviousEarsModel()
    {
        characterCustomization.GetComponent<CustomizeCharacterParts>().PreviousEarsModel();
    }


    public void NextHornsModel()
    {
        characterCustomization.GetComponent<CustomizeCharacterParts>().NextHornsModel();
    }

    public void PreviousHornsModel()
    {
        characterCustomization.GetComponent<CustomizeCharacterParts>().PreviousHornsModel();
    }


    public void NextTailsModel()
    {
        characterCustomization.GetComponent<CustomizeCharacterParts>().NextTailsModel();
    }

    public void PreviousTailsModel()
    {
        characterCustomization.GetComponent<CustomizeCharacterParts>().PreviousTailsModel();
    }


    public void NextHairModel()
    {
        characterCustomization.GetComponent<CustomizeCharacterParts>().NextHairModel();
    }

    public void PreviousHairModel()
    {
        characterCustomization.GetComponent<CustomizeCharacterParts>().PreviousHairModel();
    }


    public void NextBeardModel()
    {
        characterCustomization.GetComponent<CustomizeCharacterParts>().NextBeardModel();
    }

    public void PreviousBeardModel()
    {
        characterCustomization.GetComponent<CustomizeCharacterParts>().PreviousBeardModel();
    }



    public void changeSkinColor(int colorIndex)
    {
        characterCustomization.GetComponent<CustomizeCharacterColors>().changeSkinColor(colorIndex);
    }


    public void changeIrisColor(int colorIndex)
    {
        characterCustomization.GetComponent<CustomizeCharacterColors>().changeIrisColor(colorIndex);
    }


    public void changeHairColor(int colorIndex)
    {
        characterCustomization.GetComponent<CustomizeCharacterColors>().changeHairColor(colorIndex);
    }
    #endregion


    #region SwitchArmorEquipment Functions
    public void NextHeadArmorPiece()
    {
        characterCustomization.GetComponent<SwitchArmorEquipment>().NextHeadArmor();
    }

    public void PreviousHeadArmorPiece()
    {
        characterCustomization.GetComponent<SwitchArmorEquipment>().PreviousHeadArmor();
    }


    public void NextChestArmorPiece()
    {
        characterCustomization.GetComponent<SwitchArmorEquipment>().NextChestArmor();
    }

    public void PreviousChestArmorPiece()
    {
        characterCustomization.GetComponent<SwitchArmorEquipment>().PreviousChestArmor();
    }


    public void NextHandsArmorPiece()
    {
        characterCustomization.GetComponent<SwitchArmorEquipment>().NextHandsArmor();
    }

    public void PreviousHandsArmorPiece()
    {
        characterCustomization.GetComponent<SwitchArmorEquipment>().PreviousHandsArmor();
    }


    public void NextLegsArmorPiece()
    {
        characterCustomization.GetComponent<SwitchArmorEquipment>().NextLegsArmor();
    }

    public void PreviousLegsArmorPiece()
    {
        characterCustomization.GetComponent<SwitchArmorEquipment>().PreviousLegsArmor();
    }


    public void NextFeetArmorPiece()
    {
        characterCustomization.GetComponent<SwitchArmorEquipment>().NextFeetArmor();
    }

    public void PreviousFeetArmorPiece()
    {
        characterCustomization.GetComponent<SwitchArmorEquipment>().PreviousFeetArmor();
    }

    #endregion

    #region Restart Or Quit Game
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }
    #endregion

}
