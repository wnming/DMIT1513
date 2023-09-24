using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ArmsController : MonoBehaviour
{
    //input variables
    public InputAction rotateAction;

    Vector2 rotateValue;
    Vector3 angles;

    //movement variables
    float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //initial movement variables
        rotationSpeed = 80.0f;
    }

    // Update is called once per frame
    void Update()
    {
        rotateValue = rotateAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, rotateValue.y * rotationSpeed * Time.fixedDeltaTime);

        angles = transform.localRotation.eulerAngles;
        if (angles.z > 60.0f && angles.z < 90.0f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 60.0f);
        }
        if (angles.z > 270.0f && angles.z < 335.0f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 335.0f);
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
