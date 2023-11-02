using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    Vector3 startingPos;
    float speed = 10.0f;
    float amount = 0.3f;

    float effectRate;
    float nextEffect;

    [SerializeField] GameObject sparkleEffect;

    void Awake()
    {
        startingPos.x = transform.position.x;
        startingPos.y = transform.position.y;
        startingPos.z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(startingPos.x + (Mathf.Sin(Time.time * speed) * amount), transform.position.y, transform.position.z);
        if ((transform.position - startingPos).magnitude > 0.25f)
            transform.position = startingPos;

        effectRate = Random.Range(3.0f, 11.0f);

        if (Time.time > nextEffect && sparkleEffect != null)
        {
            nextEffect = Time.time + effectRate;

            Instantiate(sparkleEffect, new Vector3(transform.position.x + 1.6f, transform.position.y + 3 - Random.Range(0,8), transform.position.z - 0.2f), transform.rotation);
            // new Quaternion(90, transform.rotation.y, transform.rotation.z, transform.rotation.w)
        }
    }
}
