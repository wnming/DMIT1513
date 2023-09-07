using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    //input variables
    public InputAction moveAction, rotateAction;

    Vector2 moveValue, rotateValue;

    //movement variables
    float movementSpeed, rotationSpeed;

    [SerializeField] GameObject weapon;

    Vector3 angles;

    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        //initial movement variable
        movementSpeed = 3.0f;
        rotationSpeed = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //get player input
        moveValue = moveAction.ReadValue<Vector2>();
        rotateValue = rotateAction.ReadValue<Vector2>();

        //Rotate player and weapon
        transform.Rotate(Vector3.up, rotateValue.x * rotationSpeed * Time.deltaTime);

        weapon.transform.Rotate(Vector3.right, rotateValue.y * rotationSpeed * Time.deltaTime);

        //Get the current weapon angles
        angles = weapon.transform.eulerAngles;

        //Check the angles to see if they need to be clamped
        if(angles.x > 45.0f && angles.x < 180.0f)
        {
            weapon.transform.localRotation = Quaternion.Euler(45.0f, 0, 0);
        }
        if(angles.x < 315.0f && angles.x > 180.0f)
        {
            weapon.transform.localRotation = Quaternion.Euler(315.0f, 0, 0);
        }

        var keyboard = Keyboard.current;
        if(keyboard != null && isGrounded)
        {
            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                //rb.velocity = new Vector3(0, 10, 0);
                GetComponent<Rigidbody>().velocity = new Vector3(0, 5.0f, 0);
                //GetComponent<Rigidbody>().AddForce(Vector3.up * 500.0f * Time.deltaTime, ForceMode.VelocityChange);
                isGrounded = false;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "ground")
    //    {
    //        isGrounded = true;
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "ground")
    //    {
    //        isGrounded = false;
    //    }
    //}

    private void FixedUpdate()
    {
        //move the object
        transform.Translate(new Vector3(moveValue.x, 0, moveValue.y) * movementSpeed * Time.deltaTime);

        transform.Rotate(Vector3.up, rotateValue.y * rotationSpeed * Time.deltaTime);

        weapon.transform.Rotate(Vector3.right, rotateValue.y * rotationSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        moveAction.Enable();
        rotateAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        rotateAction.Disable();
    }
}
