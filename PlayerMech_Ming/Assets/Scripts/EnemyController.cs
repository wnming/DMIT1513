using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float wanderTimer;
    private float timer;

    private Transform target;
    private NavMeshAgent enemyAgent;

    private float range = 3.0f;

    [SerializeField] GameObject player;

    [SerializeField] GameObject destination1;
    [SerializeField] GameObject destination2;

    [SerializeField] GameObject enemyArms;

    private GameObject currentDestination;


    void OnEnable()
    {
        currentDestination = destination1;
        enemyAgent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        enemyArms.transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (Vector3.Distance(enemyAgent.transform.position, player.transform.position) < range)
        {
            enemyAgent.transform.LookAt(player.transform.position);
            enemyArms.transform.localRotation = Quaternion.Euler(-66.0f, 0, 0);
            //enemyAgent.SetDestination(player.transform.position - new Vector3(0.3f, 0, 0.3f));
        }
        if (timer >= wanderTimer)
        {
            if (Vector3.Distance(enemyAgent.transform.position, destination1.transform.position) < 1)
            {
                currentDestination = destination2;
            }
            if (Vector3.Distance(enemyAgent.transform.position, destination2.transform.position) < 1)
            {
                currentDestination = destination1;
            }
            enemyAgent.SetDestination(currentDestination.transform.position);
            timer = 0;
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
