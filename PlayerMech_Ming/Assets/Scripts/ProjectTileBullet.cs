using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectTileBullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    public void Fire(float speed)
    {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
