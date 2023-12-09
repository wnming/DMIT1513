using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float timer;
    private float attackTime = 2.0f;
    private float timerShoot;
    private float shootAttackTime = 1.2f;

    private Transform target;
    private NavMeshAgent enemyAgent;

    private float range = 3.0f;

    [SerializeField] GameObject player;

    [SerializeField] GameObject destination1;
    [SerializeField] GameObject destination2;

    [SerializeField] GameObject enemyArms;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject endBarrel;

    private GameObject currentDestination;

    public bool isStop;

    public bool isShot;

    void OnEnable()
    {
        isShot = false;
        isStop = false;
        currentDestination = destination1;
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {
            timer += Time.deltaTime;
            timerShoot += Time.deltaTime;
            //enemyArms.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //&& timer >= attackTime
            if (isShot)
            {
                if (timerShoot >= shootAttackTime)
                {
                    enemyAgent.transform.LookAt(player.transform.position);
                    var bullet = Instantiate(bulletPrefab, endBarrel.transform.position, endBarrel.transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.TransformDirection(Vector3.back * 10.0f);

                    timerShoot = 0;
                }
            }
            else
            {
                timerShoot = 0;
                if (Vector3.Distance(enemyAgent.transform.position, player.transform.position) < range && timer >= attackTime)
                {
                    enemyAgent.transform.LookAt(player.transform.position);
                    var bullet = Instantiate(bulletPrefab, endBarrel.transform.position, endBarrel.transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.TransformDirection(Vector3.back * 10.0f);

                    timer = 0;
                    //enemyAgent.SetDestination(player.transform.position - new Vector3(0.3f, 0, 0.3f));
                }
                //enemyArms.transform.localRotation = Quaternion.Euler(0, 0, 0);
                //isBroomDown = enemyArms.transform.localRotation.x == -66.0f ? true : false;
                //if (timer >= wanderTimer)
                //{
                if (Vector3.Distance(enemyAgent.transform.position, destination1.transform.position) < 1)
                {
                    currentDestination = destination2;
                }
                if (Vector3.Distance(enemyAgent.transform.position, destination2.transform.position) < 1)
                {
                    currentDestination = destination1;
                }
                enemyAgent.SetDestination(currentDestination.transform.position);
            }
            
            //    timer = 0;
            //}
        }
        //else
        //{
        //    if (timer >= wanderTimer)
        //    {
        //        if (Vector3.Distance(enemyAgent.transform.position, destination1.transform.position) < 1)
        //        {
        //            currentDestination = destination2;
        //        }
        //        if (Vector3.Distance(enemyAgent.transform.position, destination2.transform.position) < 1)
        //        {
        //            currentDestination = destination1;
        //        }
        //        enemyAgent.SetDestination(currentDestination.transform.position);
        //        timer = 0;
        //    }
        //}
    }
}
