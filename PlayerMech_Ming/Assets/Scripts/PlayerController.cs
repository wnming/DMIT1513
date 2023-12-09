using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject[] enemyList;

    Rigidbody rb;
    [SerializeField] AudioSource movingSound;
    [SerializeField] AudioSource itemPickupSound;
    [SerializeField] AudioSource shotByEnemy;

    [SerializeField] GameObject Torso;
    [SerializeField] GameObject LeftArm;
    [SerializeField] GameObject RightArm;

    [SerializeField] GameObject pausePanel;
    GameSceneManager gameSceneManager;

    [SerializeField] WeaponController yellowWeapon;

    float moveSpeed;
    float rotationSpeed;

    //public InputAction rotateAction;
    Vector2 rotateValue;
    Vector3 angles;
    Vector3 ShoulderAngles;

    [SerializeField] GameObject SelectWeaponSlotText;

    [SerializeField] TextMeshProUGUI endGameText;

    public bool isShowText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 30.0f;
        rotationSpeed = 50.0f;
        //Cursor.lockState = CursorLockMode.Locked;
        pausePanel.SetActive(false);
        gameSceneManager = new GameObject("GameSceneManager").AddComponent<GameSceneManager>();
        SelectWeaponSlotText.SetActive(false);
        isShowText = false;
        endGameText.text = "";
    }

    void Update()
    {
        if(enemyList.Where(x => x.activeSelf).Count() <= 0)
        {
            endGameText.text = "You Win!!!";
            StartCoroutine(CoundownToClose());
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (verticalInput != 0 && !movingSound.isPlaying)
            {
                movingSound.Play();
            }
            else
            {
                if (verticalInput == 0)
                {
                    movingSound.Stop();
                }
            }

            rb.AddRelativeForce(Vector3.forward * verticalInput * moveSpeed);

            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

            TorsoRotation();
            ArmRotation();

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
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "BroomStick")
    //    {
    //        Debug.Log(other.gameObject.transform.localRotation.x);
    //        if(other.gameObject.transform.localRotation.x == -7.5f)
    //        {
    //            HealthScript health = this.GetComponent<HealthScript>();
    //            health.ApplyDamage(10);
    //            if (health.currentHealth <= 0)
    //            {
    //                movingSound.Stop();
    //                StartCoroutine(CoundownToClose());
    //            }
    //        }
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            shotByEnemy.Play();
            other.gameObject.SetActive(false);
            HealthScript health = this.GetComponent<HealthScript>();
            health.ApplyDamage(10);
            if (health.currentHealth <= 0)
            {
                endGameText.text = "Game Over!";
                movingSound.Stop();
                StartCoroutine(CoundownToClose());
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

    IEnumerator CoundownToClose()
    {
        movingSound.Stop();
        Time.timeScale = 0;
        yield return StartCoroutine(WaitForRealSeconds(3));
        Application.Quit();
    }

    IEnumerator WaitForRealSeconds(float seconds)
    {
        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - startTime < seconds)
        {
            yield return null;
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
        //yellow
        if (other.gameObject.tag == "Ammo" && yellowWeapon.currentSlot != -1 && Vector3.Distance(transform.position, other.gameObject.transform.position) < 2.5f)
        {
            itemPickupSound.Play();
            other.gameObject.SetActive(false);
            yellowWeapon.ammo += 5;
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
