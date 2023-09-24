using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class BucketController : MonoBehaviour
{
    public InputAction rotateAction;

    Vector2 rotateValue;
    Vector3 angles;

    //movement variables
    float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 80.0f;
    }

    // Update is called once per frame
    void Update()
    {
        rotateValue = rotateAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.left, rotateValue.y * rotationSpeed * Time.fixedDeltaTime);

        angles = transform.localRotation.eulerAngles;

        if (angles.x > 70.0f && angles.x < 90.0f)
        {
            transform.localRotation = Quaternion.Euler(70.0f, 90, 0);
        }
        if (angles.x < 320.0f && angles.x > 270.0f)
        {
            transform.localRotation = Quaternion.Euler(320.0f, 90, 0);
        }
    }

    private void OnEnable()
    {
        rotateAction.Enable();
    }

    private void OnDisable()
    {
        rotateAction.Disable();
    }
}
