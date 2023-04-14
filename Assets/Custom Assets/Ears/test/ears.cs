using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ears : MonoBehaviour
{

    public MeshRenderer ear;
    public MeshRenderer[] earArray;


    public void changeEars(int earsIndex)
    {
        ear.GetComponent<MeshFilter>().sharedMesh = earArray[earsIndex].GetComponent<MeshFilter>().sharedMesh;
    }
    
       
    
}
