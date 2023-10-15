using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DiceButton : MonoBehaviour
{
    [SerializeField] Dice parentDice;
    [Tooltip("If true, the die will not roll when the roll button is pressed.")]
    public bool keepDice;

    [SerializeField] TMP_Text buttonText;
    [SerializeField] Button keepButton;
    [SerializeField] GameObject keepButtonCanvas;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
        keepButton = GetComponentInChildren <Button>();
    }

    private void Start()
    {
        Debug.Log("Start");
        keepDice = false;
    }

    public void DisableInteractButton()
    {
        keepButton.interactable = false;
    }

    public void EnableInteractButton()
    {
        keepButton.interactable = true;
    }

    public void HideButton()
    {
        keepButtonCanvas.SetActive(false);
        //keepButton.GetComponentInParent<GameObject>().SetActive(false);
        //keepButton.enabled = false;
    }

    public void ShowButton()
    {
        keepButtonCanvas.SetActive(true);
        //keepButton.GetComponentInParent<GameObject>().SetActive(true);
        //keepButton.enabled = true;
    }

    public void ToggleDice()
    {
        if (keepDice)
        {
            keepDice = false;
        }
        else
        {
            keepDice = true;
        }
        buttonText.text = keepDice ? "Keep" : "Roll";
    }

    public void ResetDice()
    {
        keepDice = false;
        buttonText.text = "Roll";
    }
}
