using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    float moveSpeed = 20.0f;
    float rotationSpeed = 80.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.AddRelativeForce(Vector3.forward * verticalInput * moveSpeed);
        //transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        //Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        //movementDirection.Normalize();

        //transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);

        //transform.Rotate(new Vector3(0, Input.GetAxisRaw("Horizontal") * rotationSpeed, 0));
    }
}
