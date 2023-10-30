using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] Canvas weaponSelectionUI;
    [SerializeField] Camera weaponSelectionCamera;

    void Start()
    {
        weaponHolder = GetComponent<GameObject>();
        weaponHolder.gameObject.GetComponent<Renderer>().enabled = false;
        weaponSelectionUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponSelectionCamera.enabled)
        {
            weaponHolder.gameObject.GetComponent<Renderer>().enabled = true;
            weaponSelectionCamera.enabled = true;
        }
        else
        {
            weaponHolder.gameObject.GetComponent<Renderer>().enabled = false;
            weaponSelectionUI.enabled = false;
        }
    }
}
