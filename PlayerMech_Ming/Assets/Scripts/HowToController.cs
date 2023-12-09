using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToController : MonoBehaviour
{
    [SerializeField] GameObject howToPanel;

    void Start()
    {
        howToPanel.SetActive(false);
    }

    public void ShowHowTo()
    {
        howToPanel.SetActive(true);
    }

    public void HideHowTo()
    {
        howToPanel.SetActive(false);
    }
}
