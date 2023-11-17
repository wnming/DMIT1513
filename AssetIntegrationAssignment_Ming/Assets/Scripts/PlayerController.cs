using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    public bool isCarCollider;

    public List<bool> FindItemsList = new List<bool>();

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject findPanel;
    [SerializeField] TextMeshProUGUI panelText;
    [SerializeField] GameObject room2StartLocation;

    [SerializeField] AudioSource terrain1;
    [SerializeField] AudioSource terrain2;

    [SerializeField] Camera mainCamera;
    float defalutCam;

    [SerializeField] GameObject player;

    [SerializeField] AudioSource success;

    private bool isRoom2;

    public bool isFinish;

    AudioSource grabbingSound;

    GameScene gameSceneManager;

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
        gameSceneManager = new GameObject("GameSceneManager").AddComponent<GameScene>();
        findPanel.SetActive(true);
        pausePanel.SetActive(false);
        grabbingSound = GetComponent<AudioSource>();
        //terrain1 = GetComponent<AudioSource>();
        //terrain2 = GetComponent<AudioSource>();
        isRoom2 = false;
        isCarCollider = false;
        isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindItemsList.Where(x => x == true).Count() == FindItemsList.Count && !isRoom2) 
        {
            isRoom2 = true;
            StartCoroutine(setLocation());
        }

        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.spaceKey.wasPressedThisFrame && !isJumping)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, jumpSpeed, 0);
                isJumping = true;
            }
        }

        if (keyboard.escapeKey.wasPressedThisFrame && !pausePanel.activeSelf)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            if (keyboard.escapeKey.wasPressedThisFrame && pausePanel.activeSelf)
            {
                ResumeGame();
            }
        }
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        rb.AddRelativeForce(Vector3.forward * verticalInput * moveSpeed);

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }

    IEnumerator setLocation()
    {
        yield return new WaitForSeconds(3);
        findPanel.SetActive(false);
        panelText.text = "Find My Truck";
        panelText.fontSize = 25;
        terrain1.Stop();
        terrain2.Play();
        transform.position = room2StartLocation.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
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
                grabbingSound.Play();
                FindItemsList[(int)ItemType.Ball] = true;
                collision.gameObject.SetActive(false);
            }
            if (collision.gameObject.name == "Car")
            {
                grabbingSound.Play();
                FindItemsList[(int)ItemType.Car] = true;
                collision.gameObject.SetActive(false);
            }
            if (collision.gameObject.name == "House")
            {
                grabbingSound.Play();
                FindItemsList[(int)ItemType.House] = true;
                collision.gameObject.SetActive(false);
            }
            if (collision.gameObject.name == "Penguin")
            {
                grabbingSound.Play();
                FindItemsList[(int)ItemType.Penguin] = true;
                collision.gameObject.SetActive(false);
            }
            if (collision.gameObject.name == "Telescope")
            {
                grabbingSound.Play();
                FindItemsList[(int)ItemType.Telescope] = true;
                collision.gameObject.SetActive(false);
            }
        }

        if(collision.gameObject.tag == "CarCollider")
        {
            isCarCollider = true;
        }

        if(collision.gameObject.tag == "mazeEntrance")
        {
            collision.gameObject.SetActive(false);
            mainCamera.transform.localPosition = new Vector3(-0.52f, 5.377f, -1.82f);
            mainCamera.transform.localRotation = Quaternion.Euler(32.868f, mainCamera.transform.localRotation.y, mainCamera.transform.localRotation.z);
        }

        if (collision.gameObject.tag == "mazeExit")
        {
            collision.gameObject.SetActive(false);
            mainCamera.transform.localPosition = new Vector3(-0.52f, 4.64f, -5.82f);
            mainCamera.transform.localRotation = Quaternion.Euler(15.457f, 0, 0);
        }

        if (collision.gameObject.tag == "Truck")
        {
            player.transform.localRotation = Quaternion.Euler(player.transform.localRotation.x, 180, player.transform.localRotation.z);
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        success.Play();
        isFinish = true;
        yield return new WaitForSeconds(3);
        QuitTheGame();
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        gameSceneManager.LoadMainMenu();
    }
}
