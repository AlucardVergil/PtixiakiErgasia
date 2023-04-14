using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DissolvingController : MonoBehaviour
{
    public Animator animator;
    public SkinnedMeshRenderer[] skmrArray;
    public VisualEffect VFXGraph;
    public float dissolveRate = 0.02f;
    public float refreshRate = 0.05f;

    private List<Material> dissolveMaterialsList;
    

    // Start is called before the first frame update
    void Start()
    {
        //stop particles effects 
        if (VFXGraph != null)
        {
            VFXGraph.Stop(); //stop particles effects
            VFXGraph.gameObject.SetActive(false);
        }

        dissolveMaterialsList = new List<Material>();

        //Save all materials inside every skinnedMesh of the enemy on list
        for (int i = 0; i < skmrArray.Length; i++)
        {
            foreach (Material mat in skmrArray[i].materials)            
                dissolveMaterialsList.Add(mat);                
        }
    }

    //function in order to use yield to suspend execution for given seconds
    public IEnumerator Dissolve()
    {
        if (animator != null)
            animator.SetTrigger("die"); //trigger death animation
        
        yield return new WaitForSeconds(0.2f); //suspend execution for 0.2 secs

        //Activate particles and play them
        if (VFXGraph != null)
        {
            VFXGraph.gameObject.SetActive(true);
            VFXGraph.Play();
        }

        float counter = 0;
       

        if (dissolveMaterialsList.Count > 0)
        {
            //check the first material's dissolve since every material dissolves together
            while (dissolveMaterialsList[0].GetFloat("_DissolveAmount") < 1) //Get the _DissolveAmount which is the reference not the name inside the shader graph
            {
                counter += dissolveRate; //increase the the shader dissolve based on the rate

                for (int i = 0; i < dissolveMaterialsList.Count; i++)
                    dissolveMaterialsList[i].SetFloat("_DissolveAmount", counter);

                yield return new WaitForSeconds(refreshRate); //suspend execution so it doesn't dissolve instantly
            }
        }

        Destroy(gameObject, 1); //destroy enemy after 1 sec
    }

}