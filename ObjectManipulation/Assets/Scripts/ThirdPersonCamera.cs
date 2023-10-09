using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Camera thirdPersonCamera;
    float min = 30f;
    float max = 75f;
    float scrollSpeed = 8f;

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
