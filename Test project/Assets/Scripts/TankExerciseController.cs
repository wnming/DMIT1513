using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    Vector3 angles;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 3.0f;
        rotationSpeed = 50.0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveValueLeft = leftThreadAction.ReadValue<Vector2>();
        moveValueRight = rightThreadAction.ReadValue<Vector2>();
        turretValue = turretAction.ReadValue<Vector2>();
        barrelValue = barrelAction.ReadValue<Vector2>();

        //angles = barrel.transform.eulerAngles;

        turret.transform.Rotate(Vector3.up, turretValue.x * rotationSpeed * Time.deltaTime);

        //Rotation
        //If the right tread is moving forward and the left is moving backward the tank should rotate to the left at full speed.sii
        if(moveValueRight.y == 1.0f && moveValueLeft.y == -1.0f)
        {
            transform.Rotate(Vector3.up, moveValueRight.y * rotationSpeed * Time.deltaTime);
        }

        //If the right tread is moving backward and the left is moving forward the tank should rotate to the right at full speed.
        if (moveValueRight.y == -1.0f && moveValueLeft.y == 1.0f)
        {
            transform.Rotate(Vector3.up, moveValueRight.y * rotationSpeed * Time.deltaTime);
        }

        //If the right tread is moving forward and the left is not moving the tank should rotate to the left at half speed.
        if (moveValueRight.y == 1.0f && moveValueLeft.y == 0.0f)
        {
            transform.Rotate(Vector3.up, moveValueRight.y * (rotationSpeed / 2) * Time.deltaTime);
        }

        //If the left tread is moving forward and the right is not moving the tank should rotate to the right at half speed.
        if (moveValueRight.y == 0.0f && moveValueLeft.y == 1.0f)
        {
            transform.Rotate(Vector3.up, 220.0f * Time.deltaTime);
        }

        //Reverse the rotation of the last two if the movement of the tread is backward.
        //xxxxxxxxxxxx

        Debug.Log(barrelValue);
        //Barrel Rotate Up – O
        //Barrel Rotate Down – L
        barrel.transform.Rotate(Vector3.back, barrelValue.y * rotationSpeed * Time.deltaTime);

        //Check the angles to see if they need to be clamped
        //if (angles.x > 45.0f && angles.x < 180.0f)
        //{
        //    barrel.transform.localRotation = Quaternion.Euler(45.0f, 0, 0);
        //}
        //if (angles.x < 315.0f && angles.x > 180.0f)
        //{
        //    barrel.transform.localRotation = Quaternion.Euler(315.0f, 0, 0);
        //}

        //move the object
        transform.Translate(new Vector3(moveValueLeft.x, 0, moveValueLeft.y) * movementSpeed * Time.deltaTime);
        transform.Translate(new Vector3(moveValueRight.x, 0, moveValueRight.y) * movementSpeed * Time.deltaTime);
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
