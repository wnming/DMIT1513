using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;

    float moveSpeed;
    float jumpSpeed;
    float rotationSpeed;

    int damage;

    bool isJumping = false;

    [SerializeField] Camera weaponSelectionCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpSpeed = 20.0f;
        moveSpeed = 20.0f;
        rotationSpeed = 80.0f;
        damage = 10;
        weaponSelectionCamera.enabled = false;
    }

    private void Update()
    {
        if (!weaponSelectionCamera.enabled)
        {
            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                if (keyboard.leftAltKey.wasPressedThisFrame && !isJumping)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(0, jumpSpeed, 0);
                    isJumping = true;
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!weaponSelectionCamera.enabled)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            rb.AddRelativeForce(Vector3.forward * verticalInput * moveSpeed);

            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Npc") || collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }
}
