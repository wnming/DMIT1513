using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponControl : MonoBehaviour
{
    [SerializeField] GameObject bullet, endWeapon;
    GameObject[] bulletPool;
    [SerializeField] Camera interactPersonCamera;

    float fireRate, velocity, timeStamp;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 0.5f;
        velocity = 15.0f;
        index = 0;

        bulletPool = new GameObject[10];
        for (int i = 0; i < bulletPool.Length; i++)
        {
            bulletPool[i] = Instantiate(bullet, transform);
            bulletPool[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        if (Input.GetKeyDown(KeyCode.Space) && !interactPersonCamera.enabled)
        {
            if (Time.time > timeStamp + fireRate)
            {
                timeStamp = Time.time;
                bulletPool[index].SetActive(true);
                bulletPool[index].transform.position = endWeapon.transform.position;
                bulletPool[index].transform.rotation = endWeapon.transform.rotation;
                bulletPool[index].GetComponent<Rigidbody>().velocity = transform.forward * velocity;
                index++;
                if (index == bulletPool.Length - 1)
                {
                    index = 0;
                }
            }
        }
    }
}
