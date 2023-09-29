using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //change the color
        if(collision.transform.tag == "Paintable")
        {
            collision.gameObject.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;
            gameObject.SetActive(false);
        }
    }
}
