using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageHowTo : MonoBehaviour
{
    public HowToButton HowToButton;
    public ShowHowTo ShowHowTo;
    public CloseHowToButton CloseHowToButton;

    public void ShowHowToObject()
    {
        HowToButton.HideHowToButton();
        ShowHowTo.ShowHowToObject();
    }

    public void HideHowToObject()
    {
        ShowHowTo.HideHowToObject();
        HowToButton.ShowHowToButton();
    }
}
