using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToButton : MonoBehaviour
{
    public Button howToButton;

    private void Awake()
    {
        howToButton = GetComponent<Button>();
        ShowHowToButton();
    }

    public void ShowHowToButton()
    {
        howToButton.gameObject.SetActive(true);
    }

    public void HideHowToButton()
    {
        howToButton.gameObject.SetActive(false);
    }
}
