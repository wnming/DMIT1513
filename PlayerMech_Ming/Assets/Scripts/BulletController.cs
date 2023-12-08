using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] string bulletName;

    [SerializeField] GameObject paticleExplode;

    [SerializeField] int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Structure" || other.gameObject.tag == "Enemy")
        {
            //Instantiate(explodeSound, other.transform.position, other.transform.rotation);
            //explodeSound.Play();
        }
        if (other.gameObject.tag == "Structure" && bulletName == "Orange")
        {
            HealthScript health = other.GetComponent<HealthScript>();
            health.ApplyDamage(damage);
            if(health.currentHealth <= 0)
            {
                StructureController structure = other.GetComponent<StructureController>();
                structure.Explode();
                Instantiate(paticleExplode, structure.transform.position, structure.transform.rotation);
                gameObject.SetActive(false);
            }
        }
    }
}
