using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EnermyScript : MonoBehaviour
{
    [SerializeField] GameObject explosionParticle;
    [SerializeField] GameObject explodeZombie;
    AudioSource explosionSource;
    void Start()
    {
        gameObject.SetActive(true);
        explosionSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            explosionSource.Play();
            Death();
        }
    }
    void Death()
    {
        gameObject.SetActive(false);
        GameObject explodeZombieObj = Instantiate(explodeZombie, transform.position, transform.rotation) as GameObject;
        if (explosionParticle != null)
        {
            Instantiate(explosionParticle, transform.position, transform.rotation);
        }
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
        explosionSource.Stop();
        Destroy(explodeZombieObj, 4);
    }
}
