using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankExerciseController : MonoBehaviour
{
    //input variables
    public InputAction leftThreadAction, rightThreadAction, rotateAction;

    Vector2 moveValueLeft, moveValueRight, rotateValue;

    //movement variables
    float movementSpeed, rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveValueLeft = leftThreadAction.ReadValue<Vector2>();
        moveValueRight = rightThreadAction.ReadValue<Vector2>();

        transform.Rotate(Vector3.up, rotateValue.x * rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Debug.Log(moveValueLeft);
        //move the object
        transform.Translate(new Vector3(moveValueLeft.x, 0, moveValueLeft.y) * movementSpeed * Time.deltaTime);
        transform.Translate(new Vector3(moveValueRight.x, 0, moveValueRight.y) * movementSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        leftThreadAction.Enable();
        rightThreadAction.Enable();
        rotateAction.Enable();
    }

    private void OnDisable()
    {
        leftThreadAction.Disable();
        rightThreadAction.Enable();
        rotateAction.Disable();
    }
}
