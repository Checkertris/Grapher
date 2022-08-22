using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mesh_render : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    float t;


    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();


    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        //repeat no. of vertices times, z increases for every z loop, x increases for every x loop and resets after 1 row, i increases for every single point 
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.Sin(z);
                vertices[i] = new Vector3(x, y, z);
                i++;
            }

        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {

            for (int x = 0; x < zSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;

            }

            vert++;

        }

    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

    }

    private void OnDrawGizmos()
    {

        //draws sphere
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }


    void Update()
    {

        for (int i = 0, z = 0; z <= zSize; z++)
        { 

            for (int x = 0; x <= xSize; x++)
            {

                float y = Mathf.Sin(z + t);
                vertices[i] = new Vector3(x, y, z);
                i++;
            }

            t += 0.001f;
        }

        UpdateMesh();

    }


}
