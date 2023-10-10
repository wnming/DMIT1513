using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    float moveSpeed = 20.0f;
    float rotationSpeed = 80.0f;
    [SerializeField] Camera interactPersonCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!interactPersonCamera.enabled)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            rb.AddRelativeForce(Vector3.forward * verticalInput * moveSpeed);

            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
