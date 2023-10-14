using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AIActivationButton : MonoBehaviour
{
    [SerializeField] TMP_Text buttonText;
    [SerializeField] Button AIOnOffButton;
    public bool isAIOn;

    // Start is called before the first frame update
    void Awake()
    {
        isAIOn = false;
        buttonText.text = "AI ";
        buttonText.text += isAIOn ? "On" : "Off";
    }

    public void TurnAIOnOff()
    {
        isAIOn = !isAIOn;
        buttonText.text = "AI ";
        buttonText.text += isAIOn ? "On" : "Off";
    }

    public void ProtectButton()
    {
        AIOnOffButton.GetComponent<Button>().interactable = false;
    }
    public void ReleaseButton()
    {
        AIOnOffButton.GetComponent<Button>().interactable = true;
    }
}
