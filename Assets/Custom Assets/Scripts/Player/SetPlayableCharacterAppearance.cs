using UnityEngine;


public class SetPlayableCharacterAppearance : MonoBehaviour
{

    GameObject characterCustomization;

    private SkinnedMeshRenderer skmr;



    // Start is called before the first frame update
    void Start()
    {
        //Load Character Customized Body Parts
        characterCustomization = GameObject.Find("CharacterCustomization");
        characterCustomization.GetComponent<CustomizeCharacterParts>().LoadAppearance();

        //Load Character Customized Blendshapes
        skmr = GameObject.Find("Male_model_head_2").GetComponent<SkinnedMeshRenderer>();
        CharacterCustomization.Instance.LoadBlendshapesToCharacter(skmr);

        //Load Armor Pieces
        characterCustomization.GetComponent<SwitchArmorEquipment>().LoadArmor();



        Destroy(GameObject.Find("human_male_model_14")); //Destroy model transfered from customization screen



    }

}

