using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseLoader : MonoBehaviour
{
    //input variables
    public InputAction moveAction, rotateAction;

    Vector2 moveValue, rotateValue;

    //movement variables
    float movementSpeed, rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //initial movement variables
        movementSpeed = 3.0f;
        rotationSpeed = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        rotateValue = rotateAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        //move the object
        transform.Translate(new Vector3(moveValue.x, 0, moveValue.y) * movementSpeed * Time.deltaTime);

        transform.Rotate(Vector3.up, rotateValue.y * rotationSpeed * Time.deltaTime);
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
