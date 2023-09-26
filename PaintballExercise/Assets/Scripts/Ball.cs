using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameObject ball;
    private void OnCollisionEnter(Collision collision)
    {
        //change the color
        collision.gameObject.GetComponent<Renderer>().material.color = ball.GetComponent<Renderer>().material.color;
        Destroy(ball);
    }
}
