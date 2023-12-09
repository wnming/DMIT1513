using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureController : MonoBehaviour
{
    [SerializeField] GameObject explodeStructure;
    [SerializeField] AudioSource explodeSound;

    void Start()
    {
        gameObject.SetActive(true);
    }

    public void Explode()
    {
        gameObject.SetActive(false);
        explodeSound.Play();
        GameObject explodeObj = Instantiate(explodeStructure, transform.position, transform.rotation) as GameObject;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 5.0f);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(500.0f, explosionPos, 5.0f, 3.0f);
            }
        }
        Destroy(explodeObj, 4);
    }
}
