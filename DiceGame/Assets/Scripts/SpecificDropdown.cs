using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecificDropdown : MonoBehaviour
{
    TMP_Dropdown dropdown;
    private void Awake()
    {
    }

    public void HideDropdown()
    {
        dropdown.Hide();
    }
}
