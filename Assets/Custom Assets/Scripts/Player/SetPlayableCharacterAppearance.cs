using UnityEngine;
using Unity.Netcode;

public class SetPlayableCharacterAppearance : NetworkBehaviour
{

    GameObject characterCustomization;

    [Header("Skinned Mesh Renderer for player model head")]
    [SerializeField] private SkinnedMeshRenderer skmr;

    [SerializeField] private GameObject gameSceneFaceBone;
    [SerializeField] private GameObject gameSceneTailBone; 


    private void Awake()
    {
        characterCustomization = GameObject.FindGameObjectWithTag("CharacterCustomization");
        characterCustomization.GetComponent<CustomizeCharacterParts>().SetNewFaceAndNewTailBone(gameSceneFaceBone, gameSceneTailBone);
    }


    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner) return; // For NetworkBehaviour

        //Load Character Customized Body Parts
        characterCustomization.GetComponent<CustomizeCharacterParts>().LoadAppearance();

        //Load Character Customized Blendshapes
        //skmr = GameObject.Find("Male_model_head_2").GetComponent<SkinnedMeshRenderer>();
        CharacterCustomization.Instance.LoadBlendshapesToCharacter(skmr);

        //Load Armor Pieces
        characterCustomization.GetComponent<SwitchArmorEquipment>().LoadArmor();



        Destroy(GameObject.Find("human_male_model_14")); //Destroy model transfered from customization screen



    }

}

