using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Es.InkPainter;

public class Ground : MonoBehaviour
{

    public NavMeshSurface surface;
    private MeshFilter meshFilter;
    private SkinnedMeshRenderer meshRenderer;
    private Mesh mesh;
    private MeshCollider meshCollider;
    private InkCanvas inkCanvas;
    private InkCanvas.PaintSet paintSet;
    private Vector3[] vertexes = new Vector3[10000];
    private int[] triangles = new int[58806];
    private Vector2[] uvs = new Vector2[10000];
    private Vector3[] normals = new Vector3[10000];
    // Start is called before the first frame update
    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        mesh = meshFilter.mesh;
        meshRenderer.sharedMesh = mesh;
        mesh.Clear();

        mesh.vertices = CreateVertexes(100);
        mesh.triangles = CreateTriangles();
        mesh.uv = CreateUVs();
        mesh.normals = CreateNormals();
        meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        gameObject.AddInkCanvas(new InkCanvas.PaintSet());    
        
    }

    private void Start()
    {
        //surface.BuildNavMesh();
        NavMeshData navData  = new NavMeshData();
        surface.UpdateNavMesh(navData);
    }

    private Vector3[] CreateVertexes(int arr_num) {

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                vertexes[arr_num * i + j].x = j * 0.2f;
                vertexes[arr_num * i + j].y = 0;
                vertexes[arr_num * i + j].z = i * 0.2f;
            }
        }

        return vertexes;
    }

    private int[] CreateTriangles() {
        int e = 0;
        for (int i = 0; i < 9899; i++)
        {
            if (i % 100 != 99) {
                triangles[e] = i;
                triangles[e + 1] = i + 100;
                triangles[e + 2] = i + 101;
                triangles[e + 3] = i + 101;
                triangles[e + 4] = i + 1;
                triangles[e + 5] = i;
                e += 6;
            }
        }
        return triangles;
    }

    private Vector2[] CreateUVs() {
        float u = 0f;
        float v = -0.01f;
        for (int i = 0; i < uvs.Length; i++)
        {

            
            if (i % 100 != 0)
            {
                u += 0.01f;
            }
            else
            {
                u = 0;
                v += 0.01f;
            }
            uvs[i].y = v;
            uvs[i].x = u;
        }
        return uvs;
    }

    private Vector3[] CreateNormals() {
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = Vector3.up;
        }
        return normals;
    }
}
