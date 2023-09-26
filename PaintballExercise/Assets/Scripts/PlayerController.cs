using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject weapon;

    public InputAction rotateAction;

    float rotationSpeed;

    Vector3 angles;

    Vector2 rotateValue;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        rotateValue = rotateAction.ReadValue<Vector2>();

        weapon.transform.Rotate(Vector3.right, rotateValue.y * rotationSpeed * Time.deltaTime);

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

    private void OnEnable()
    {
        rotateAction.Enable();
    }

    private void OnDisable()
    {
        rotateAction.Disable();
    }
}
