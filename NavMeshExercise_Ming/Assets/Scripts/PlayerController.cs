using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    float moveSpeed;
    float rotationSpeed;
    float jumpSpeed;
    bool isJumping = false;

    public InputAction rotateAction;
    Vector2 rotateValue;
    Vector3 angles;

    bool isHolding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 30.0f;
        rotationSpeed = 80.0f;
        jumpSpeed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.spaceKey.wasPressedThisFrame && !isJumping)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, jumpSpeed, 0);
                isJumping = true;
            }
        }

        if (isHolding)
        {
            rotateValue = rotateAction.ReadValue<Vector2>();
            transform.Rotate(Vector3.left, rotateValue.y * rotationSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up, rotateValue.x * rotationSpeed * Time.deltaTime);

            angles = transform.eulerAngles;

            if (angles.x > 20.0f && angles.x < 180.0f)
            {
                transform.localRotation = Quaternion.Euler(20.0f, 0, 0);
            }
            if (angles.x < 340.0f && angles.x > 180.0f)
            {
                transform.localRotation = Quaternion.Euler(340.0f, 0, 0);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            isHolding = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isHolding = false;
        }
        //var mouse = Mouse.current;

        //if(mouse != null)
        //{
        //    if (Input.GetMouseButtonDown(1))
        //    {
        //        rotateValue = rotateAction.ReadValue<Vector2>();
        //        transform.Rotate(Vector3.left, rotateValue.y * rotationSpeed * Time.deltaTime);

        //        angles = transform.eulerAngles;

        //        if (angles.x > 20.0f && angles.x < 180.0f)
        //        {
        //            transform.localRotation = Quaternion.Euler(20.0f, 0, 0);
        //        }
        //        if (angles.x < 340.0f && angles.x > 180.0f)
        //        {
        //            transform.localRotation = Quaternion.Euler(340.0f, 0, 0);
        //        }
        //    }
        //}

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.AddRelativeForce(Vector3.forward * verticalInput * moveSpeed);

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        rotateAction.Enable();
    }

    private void OnDisable()
    {
        rotateAction.Disable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stair") || collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }
}
