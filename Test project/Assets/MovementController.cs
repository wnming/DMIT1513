using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    //input variables
    public InputAction moveAction;
    Vector2 moveValue;

    //movement variables
    float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //initial movement variable
        movementSpeed = 0.9f;
    }

    // Update is called once per frame
    void Update()
    {
        //get player input
        moveValue = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        //move the object
        transform.Translate(new Vector3(moveValue.x, 0, moveValue.y) * movementSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }
}
