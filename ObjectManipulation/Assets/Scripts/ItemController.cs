using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject map;
    [SerializeField] GameObject player;

    [SerializeField] GameObject npc;

    float velocity = 20.0f;

    bool isFirstTime;

    // Start is called before the first frame update
    void Start()
    {
        map.SetActive(false);
        isFirstTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(npc.GetComponent<NPCController>().isPressT);
        if(enemies.ToList().Where(x => x.activeSelf == false).Count() >= 1 && npc.GetComponent<NPCController>().isPressT)
        {
            map.SetActive(true);
            if (isFirstTime)
            {
                isFirstTime = false;
                map.transform.LookAt(new Vector3(player.transform.position.x + 2, player.transform.position.y + 3, player.transform.position.z + 3));
            }
        }
    }
}
