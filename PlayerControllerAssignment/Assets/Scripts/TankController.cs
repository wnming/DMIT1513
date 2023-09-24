using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using static UnityEditor.Progress;

public class TankController : MonoBehaviour
{
    public InputAction moveAction, rotationAction;
    float movementSpeed, rotationSpeed, moveForce, rotationForce, floatForce;
    Vector2 moveValue, rotationValue;
    Rigidbody rb;

    public float vInput, hInput;

    float distance;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        moveForce = 3.0f;
        movementSpeed = 25.0f;
        rotationSpeed = 80.0f;
        floatForce = 10.0f;

        distance = 0.8f;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        rotationValue = rotationAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");

        Debug.Log(vInput);
        Debug.Log(hInput);
        //rb.AddRelativeForce(Vector3.forward * vInput * movementSpeed);
        transform.Rotate(Vector3.up, hInput * Time.deltaTime * rotationSpeed);
        rb.AddForce(Vector3.forward * (moveValue.x + moveValue.y) * movementSpeed);

        //hover effect
        if (Physics.Raycast(transform.position, Vector3.down, out hit, distance))
        {
            rb.AddForce(transform.up * (distance - hit.distance) / distance * rb.mass, ForceMode.Impulse);
        }

        //transform.Translate(Vector3.forward * movementSpeed * Time.fixedDeltaTime * (moveValue.x + moveValue.y));
        //transform.Rotate(Vector3.up * rotationSpeed * Time.fixedDeltaTime * (rotationValue.x - rotationValue.y));

        //rb.AddRelativeTorque(Vector3.up * rotationForce * Time.fixedDeltaTime * (moveValue.y - moveValue.x), ForceMode.Acceleration);
    }

    private void OnEnable()
    {
        moveAction.Enable();
        rotationAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        rotationAction.Disable();
    }
}
