using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Camera thirdPersonCamera;
    float min = 30.0f;
    float max = 75.0f;
    float scrollSpeed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        thirdPersonCamera.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * -scrollSpeed;
        thirdPersonCamera.fieldOfView = Mathf.Clamp(thirdPersonCamera.fieldOfView, min, max);
    }
}
