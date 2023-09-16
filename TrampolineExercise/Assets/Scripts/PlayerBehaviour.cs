using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public InputAction moveAction;
    Vector2 moveValue;
    float movementSpeed, jumpSpeed;
    bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 2.0f;
        jumpSpeed = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();

        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.spaceKey.wasPressedThisFrame && !isJumping)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, jumpSpeed, 0);
                isJumping = true;
            }
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(moveValue.x, 0, moveValue.y) * movementSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("trampoline") || other.gameObject.CompareTag("floor"))
        {
            isJumping = false;
        }
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void Ondisable()
    {
        moveAction.Enable();
    }
}
