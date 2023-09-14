using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPlatform : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] float distance, antigravForce;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        antigravForce = rigidbody.mass;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, distance))
        {
            rigidbody.AddForce(transform.up * (distance - hit.distance) / distance * antigravForce, ForceMode.Impulse);
        }
    }
}
