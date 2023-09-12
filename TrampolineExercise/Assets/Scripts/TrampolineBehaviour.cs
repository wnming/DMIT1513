using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineBehaviour : MonoBehaviour
{
    //Trampoline Variables
    [SerializeField] 

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
        //colliders = Physics.OverlapSphere(transform.position, radius);

        //foreach (Collider collider in colliders)
        //{
        //    //float distance;
        //    rigidbody = collider.transform.gameObject.GetComponentInParent<Rigidbody>();

        //    //distance = Vector3.Distance(transform.position, collider.transform.position);

        //    if (rigidbody != null)
        //    {
        //        rigidbody.AddExplosionForce(force, transform.position, radius);
        //    }
        //}
    }
}
