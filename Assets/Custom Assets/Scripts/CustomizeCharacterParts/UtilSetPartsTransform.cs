using UnityEngine;

//Script to set the default position of body parts relative to a parent bone, which is considered as a transform fuction
//so it doesn't need to be placed inside any game object, you just call the fuctions from "gameobject.transform. ".
//NOTE: I set the location of the models inside the scene by creating an empty gameobject which i called "pivot", as a child
//of the parent bone and i placed it at 0 position, 0 rotation and scale 1. Then placed each model individually inside the scene
//at the location i wanted on the character and then placed each model as a child of that pivot and created a prefab of each
//model with the pivot. So each model has different transforms, relative to the pivot but they all have the same parent pivot so i can just
//universally change the pivot location to the same values (positon 0, rotation 0, scale 1) and the objects go to the right place, without
//the need to set each models position separately through script.
public static class UtilSetPartsTransform
{

    public static void ResetBodyPartsTransform(this Transform _transform)
    {
        _transform.localPosition = Vector3.zero; //Sets position to zero
        _transform.localEulerAngles = Vector3.zero; //Sets rotation to zero
        _transform.localScale = Vector3.one; //Sets scale to one
    }

}
