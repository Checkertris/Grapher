using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{

    public Rigidbody rb;

    void Start()

    {

        rb = gameObject.GetComponent<Rigidbody>();
    }
   
    void Update()
    {
        if (Input.GetKey("w"))
        {
            Debug.Log("w");

            rb.AddForce(Vector3.up*5000*Time.deltaTime);

        }

    }
}
