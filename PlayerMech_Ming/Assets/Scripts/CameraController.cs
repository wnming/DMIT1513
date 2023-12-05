using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera firstPersonCamera;
    [SerializeField] Camera thirdPersonCamera;

    float initialZ;
    float topDownZ;

    private void Start()
    {
        Debug.Log(firstPersonCamera.transform.localPosition.z);
        initialZ = firstPersonCamera.transform.localPosition.z;
        topDownZ = firstPersonCamera.transform.localPosition.z - 0.87f;
        ShowThirdPersonView();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.T))
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                firstPersonCamera.transform.localPosition = new Vector3(firstPersonCamera.transform.localPosition.x, firstPersonCamera.transform.localPosition.y, topDownZ);
                ShowFirstPersonView();
            }
            else
            {
                if (thirdPersonCamera.enabled)
                {
                    firstPersonCamera.transform.localPosition = new Vector3(firstPersonCamera.transform.localPosition.x, firstPersonCamera.transform.localPosition.y, initialZ);
                    ShowFirstPersonView();
                }
                else
                {
                    ShowThirdPersonView();
                }
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
