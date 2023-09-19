using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    public InputAction moveAction;
    float movementSpeed, rotationSpeed, rotationForce;
    Vector2 moveValue;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.fixedDeltaTime * (moveValue.x + moveValue.y));
        transform.Rotate(Vector3.up * rotationSpeed * Time.fixedDeltaTime * (moveValue.x - moveValue.y));

        rb.AddRelativeTorque(Vector3.up * rotationForce * Time.fixedDeltaTime * (moveValue.y - moveValue.x), ForceMode.Acceleration);
    }
}
