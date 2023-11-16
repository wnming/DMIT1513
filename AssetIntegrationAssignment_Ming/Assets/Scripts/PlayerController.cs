using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum ItemType 
    { 
        Ball,
        Car,
        House,
        Penguin,
        Telescope
    }

    Rigidbody rb;

    float moveSpeed;
    float jumpSpeed;
    float rotationSpeed;

    int damage;

    public bool isJumping = false;
    public float horizontalInput;
    public float verticalInput;

    public List<bool> FindItemsList = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 30.0f;
        rotationSpeed = 80.0f;
        jumpSpeed = 15.0f;
        damage = 10;
        for(int index = 0; index < 5; index++)
        {
            FindItemsList.Add(false);
        }
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

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        rb.AddRelativeForce(Vector3.forward * verticalInput * moveSpeed);

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }

        if (collision.gameObject.CompareTag("Find"))
        {
            if(collision.gameObject.name == "Ball")
            {
                FindItemsList[(int)ItemType.Ball] = true;
                collision.gameObject.SetActive(false);
            }
            if (collision.gameObject.name == "Car")
            {
                FindItemsList[(int)ItemType.Car] = true;
                collision.gameObject.SetActive(false);
            }
            if (collision.gameObject.name == "House")
            {
                FindItemsList[(int)ItemType.House] = true;
                collision.gameObject.SetActive(false);
            }
            if (collision.gameObject.name == "Penguin")
            {
                FindItemsList[(int)ItemType.Penguin] = true;
                collision.gameObject.SetActive(false);
            }
            if (collision.gameObject.name == "Telescope")
            {
                FindItemsList[(int)ItemType.Telescope] = true;
                collision.gameObject.SetActive(false);
            }
        }

        //if (collision.gameObject.tag == "Enemy")
        //{
        //    Health health = gameObject.GetComponent<Health>();
        //    if (health != null)
        //    {
        //        health.ApplyDamage(damage);
        //    }
        //}
    }
}
