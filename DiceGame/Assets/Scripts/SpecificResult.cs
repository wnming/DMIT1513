using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecificResult : MonoBehaviour
{
    [SerializeField] TMP_Text buttonText;
    [SerializeField] Button SpecificResultButton;
    public bool isSpecificResultOn;

    // Start is called before the first frame update
    void Awake()
    {
        isSpecificResultOn = false;
        buttonText.text = "Specific Result ";
        buttonText.text += isSpecificResultOn ? "On" : "Off";
    }

    public void TurnSpecificResultOnOff()
    {
        isSpecificResultOn = !isSpecificResultOn;
        buttonText.text = "Specific Result ";
        buttonText.text += isSpecificResultOn ? "On" : "Off";
    }

    public void TurnSpecificResultOff()
    {
        isSpecificResultOn = false;
        buttonText.text = "Specific Result Off";
    }

    public void ProtectButton()
    {
        SpecificResultButton.GetComponent<Button>().interactable = false;
    }
    public void ReleaseButton()
    {
        SpecificResultButton.GetComponent<Button>().interactable = true;
    }
}
