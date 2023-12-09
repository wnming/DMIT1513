using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] string bulletName;

    [SerializeField] GameObject paticleExplode;

    [SerializeField] int damage;

    [SerializeField] AudioSource orangeExplode;

    IEnumerator CountDownShot(EnemyController enemy)
    {
        yield return new WaitForSeconds(6);
        enemy.isShot = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(bulletName == "Grey")
            {
                Instantiate(paticleExplode, other.transform.position, other.transform.rotation);
                HealthScript health = other.GetComponent<HealthScript>();
                health.ApplyDamage(damage);
                if (health.currentHealth <= 0)
                {
                    //Instantiate(paticleExplode, other.transform.position, other.transform.rotation);
                    other.gameObject.SetActive(false);
                    gameObject.SetActive(false);
                }
                else
                {
                    EnemyController enemy = other.GetComponent<EnemyController>();
                    enemy.isShot = true;
                    StartCoroutine(CountDownShot(enemy));
                }
            }
            if (bulletName == "Orange")
            {
                orangeExplode.Play();
                Instantiate(paticleExplode, other.transform.position, other.transform.rotation);
                HealthScript health = other.GetComponent<HealthScript>();
                health.ApplyDamage(damage);
                if (health.currentHealth <= 0)
                {
                    //Instantiate(paticleExplode, other.transform.position, other.transform.rotation);
                    other.gameObject.SetActive(false);
                    //gameObject.SetActive(false);
                }
            }
            if (bulletName == "Yellow")
            {
                EnemyController enemy = other.GetComponent<EnemyController>();
                if(!enemy.isStop)
                {
                    enemy.isStop = true;
                    Instantiate(paticleExplode, other.transform.position, other.transform.rotation);
                    StartCoroutine(EnemyMoving(enemy));
                }
            }
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
            }
            gameObject.SetActive(false);
        }
    }

    IEnumerator EnemyMoving(EnemyController enemy)
    {
        yield return new WaitForSeconds(10);
        enemy.isStop = false;
    }
}
