using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fourier : MonoBehaviour
{
    //Set Ups
    private float t = 0;
    public Material lineMaterial;
    private GameObject newDrawer;
    private int cycle;
    public Vector3 startingVector;

    //Settings
    public int vertCount;
    public float deltaTheta;
    public float radius;
    public float inc = 0.1f;
    public int itemLength;

    //What to Draw
    public Vector3[] displacement;
    public float[] period;

    public GameObject[] Point;
    public GameObject[] Circle;
    public GameObject Line;
    public GameObject displayLine;

    private LineRenderer lineRenderer;
    private LineRenderer circleRenderer;
    private LineRenderer imageRenderer;

    //Fourier function
    float[] x = {100f,100f,100f,-100f,-100f,-100f,100f,100f,100f,-100f,-100f,-100f};
    public Vector2[] X;
    public Vector3[] XParam;

    private Vector3[] display;

    void Start()
    {
        //Settings
        vertCount = 40;
        deltaTheta = (2 * Mathf.PI) / vertCount;
        radius = 5;
        inc = 0.1f;
        itemLength = 5;

        //set array lengths
        x = new float[itemLength];
        X = new Vector2[itemLength];
        XParam = new Vector3[itemLength];

        displacement = new Vector3[itemLength];
        period = new float[itemLength];

        Point = new GameObject[itemLength];
        Circle = new GameObject[itemLength];

        Create_Everything();

        //generate points

        for (int k = 0; k < itemLength; k++)
        {
            float re = 0;
            float im = 0;

            for (int n = 0; n < itemLength; n++)
            {
                float phi = (Mathf.PI * 2 * k * n) / itemLength;
                re += x[n] * Mathf.Cos(phi);
                im -= x[n] * Mathf.Sin(phi);
            }

            re = re / itemLength;
            im = im / itemLength;

            int freq = k;
            float amp = Mathf.Sqrt(re * re + im * im);
            float phase = Mathf.Atan(im / re);

            X[k] = new Vector2(re, im);
            XParam[k] = new Vector3(freq, amp, phase);
        }


        for (int i = 0; i < 12; i++)
        {
         //   Debug.Log(x[i]);
        //    Debug.Log(X[i]);

        }

        //Draw Image
      //  CalcStartingVector();
     //   displayLine.transform.position = startingVector;
     //   StartCoroutine(Image());

    }


    void CalcStartingVector()
    {
        float x = 0;
        float y = 0;

        for (int i = 0; i < itemLength; i++)
        {

            float freq = XParam[i].x;
            float newRadius = radius * XParam[i].y;
            float phase = XParam[i].z;

            x += newRadius * Mathf.Cos(freq * t + phase + Mathf.PI * 0.5f);
            y += newRadius * Mathf.Sin(freq * t + phase + Mathf.PI * 0.5f);

        }

        startingVector = new Vector3(x,y,0f);

    }

    void Update()
    {
        UpdateTime();
        UpdateEverything();
    }

    void Create_Everything()
    {
        //for line
        newDrawer = new GameObject("Line");
        lineRenderer = newDrawer.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.numCornerVertices = 30;
        lineRenderer.numCapVertices = 30;

        lineRenderer.useWorldSpace = false;
        lineRenderer.material.SetColor("_Color", Color.blue);

        for (int i = 0; i < itemLength - 1; i++)
        {
            lineRenderer.positionCount++;
        }

        //for displayline
        displayLine = new GameObject("displayLine");
        imageRenderer = displayLine.AddComponent<LineRenderer>();
        imageRenderer.material = lineMaterial;
        imageRenderer.numCornerVertices = 30;
        imageRenderer.numCapVertices = 30;

        imageRenderer.useWorldSpace = false;
        imageRenderer.material.SetColor("_Color", Color.yellow);


        for (int i = 0; i < itemLength - 1; i++)
        {
            imageRenderer.positionCount++;
        }

        //for POINTS & CIRCLES
        for (cycle = 0; cycle < itemLength; cycle++)
        {

            CreateDrawer("Point", Point, Color.white);
            CreateDrawer("Circle", Circle, Color.magenta);
            circleRenderer = Circle[cycle].GetComponent<LineRenderer>();

            for (int i = 0; i < vertCount - 1; i++)
            {
                circleRenderer.positionCount++;
            }
        }

    }

    void UpdateEverything()
    {
        float x = 0;
        float y = 0;
        Vector3 pos;

        for (int i = 0; i < itemLength; i++)
        {
            Vector3 prevPos = new Vector3(x, y, 0);
            float localx;
            float localy;

            float freq = XParam[i].x;
            float newRadius = radius * XParam[i].y;
            float phase = XParam[i].z;

            //definitions for circle
            circleRenderer = Circle[i].GetComponent<LineRenderer>();
            float theta = 0;

            //Generate "pos"
            localx = newRadius * Mathf.Cos(freq * t + phase + Mathf.PI * 0.5f);
            localy = newRadius * Mathf.Sin(freq * t + phase + Mathf.PI * 0.5f);
            x += localx;
            y += localy;

            pos = new Vector3(x, y, 0);




            //LINE and POINT
            Point[i].transform.position = pos;
            Circle[i].transform.position = prevPos + new Vector3(newRadius, 0f, 0f);
            lineRenderer.SetPosition(i + 1, pos);

            //assign and generate Points
            for (int j = 0; j < vertCount + 1; j++)
            {

                localx = newRadius * Mathf.Cos(theta) - newRadius;
                localy = newRadius * Mathf.Sin(theta);

                pos = new Vector3(localx, localy, 0);
                circleRenderer.SetPosition(j, pos);

                theta += deltaTheta;
            }
        }

    }

    //basically useless

    void UpdateLine(float radius, Vector3 displacement, float period, GameObject Line)
    {
        float x;
        float y;
        Vector3 pos;

        x = radius * Mathf.Cos(2 * t * Mathf.PI / period);
        y = radius * Mathf.Sin(2 * t * Mathf.PI / period);

        pos = new Vector3(x, y, 0);

        //  LineRenderer line = Line.GetComponent<LineRenderer>();
        //    line.SetPosition(1, pos);


    }

    void CreatePoint()
    {
        CreateDrawer("Point", Point, Color.white);

    }

    void UpdatePoint(float radius, Vector3 displacement, float period, GameObject Point)
    {
        //  line.positionCount++;
        float x;
        float y;
        float z;
        Vector3 pos;

        x = radius * Mathf.Cos(2 * t * Mathf.PI / period);
        y = radius * Mathf.Sin(2 * t * Mathf.PI / period);
        z = displacement.z;

        pos = new Vector3(x, y, z);

        Point.transform.position = pos;


    }

    void CreateXCircle(float radius, Vector3 displacement, int vertCount)
    {
        CreateDrawer("Circle", Circle, Color.magenta);


        //Initial values for newDrawer
        newDrawer.transform.position += new Vector3(radius, 0, 0);

        float deltaTheta = (2 * Mathf.PI) / vertCount;
        float theta = 0;

        float x;
        float y;
        Vector3 pos;

        //create Points
        for (int i = 0; i < vertCount - 1; i++)
        {

            //   line.positionCount++;
        }

        //assign and generate Points
        for (int i = 0; i < vertCount + 1; i++)
        {

            x = radius * Mathf.Cos(theta) - radius;
            y = radius * Mathf.Sin(theta);

            pos = new Vector3(x, y, 0);
            //   line.SetPosition(i, pos);

            theta += deltaTheta;
        }

    }

    void UpdateTime()
    {
        float dt = Mathf.PI * 2 / itemLength;
        t += Mathf.PI * 4 / itemLength;
    }

    void CreateDrawer(string name, GameObject[] type, Color color)
    {
        //Set Up new game obj
        newDrawer = new GameObject(name + cycle);
        LineRenderer Renderer = newDrawer.AddComponent<LineRenderer>();
        Renderer.material = lineMaterial;
        Renderer.numCornerVertices = 30;
        Renderer.numCapVertices = 30;

        //Set Up Initial Values of newDrawer
        Renderer.useWorldSpace = false;
        type[cycle] = newDrawer;
        Renderer.material.SetColor("_Color", color);
    }

    void FourierTransform(float[] x, Vector2[] X, Vector3[] XParam)
    {
        for (int k = 0; k < itemLength; k++)
        {
            float re = 0;
            float im = 0;

            for (int n = 0; n < itemLength; n++)
            {
                float phi = (Mathf.PI * 2 * k * n) / itemLength;
                re += x[n] * Mathf.Cos(phi);
                im -= x[n] * Mathf.Sin(phi);
            }

            re = re / itemLength;
            im = im / itemLength;

            int freq = k;
            float amp = Mathf.Sqrt(re * re + im * im);
            float phase = Mathf.Atan(im / re);

            X[k] = new Vector2(re, im);
            XParam[k] = new Vector3(freq, amp, phase);
        }

    }

    IEnumerator Image()
    {
        yield return new WaitForSeconds(inc);

        //calc new point
        float y = Point[itemLength - 1].transform.position.y;
        Vector3 pos = new Vector3(0f, y, 0f) - startingVector;

        //generate point
        imageRenderer.SetPosition(imageRenderer.positionCount - 1, pos);
        imageRenderer.positionCount++;


        StartCoroutine(Image());

    }

}




