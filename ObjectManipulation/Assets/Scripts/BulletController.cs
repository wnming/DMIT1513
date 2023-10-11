using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    int damage = 10;
    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null && collision.gameObject.tag != "Player")
        {
            gameObject.SetActive(false);
            health.ApplyDamage(damage);
        }
    }
}
