using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SkinnedMeshToMesh : MonoBehaviour
{
    public SkinnedMeshRenderer skmr;
    public VisualEffect VFXGraph;
    public float refreshRate = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateVFXGraph()); //call IEnumerator function using StartCoroutine
    }

    IEnumerator UpdateVFXGraph()
    {
        //while enemy gameobject is active
        while(gameObject.activeSelf) 
        {
            Mesh mesh = new Mesh();
            skmr.BakeMesh(mesh); //Bake a snapshot of skmr to mesh

            //get vertices of mesh to set as "mesh" positions for particles
            Vector3[] vertices = mesh.vertices; 
            Mesh mesh2 = new Mesh();
            mesh2.vertices = vertices;

            VFXGraph.SetMesh("Mesh", mesh2); //set the mesh of particles as this

            yield return new WaitForSeconds (refreshRate); //suspend execution 
        }
    }
}
