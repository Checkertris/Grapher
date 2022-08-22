using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane : MonoBehaviour
{

    public Rigidbody rb;

    void Start()
    {
        
    }


    void Update()
    {
        rb.velocity = new Vector3(0, -10, 0);


    }
}
