using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Camera firstPersonCamera;
    [SerializeField] Camera thirdPersonCamera;
    [SerializeField] Camera interactPersonCamera;

    private void Start()
    {
        ShowThirdPersonView();
        interactPersonCamera.enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && !interactPersonCamera.enabled)
        {
            if (thirdPersonCamera.enabled)
            {
                ShowFirstPersonView();
            }
            else
            {
                ShowThirdPersonView();
            }
        }
    }

    //show third person view
    public void ShowThirdPersonView()
    {
        firstPersonCamera.enabled = false;
        thirdPersonCamera.enabled = true;
    }

    //show first person view
    public void ShowFirstPersonView()
    {
        firstPersonCamera.enabled = true;
        thirdPersonCamera.enabled = false;
    }
}
