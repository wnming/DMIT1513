using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    [SerializeField] Camera firstPersonCamera;
    [SerializeField] Camera interactPersonCamera;

    public InputAction rotateAction;
    Vector2 rotateValue;
    Vector3 angles;

    float rotationSpeed = 70.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateValue = rotateAction.ReadValue<Vector2>();
        if (!interactPersonCamera.enabled) 
        {
            if (firstPersonCamera.enabled)
            {
                //Rotate player, and weapon
                transform.Rotate(Vector3.up, rotateValue.x * rotationSpeed * Time.deltaTime);
            }

            weapon.transform.Rotate(Vector3.left, rotateValue.y * rotationSpeed * Time.deltaTime);

            //Get the current weapon angles
            angles = weapon.transform.eulerAngles;

            //Check the angles to see if they need to be clamped
            if (angles.x > 45.0f && angles.x < 180.0f)
            {
                weapon.transform.localRotation = Quaternion.Euler(45.0f, 0, 0);
            }
            if (angles.x < 315.0f && angles.x > 180.0f)
            {
                weapon.transform.localRotation = Quaternion.Euler(315.0f, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
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
