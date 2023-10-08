using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveControl : MonoBehaviour
{
    float rotateSpeed = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed, 0.0f, Space.World);
    }
}
