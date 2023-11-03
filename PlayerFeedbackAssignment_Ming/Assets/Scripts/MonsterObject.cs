using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObject : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] AudioSource screaming;
    [SerializeField] Camera playerCamera;
    [SerializeField] Camera endGameCamera;

    bool isSoundPlayed = false;
    float distance;
    public bool isEnd = false;

    void Update()
    {
        if (!isSoundPlayed)
        {
            isSoundPlayed = true;
            screaming.Play();
            isEnd = true;
            StartCoroutine(EndGame());
        }
        distance = (player.transform.position - transform.position).magnitude;
        if(distance > 2.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, .19f);
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(4);
        playerCamera.enabled = false;
        endGameCamera.enabled = true;
        yield return new WaitForSeconds(5);
        Application.Quit();
    }
}
