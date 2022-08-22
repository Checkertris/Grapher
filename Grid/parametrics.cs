using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parametrics : MonoBehaviour
{
    public Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    int vSize = 100;
    int uSize = 100;
    public string UserInput;
    float[] U;
    float[] V;

    //for GeneratePoints
    int nPoints;
    float[] xPoint;
    float[] zPoint;
    public GameObject cylinder;
    public GameObject cylinder2;

    //calc total distance
    float[] heights;
    float averageDistance;



    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //ConvertUserInput();

        GenerateUV();
        CreateShape();
        UpdateMesh();

       GenerateRandomPoints();
        //GenerateCustomPoints();
    }

    void FindMax()

    { 

       Debug.Log("Minimum distance: " + Mathf.Min(heights));

    }

    void GenerateCustomPoints()
    {
        nPoints = 3;
        xPoint = new float[nPoints];
        zPoint = new float[nPoints];

        float x = 0;
        float z = 0;

        xPoint[0] = 50;
        zPoint[0] = 0;
        Instantiate(cylinder, new Vector3(xPoint[0], 0, zPoint[0]), Quaternion.identity);

        xPoint[1] = 0;
        zPoint[1] = 50;
        Instantiate(cylinder, new Vector3(xPoint[1], 0, zPoint[1]), Quaternion.identity);

        xPoint[2] = 0;
        zPoint[2] = 0;
        Instantiate(cylinder, new Vector3(xPoint[2], 0, zPoint[2]), Quaternion.identity);


        for (int i = 0; i <= nPoints - 1; i++)
        {
        
            x += xPoint[i];
            z += zPoint[i];

        }

        x = x / (nPoints);
        z = z / (nPoints);

        Instantiate(cylinder2, new Vector3(x, 0, z), Quaternion.identity);

    }


    void GenerateRandomPoints()
    {
        nPoints = 3;
        xPoint = new float[nPoints];
        zPoint = new float[nPoints];

        float x = 0;
        float z = 0;
 

        for (int i = 0; i <= nPoints-1; i++)
        {
            xPoint[i] = Random.Range(0, vSize);
            zPoint[i] = Random.Range(0, uSize);
            Instantiate(cylinder, new Vector3(xPoint[i], 0, zPoint[i]), Quaternion.identity);

            x += xPoint[i];
            z += zPoint[i];

        }

        x = x / (nPoints);
        z = z / (nPoints);

        Instantiate(cylinder2, new Vector3(x, 0, z), Quaternion.identity);






        averageDistance = 0f;

        for (int i = 0; i <= nPoints - 1; i++)
        {
            averageDistance += Mathf.Sqrt(Mathf.Pow(xPoint[i] - x, 2) + Mathf.Pow(zPoint[i] - z, 2));
        }

        Debug.Log("total distance from average point: " + averageDistance);


    }

 

    void Update()
    {
        CreateShape();
        UpdateMesh();
        FindMax();
    }

    void GenerateUV()
    {
        U = new float[(vSize + 1) * (uSize + 1)];
        V = new float[(vSize + 1) * (uSize + 1)];

        for (int i = 0, z = 0; z <= uSize; z++)
        {
            for (int x = 0; x <= vSize; x++)
            {
                U[i] = z;
                V[i] = x;
                i++;
            }

        }

    }

    void ConvertUserInput()
    {

        UserInput = UserInput.Replace("v", "V[i]");
        UserInput = UserInput.Replace("u", "U[i]");
        UserInput = UserInput.Replace("sin", "Mathf.Sin");
        UserInput = UserInput.Replace("cos", "Mathf.Cos");
        UserInput = UserInput.Replace("tan", "Mathf.Tan");


    }


    void CreateShape()
    {
        vertices = new Vector3[(vSize + 1) * (uSize + 1)];
        heights = new float[(vSize + 1) * (uSize + 1)];

        for (int i = 0, z = 0; z <= uSize; z++)
        {
            for (int x = 0; x <= vSize; x++)
            {
                float y = 0;

              for (int a = 0; a <= nPoints-1; a++)
                {
                    y += Mathf.Sqrt(Mathf.Pow(xPoint[a]-V[i],2) + Mathf.Pow(zPoint[a] - U[i], 2));
               }

                vertices[i] = new Vector3(V[i], -y, U[i]);
                heights[i] = y;
                i++;
            }

        }

        triangles = new int[vSize * uSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < uSize; z++)
        {

            for (int x = 0; x < uSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + vSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + vSize + 1;
                triangles[tris + 5] = vert + vSize + 2;

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

//    private void OnDrawGizmos()
 //   {

  //      for (int i = 0; i < vertices.Length; i++)
   //     {
        //    Gizmos.DrawSphere(vertices[i], 0.1f);
      //  }
//    }

}
