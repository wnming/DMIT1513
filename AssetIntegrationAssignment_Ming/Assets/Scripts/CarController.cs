using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject dropOffLocation;

    private Rigidbody player;

    private NavMeshAgent car;

    void Start()
    {
        car = GetComponent<NavMeshAgent>();
        player = playerController.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isCarCollider)
        {
            car.SetDestination(dropOffLocation.transform.position);
            player.transform.position = car.transform.position;
            player.transform.rotation = car.transform.rotation;
            if (!car.pathPending)
            {
                if (car.remainingDistance <= car.stoppingDistance)
                {
                    if (!car.hasPath || car.velocity.sqrMagnitude == 0f)
                    {
                        playerController.isCarCollider = false;
                    }
                }
            }
        }
    }
}
