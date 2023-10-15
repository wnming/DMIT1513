using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClaimButton : MonoBehaviour
{
    [SerializeField] TMP_Text buttonText;
    [SerializeField] Button thisClaimButton;
    public bool isClaim = false;

    private void Awake()
    {
        thisClaimButton = GetComponent<Button>();
        thisClaimButton.gameObject.SetActive(false);
        buttonText.fontSizeMax = 18;
    }

    public void Claim()
    {
        isClaim = true;
        thisClaimButton.gameObject.SetActive(true);
        buttonText.text = "Claimed";
        buttonText.fontSizeMax = 14;
        thisClaimButton.interactable = false;
    }

    public void ShowClaimButton()
    {
        thisClaimButton.gameObject.SetActive(true);
    }

    public void HideClaimButton()
    {
        thisClaimButton.gameObject.SetActive(false);
    }
}
