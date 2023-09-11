using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankExerciseController : MonoBehaviour
{
    //input variables
    public InputAction leftThreadAction, rightThreadAction;
    public InputAction turretAction, barrelAction;

    Vector2 moveValueLeft, moveValueRight;
    Vector2 turretValue, barrelValue;

    //movement variables
    float movementSpeed, rotationSpeed;

    [SerializeField] GameObject turret, barrel;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 3.0f;
        rotationSpeed = 250.0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveValueLeft = leftThreadAction.ReadValue<Vector2>();
        moveValueRight = rightThreadAction.ReadValue<Vector2>();
        turretValue = turretAction.ReadValue<Vector2>();
        barrelValue = barrelAction.ReadValue<Vector2>();

        //Turret Rotate Left – D
        //Turret Rotate Right – J
        turret.transform.Rotate(Vector3.up, turretValue.x * rotationSpeed * Time.deltaTime);

        //Rotation
        //If the right tread is moving forward and the left is moving backward the tank should rotate to the left at full speed.
        if(moveValueRight.y == 1.0f && moveValueLeft.y == -1.0f)
        {
            transform.Rotate(Vector3.down, rotationSpeed * Time.deltaTime);
        }

        //If the right tread is moving backward and the left is moving forward the tank should rotate to the right at full speed.
        if (moveValueRight.y == -1.0f && moveValueLeft.y == 1.0f)
        {
            transform.Rotate(Vector3.down, rotationSpeed * Time.deltaTime);
        }

        //If the right tread is moving forward and the left is not moving the tank should rotate to the left at half speed.
        if (moveValueRight.y == 1.0f && moveValueLeft.y == 0.0f)
        {
            transform.Rotate(Vector3.down, (rotationSpeed / 2) * Time.deltaTime);
        }

        //If the left tread is moving forward and the right is not moving the tank should rotate to the right at half speed.
        if (moveValueRight.y == 0.0f && moveValueLeft.y == 1.0f)
        {
            transform.Rotate(Vector3.up, (rotationSpeed / 2) * Time.deltaTime, Space.World);
        }

        //Reverse the rotation of the last two if the movement of the tread is backward.
        if (moveValueRight.y == -1.0f && moveValueLeft.y == 0.0f)
        {
            transform.Rotate(Vector3.up, (rotationSpeed / 2) * Time.deltaTime);
        }
        if (moveValueRight.y == 0.0f && moveValueLeft.y == -1.0f)
        {
            transform.Rotate(Vector3.down, (rotationSpeed / 2) * Time.deltaTime);
        }

        //Barrel Rotate Up – O
        //Barrel Rotate Down – L
        barrel.transform.Rotate(Vector3.forward, barrelValue.y * rotationSpeed * Time.deltaTime);

        //move the object
        //When both treads are moving forward the tank should move forward at full speed.
        if(moveValueRight.y == 1.0f && moveValueLeft.y == 1.0f)
        {
            transform.Translate(new Vector3(moveValueLeft.x, 0, moveValueLeft.y) * movementSpeed * Time.deltaTime, Space.Self);
        }
        else
        {
            //If one tread is moving forward the tank should move forward at half speed.
            transform.Translate(new Vector3(moveValueLeft.x, 0, moveValueLeft.y) * (movementSpeed / 2) * Time.deltaTime, Space.Self);
        }
        
        //When both treads are moving backward the tank should move backward at full speed.
        if(moveValueRight.y == -1.0f && moveValueLeft.y == -1.0f)
        {
            transform.Translate(new Vector3(moveValueRight.x, 0, moveValueRight.y) * movementSpeed * Time.deltaTime, Space.Self);
        }
        else
        {
            //If one tread is moving backward the tank should move backward at half speed.
            transform.Translate(new Vector3(moveValueRight.x, 0, moveValueRight.y) * (movementSpeed / 2) * Time.deltaTime, Space.Self);
        }
    }

    private void FixedUpdate()
    {
        ////move the object
        //transform.Translate(new Vector3(moveValueLeft.x, 0, moveValueLeft.y) * movementSpeed * Time.deltaTime);
        //transform.Translate(new Vector3(moveValueRight.x, 0, moveValueRight.y) * movementSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        leftThreadAction.Enable();
        rightThreadAction.Enable();
        turretAction.Enable();
        barrelAction.Enable();
    }

    private void OnDisable()
    {
        leftThreadAction.Disable();
        rightThreadAction.Enable();
        turretAction.Disable();
        barrelAction.Disable();
    }
}
