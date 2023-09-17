using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class TrampolineBehaviour : MonoBehaviour
{
    //Trampoline Variables
    Rigidbody rb;
    private float bounceRate = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb = collision.transform.gameObject.GetComponentInParent<Rigidbody>();

        if (rb != null)
        {
            var speed = collision.relativeVelocity.y / bounceRate;
            rb.velocity = new Vector3(rb.velocity.x, -speed, rb.velocity.z);
        }
    }
}
