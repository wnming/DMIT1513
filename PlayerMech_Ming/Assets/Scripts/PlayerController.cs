using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    AudioSource movingSound;

    [SerializeField] GameObject Torso;
    [SerializeField] GameObject LeftArm;
    [SerializeField] GameObject RightArm;

    [SerializeField] GameObject pausePanel;
    GameSceneManager gameSceneManager;

    float moveSpeed;
    float rotationSpeed;

    //public InputAction rotateAction;
    Vector2 rotateValue;
    Vector3 angles;
    Vector3 ShoulderAngles;

    [SerializeField] GameObject SelectWeaponSlotText;

    public bool isShowText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movingSound = GetComponent<AudioSource>();
        moveSpeed = 30.0f;
        rotationSpeed = 50.0f;
        //Cursor.lockState = CursorLockMode.Locked;
        pausePanel.SetActive(false);
        gameSceneManager = new GameObject("GameSceneManager").AddComponent<GameSceneManager>();
        SelectWeaponSlotText.SetActive(false);
        isShowText = false;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput != 0 && !movingSound.isPlaying)
        {
            movingSound.Play();
        }
        else
        {
            if(verticalInput == 0)
            {
                movingSound.Stop();
            }
        }

        rb.AddRelativeForce(Vector3.forward * verticalInput * moveSpeed);

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        TorsoRotation();
        ArmRotation();

        if (pausePanel.activeSelf)
        {
            //Cursor.lockState = CursorLockMode.Confined;
        }else
        {
            //Cursor.lockState = CursorLockMode.Locked;
        }

        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
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

        if (isShowText)
        {
            SelectWeaponSlotText.SetActive(true);
        }
        else
        {
            SelectWeaponSlotText.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Weapon" && Vector3.Distance(transform.position, other.gameObject.transform.position) < 2.5f)
        {
            //SelectWeaponSlotText.SetActive(true);
            WeaponController weapon = other.gameObject.GetComponentInParent<WeaponController>();
            if (weapon != null)
            {
                if (weapon.isOnTheGround && !weapon.isWeaponActive)
                {
                    isShowText = true;
                    weapon.AttachWeapon();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            isShowText = false;
        }
    }

    public void ContinueGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitTheGame()
    {
        Time.timeScale = 1;
        gameSceneManager.LoadMainMenu();
    }

    private void ArmRotation()
    {
        float horizontalInput = Input.GetAxis("Mouse Y");

        LeftArm.transform.Rotate(Vector3.left, horizontalInput * rotationSpeed * Time.deltaTime);
        RightArm.transform.Rotate(Vector3.left, horizontalInput * rotationSpeed * Time.deltaTime);

        ShoulderAngles = LeftArm.transform.localEulerAngles;

        //Check the angles to see if they need to be clamped
        if (ShoulderAngles.x > 45.0f && ShoulderAngles.x < 180.0f)
        {
            LeftArm.transform.localRotation = Quaternion.Euler(45.0f, 0, 0);
            RightArm.transform.localRotation = Quaternion.Euler(45.0f, 0, 0);
        }
        if (ShoulderAngles.x < 315.0f && ShoulderAngles.x > 180.0f)
        {
            LeftArm.transform.localRotation = Quaternion.Euler(315.0f, 0, 0);
            RightArm.transform.localRotation = Quaternion.Euler(315.0f, 0, 0);
        }
    }

    private void TorsoRotation()
    {
        float verticalInput = Input.GetAxis("Mouse X");

        Torso.transform.Rotate(Vector3.up, verticalInput * rotationSpeed * Time.deltaTime);

        angles = Torso.transform.localEulerAngles;

        if (angles.y < 270.0f && angles.y > 90.0f)
        {
            if(angles.y > 180)
            {
                Torso.transform.localRotation = Quaternion.Euler(0, 270.0f, 0);
            }
            else
            {
                Torso.transform.localRotation = Quaternion.Euler(0, 90.0f, 0);
            }
        }
    }
}
