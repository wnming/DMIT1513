using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    GameObject slot;
    public RawImage image;
    public bool isUsing;
    public bool isActive;

    public int slotNumber;
    public TextMeshProUGUI ammo;
    public Button ActivateButton;

    [SerializeField] GameObject usePanel;

    void Start()
    {
        usePanel.SetActive(false);
        slot = GetComponent<GameObject>();
        isUsing = false;
    }

    void Update()
    {
        ShowUI();
        if (isActive)
        {
            ActivateButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            ActivateButton.GetComponent<Image>().color = Color.white;
        }
    }

    private void ShowUI()
    {
        if (isUsing)
        {
            usePanel.SetActive(true);
        }
        else
        {
            usePanel.SetActive(false);
        }
    }
}
