using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Barrel : MonoBehaviour
{
    [SerializeField] GameObject ball, barrel;
    GameObject[] ballPool;

    float fireRate, velocity, timeStamp;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 0.8f;
        velocity = 15.0f;
        index = 0;

        ballPool = new GameObject[10];
        for (int i = 0; i < ballPool.Length; i++)
        {
            ballPool[i] = Instantiate(ball, transform);
            ballPool[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            if (Time.time > timeStamp + fireRate) 
            {
                timeStamp = Time.time;
                ballPool[index].SetActive(true);
                ballPool[index].transform.position = barrel.transform.position;
                ballPool[index].transform.rotation = barrel.transform.rotation;
                ballPool[index].GetComponent<Rigidbody>().velocity = transform.forward * velocity;
                index++;
                if(index == ballPool.Length - 1)
                {
                    index = 0;
                }
            }
        }
    }
}
