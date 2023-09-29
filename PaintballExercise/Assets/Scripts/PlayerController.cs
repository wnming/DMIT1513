using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject weapon;

    public InputAction rotateAction, moveAction;

    float rotationSpeed, movementSpeed;

    Vector3 angles;

    Vector2 rotateValue, moveValue;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 70.0f;
        movementSpeed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        rotateValue = rotateAction.ReadValue<Vector2>();
        moveValue = moveAction.ReadValue<Vector2>();

        //Rotate player and weapon
        transform.Rotate(Vector3.up, rotateValue.x * rotationSpeed * Time.deltaTime);
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

        if (Input.GetKey(KeyCode.Escape)) 
        {
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(moveValue.x, 0, moveValue.y) * movementSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        rotateAction.Enable();
        moveAction.Enable();
    }

    private void OnDisable()
    {
        rotateAction.Disable();
        moveAction.Disable();
    }
}
