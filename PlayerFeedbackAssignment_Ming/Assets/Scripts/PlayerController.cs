using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    float moveSpeed;
    float jumpSpeed;
    float rotationSpeed;

    bool isJumping = false;

    GameSceneManager gameSceneManager;
    [SerializeField] GameObject pausePanel;
    [SerializeField] Camera playerCamera;
    [SerializeField] Camera endGameCamera;

    public InputAction rotateAction;
    Vector2 rotateValue;
    Vector3 angles;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpSpeed = 20.0f;
        moveSpeed = 20.0f;
        rotationSpeed = 80.0f;
        gameSceneManager = new GameObject("GameSceneManager").AddComponent<GameSceneManager>();
        pausePanel.SetActive(false);
        endGameCamera.enabled = false;
    }

    private void Update()
    {
        //if (!weaponSelectionCamera.enabled)
        //{
            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                if (keyboard.leftAltKey.wasPressedThisFrame && !isJumping)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(0, jumpSpeed, 0);
                    isJumping = true;
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
                        ContinueGame();
                    }
                }
            }
            rotateValue = rotateAction.ReadValue<Vector2>();
            playerCamera.transform.Rotate(Vector3.left, rotateValue.y * rotationSpeed * Time.deltaTime);

            angles = playerCamera.transform.eulerAngles;

            if (angles.x > 20.0f && angles.x < 180.0f)
            {
                playerCamera.transform.localRotation = Quaternion.Euler(20.0f, 0, 0);
            }
            if (angles.x < 340.0f && angles.x > 180.0f)
            {
                playerCamera.transform.localRotation = Quaternion.Euler(340.0f, 0, 0);
            }
        //}
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.AddRelativeForce(Vector3.forward * verticalInput * moveSpeed);

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }

    public void ContinueGame()
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

    private void OnEnable()
    {
        rotateAction.Enable();
    }

    private void OnDisable()
    {
        rotateAction.Disable();
    }
}
