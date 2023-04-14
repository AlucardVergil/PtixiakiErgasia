using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeCharacterColors : MonoBehaviour
{
    //NOTE: i duplicated the materials outside of the models and set the materials
    //of the models to the duplicate copy so that i can
    //change for example the skin material once and both ears and character
    //model will use the same skin material
    public Color[] skinColors; //array that holds all the skin colors to use 
    public Material skinMaterial; //skin material to use the colors on
    public Material earMaterial; //ear material to use the colors on

    public Color[] irisColors; //array that holds all the iris colors to use 
    public Material irisMaterial; //iris material to use the colors on

    public Color[] hairColors; //array that holds all the hair colors to use 
    public Material hairMaterial; //hair material to use the colors on



    public void changeSkinColor(int colorIndex)
    {
        skinMaterial.color = skinColors[colorIndex];
        earMaterial.color = skinColors[colorIndex];
    }

    public void changeIrisColor(int colorIndex)
    {
        irisMaterial.color = irisColors[colorIndex];
    }

    public void changeHairColor(int colorIndex)
    {
        hairMaterial.color = hairColors[colorIndex];
    }


}
