using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Mine : MonoBehaviour
{
    //Explosion Variables
    [SerializeField] float force, radius;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Collider[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    { 
        colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            //float distance;
            rigidbody = collider.transform.gameObject.GetComponentInParent<Rigidbody>();

            //distance = Vector3.Distance(transform.position, collider.transform.position);

            if(rigidbody != null)
            {
                rigidbody.AddExplosionForce(force, transform.position, radius);
            }
        }
    }
}
