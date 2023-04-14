using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//Automatically adds component Slider to GameObject
[RequireComponent(typeof(Slider))]
public class BlendShapeSlider : MonoBehaviour
{
        //Do not need suffix
    [Header("Do not include the suffixes of the BlendShape Name")] //Displays message inside inspector
    public string blendShapeName;
    private Slider slider;


    private void Start()
    {
        blendShapeName = blendShapeName.Trim(); //Trims spaces at start or end of name that might exist
        slider = GetComponent<Slider>();

        //When slider is moved, then call function based on the blendshape name and pass float of slider
        //Gets Instance property from singleton
        //the => operator is an expression bodied member
        slider.onValueChanged.AddListener(value => CharacterCustomization.Instance.ChangeBlendshapeValue(blendShapeName, value));
    }

} 