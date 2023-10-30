using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class VendingMachineController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI instructionText;

    [SerializeField] GameObject player;

    [SerializeField] Camera playerCamera;
    [SerializeField] Camera weaponSelectionCamera;

    [SerializeField] Canvas weaponSelectionUI;
    [SerializeField] GameObject weaponHolder;

    [SerializeField] Light dLight;

    Vector3 weaponHolderOriginalPosition;
    Quaternion weaponHolderOriginalRotation;

    Vector3 weaponHolderLastPosition;
    Quaternion weaponHolderLastRotation;

    public bool isPressT;
    private bool isSelected;

    float range = 3.9f;

    // Start is called before the first frame update
    void Start()
    {
        //instructionText = GetComponent<TextMeshProUGUI>();
        instructionText.gameObject.SetActive(false);
        weaponSelectionUI.enabled = false;
        weaponHolder.gameObject.SetActive(false);
        isPressT = false;
        isSelected = false;
        weaponHolderOriginalPosition = weaponHolder.transform.position;
        weaponHolderOriginalRotation = weaponHolder.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range && playerCamera.enabled)
        {
            if (playerCamera.enabled)
            {
                instructionText.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (isSelected)
                {
                    weaponHolderLastPosition = weaponHolder.transform.position;
                    weaponHolderLastRotation = weaponHolder.transform.rotation;
                }
                weaponHolder.transform.position = weaponHolderOriginalPosition;
                weaponHolder.transform.localScale = new Vector3(2, 2, 2);
                weaponHolder.transform.rotation = weaponHolderOriginalRotation;
                instructionText.gameObject.SetActive(false);
                //instructionText.text = "";
                isPressT = true;
                weaponSelectionCamera.enabled = true;
                playerCamera.enabled = false;
                dLight.enabled = false;
                weaponHolder.gameObject.SetActive(true);
                weaponSelectionUI.enabled = true;
            }
        }
        else
        {
            instructionText.gameObject.SetActive(false);
        }
    }

    public void CloseWeaponSelection()
    {
        weaponSelectionCamera.enabled = false;
        playerCamera.enabled = true;
        dLight.enabled = true;
        weaponSelectionUI.enabled = false;
        Debug.Log(isSelected);
        if (!isSelected)
        {
            weaponHolder.gameObject.SetActive(false);
        }
        else
        {
            weaponHolder.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            weaponHolder.transform.position = weaponHolderLastPosition;
            weaponHolder.transform.rotation = weaponHolderLastRotation;
        }
    }

    public void SelectWeapon()
    {
        weaponSelectionCamera.enabled = false;
        playerCamera.enabled = true;
        dLight.enabled = true;
        weaponSelectionUI.enabled = false;
        weaponHolder.transform.SetParent(player.transform);
        weaponHolder.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        //weaponHolder.transform.rotation = player.transform.rotation;
        weaponHolder.transform.forward = new Vector3(player.transform.forward.x, player.transform.forward.y, player.transform.forward.z);
        weaponHolder.transform.Rotate(0f, 180f, 0f, Space.Self);
        //weaponHolder.transform.rotation = Quaternion.Euler(0, player.transform.rotation.y - 180, 0);
        weaponHolder.transform.position = new Vector3(player.transform.position.x - 0.05f, player.transform.position.y - 4.7138f, player.transform.position.z - 0.2701f);
        isSelected = true;
    }
}
