using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBooster : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float boosterForce;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rigidbody.AddRelativeForce(Vector3.up * boosterForce * Time.fixedDeltaTime);
        rigidbody.AddForceAtPosition(transform.up * boosterForce * Time.fixedDeltaTime, transform.position);
    }
}
