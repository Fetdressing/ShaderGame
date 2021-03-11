using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalsMerger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnValidate()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
            for (int i = 0; i < meshFilters.Length; i++)
            {
                if (meshFilters[i].sharedMesh != null)
                {
                    Vector3[] positions = meshFilters[i].sharedMesh.vertices;
                    Vector3[] normals = meshFilters[i].sharedMesh.normals;

                    for (int y = 0; y < normals.Length; y++)
                    {
                        normals[y] = (this.transform.localPosition - positions[y]).normalized;
                    }

                    // assign the array of normals to the mesh
                    meshFilters[i].sharedMesh.normals = normals;
                }
            }
        }
    }
}
