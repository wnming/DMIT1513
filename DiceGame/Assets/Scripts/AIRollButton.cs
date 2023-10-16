using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AIRollButton : MonoBehaviour
{
    [SerializeField] TMP_Text buttonText;
    [SerializeField] Button AIRollOnOffButton;
    public bool isAIRollOn;

    // Start is called before the first frame update
    void Awake()
    {
        isAIRollOn = false;
        buttonText.text = "AI Roll ";
        buttonText.text += isAIRollOn ? "On" : "Off";
        ProtectButton();
    }

    public void TurnAIOnOff()
    {
        isAIRollOn = !isAIRollOn;
        buttonText.text = "AI Roll ";
        buttonText.text += isAIRollOn ? "On" : "Off";
    }

    public void ProtectButton()
    {
        isAIRollOn = false;
        buttonText.text = "AI Roll Off";
        TemporaryProtectButton();
    }

    public void TemporaryProtectButton()
    {
        AIRollOnOffButton.GetComponent<Button>().interactable = false;
    }

    public void ReleaseButton()
    {
        AIRollOnOffButton.GetComponent<Button>().interactable = true;
    }
}
