using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHowTo : MonoBehaviour
{
    [SerializeField] GameObject howToObject;

    private void Awake()
    {
        HideHowToObject();
    }

    public void ShowHowToObject()
    {
        howToObject.gameObject.SetActive(true);
    }

    public void HideHowToObject()
    {
        howToObject.gameObject.SetActive(false);
    }
}
