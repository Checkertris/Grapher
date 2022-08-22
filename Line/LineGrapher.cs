using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGrapher : MonoBehaviour
{
    //CreateObject
    public Material lineMaterial;
    protected GameObject objectPlaceholder;
    public GameObject Obj_parentLineGrapher;
    public GameObject Obj_parentFourier3D;

    //Grapher
    protected Vector3[] dataPoints;
    public int upRange;
    public int downRange;
    protected int range;
    private GameObject Obj_Line;

    void Start()
    {

        upRange = 25;
        downRange = -25;
        range = upRange - downRange;
        dataPoints = new Vector3[range];

        RenderGraph();

    }

    void RenderGraph()
    {
        CreateObject("line", Color.white, Obj_parentLineGrapher);
        Obj_Line = objectPlaceholder;
        LineRenderer newRenderer = Obj_Line.GetComponent<LineRenderer>();
        newRenderer.material.color = new Color (1f,1f,1f,0.1f);
        newRenderer.material.SetColor("_EmissionColor", new Color(0f, 0f, 0f, 1f));

        for (int t = 0; t < range - 2; t++)
        {
            newRenderer.positionCount++;
        }

        for (int t = 0; t < range; t++)
        {
            int tOffset = t + downRange;
            float x = 30 * Mathf.Cos(tOffset);
            float y = 2 * tOffset;
            float z = 30 * Mathf.Sin(tOffset);

            dataPoints[t] = new Vector3(x, y, z);

            newRenderer.SetPosition(t, dataPoints[t]);

        }
    }

    protected void CreateObject(string name, Color color, GameObject parent)
    {
        GameObject newObject = new GameObject(name);
        LineRenderer objRenderer = newObject.AddComponent<LineRenderer>();

        objRenderer.material = lineMaterial;
        objRenderer.numCornerVertices = 30;
        objRenderer.numCapVertices = 30;
        objRenderer.useWorldSpace = false;
        objRenderer.material.SetColor("_Color", color);

        Transform targetTransform = parent.transform;
        newObject.transform.SetParent(targetTransform);
        objectPlaceholder = newObject;

    }
}
