using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceButton : MonoBehaviour
{
    [SerializeField] Dice parentDice;
    [Tooltip("If true, the die will not roll when the roll button is pressed.")]
    public bool keepDice;

    [SerializeField] TMP_Text buttonText;
    private void Awake()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        Debug.Log("Start");
        keepDice = false;
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
