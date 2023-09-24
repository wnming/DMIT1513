using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class BucketTrigger : MonoBehaviour
{
    [SerializeField] GameObject bucket;
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
        if (other.gameObject.tag == "ball")
        {
            other.transform.parent = bucket.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
