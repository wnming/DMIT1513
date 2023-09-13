using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Moving Variables
    [SerializeField] float movementSpeed;
    [SerializeField] Transform[] waypoints;
    [SerializeField] int index;
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Choose destination && Check to see if platform is at destination
        if(Vector3.Distance(transform.position, waypoints[index].position) <= 0.1f)
        {
            index++;

            if(index > waypoints.Length - 1)
            {
                index = 0;
            }
        }

        //Move platform
        //transform.Translate((waypoints[index].position - transform.position).normalized * movementSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rigidbody.AddRelativeForce((waypoints[index].position - transform.position).normalized * movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.gameObject.transform.root.parent = transform;
    }
}
