using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerController;

public class FindImagesScript : MonoBehaviour
{
    RawImage image;
    [SerializeField] PlayerController playerController;

    void Start()
    {
        image = GetComponent<RawImage>();
        Color color = Color.gray;
        color.a = 0.8f;
        image.color = color;
    } 

    // Update is called once per frame
    void Update()
    {
        if (transform.name == "Ball" && playerController.FindItemsList[(int)ItemType.Ball]) 
        {
            Color color = Color.white;
            color.a = 1.0f;
            image.color = color;
        }
        if (transform.name == "Car" && playerController.FindItemsList[(int)ItemType.Car])
        {
            Color color = Color.white;
            color.a = 1.0f;
            image.color = color;
        }
        if (transform.name == "House" && playerController.FindItemsList[(int)ItemType.House])
        {
            Color color = Color.white;
            color.a = 1.0f;
            image.color = color;
        }
        if (transform.name == "Penguin" && playerController.FindItemsList[(int)ItemType.Penguin])
        {
            Color color = Color.white;
            color.a = 1.0f;
            image.color = color;
        }
        if (transform.name == "Telescope" && playerController.FindItemsList[(int)ItemType.Telescope])
        {
            Color color = Color.white;
            color.a = 1.0f;
            image.color = color;
        }
    }
}
