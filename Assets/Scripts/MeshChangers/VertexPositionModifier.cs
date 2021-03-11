using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexPositionModifier : MonoBehaviour
{
    [SerializeField]
    private int uniqueID = 0;

    [SerializeField]
    private float yMin = 0;

    [SerializeField]
    private float yMax = 10;

    [SerializeField]
    private AnimationCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnValidate()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();

            if (meshFilter.sharedMesh != null)
            {
                Vector3[] positions = meshFilter.sharedMesh.vertices;
                Vector3[] normals = meshFilter.sharedMesh.normals;

                for (int y = 0; y < positions.Length; y++)
                {
                    float yValue = positions[y].y;
                    float norValue = (yValue - yMin) / (yMax - yMin);
                    float clampedNorValue = Mathf.Clamp(norValue, 0, 1);
                    float value = curve.Evaluate(clampedNorValue);
                    positions[y] = normals[y] * value;
                }

                Mesh meshToSet;
                if (meshFilter.sharedMesh.name.EndsWith(uniqueID.ToString()))
                {
                    meshToSet = meshFilter.sharedMesh;
                }
                else
                {
                    meshToSet = Utility.CopyMesh(meshFilter.sharedMesh, "Mesh" + uniqueID);
                }

                // assign the array of normals to the mesh
                meshToSet.vertices = positions;
                meshFilter.sharedMesh = meshToSet;
            }
        }
    }
}
