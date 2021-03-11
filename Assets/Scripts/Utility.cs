using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Utility : MonoBehaviour
{
    public static Mesh CopyMesh(Mesh toCopy, string name, string folder = "Assets/Meshes")
    {
        Mesh newmesh = new Mesh();
        newmesh.vertices = toCopy.vertices;
        newmesh.triangles = toCopy.triangles;
        newmesh.uv = toCopy.uv;
        newmesh.normals = toCopy.normals;
        newmesh.colors = toCopy.colors;
        newmesh.tangents = toCopy.tangents;
        AssetDatabase.CreateAsset(newmesh, folder + "/" + name);
        return newmesh;
    }
}
