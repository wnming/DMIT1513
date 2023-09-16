using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineBehaviour : MonoBehaviour
{
    //Trampoline Variables
    Rigidbody rb;
    [SerializeField] Collider[] colliders;
    [SerializeField] float radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        colliders = Physics.OverlapBox(transform.position, transform.localScale / 2);
        foreach (Collider collider in colliders)
        {
            //Debug.Log(collider.name);
            ////float distance;
            rb = collider.transform.gameObject.GetComponentInParent<Rigidbody>();
            //Debug.Log("fdss");

            ////distance = Vector3.Distance(transform.position, collider.transform.position);

            if (rb != null)
            {
                //Vector2 velocity = collision.relativeVelocity;
                //rb.velocity = Vector3.Reflect(rb.GetRelativePointVelocity(collider.transform.position), Vector3.up/*make this a Vector2 pointing 90 degrees from your paddle surface*/);

                //Debug.Log(rb.GetRelativePointVelocity(collider.transform.position));
                //rb.AddForce(rb.GetRelativePointVelocity(collider.transform.position) * Time.deltaTime, ForceMode.VelocityChange);

                Vector3 collisionNormal = (rb.transform.position - transform.position).normalized;
                float playerCollisionSpeed = Vector3.Dot(collisionNormal, rb.velocity);
                //rb.AddForce(rb.transform.position * playerCollisionSpeed * Time.deltaTime, ForceMode.VelocityChange);
                rb.velocity = new Vector3(0, playerCollisionSpeed, 0);

                Debug.Log(playerCollisionSpeed);

                //rb.velocity += -rb.GetRelativePointVelocity(rb.transform.position);
                //Vector3 relativeVelocity = new Vector3(0, 0, 0) - rb.velocity;
                //relativeVelocity.y = rb.velocity.y;
                //rb.GetRelativePointVelocity(rb.transform.position) += relativeVelocity * Time.fixedDeltaTime * 10.0f;
                //Vector3 force = new Vector3(0.0f, 1.0f, 0.0f);
                //rb.velocity = rb.curr
                //1.0f * force / rb.mass;
                //rigidbody.velocity = Vector3.Reflect(rigidbody.velocity, collider.transform.position);
            }
        }
    }
}
