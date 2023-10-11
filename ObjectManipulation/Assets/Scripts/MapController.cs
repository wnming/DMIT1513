using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField] GameObject indicatorMap;
    [SerializeField] GameObject item;

    [SerializeField] GameObject treasure;
    [SerializeField] TextMeshProUGUI itemText;

    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject player;

    [SerializeField] GameObject npc;

    float velocity = 20.0f;

    bool isFirstTime;

    // Start is called before the first frame update
    void Start()
    {
        indicatorMap.SetActive(false);
        treasure.SetActive(false);
        gameObject.GetComponent<Renderer>().enabled = false;
        isFirstTime = true;
    }

    void Update()
    {
        if (enemies.ToList().Where(x => x.activeSelf == false).Count() >= 3 && npc.GetComponent<NPCController>().isPressT)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            if (isFirstTime)
            {
                isFirstTime = false;
                gameObject.transform.position = new Vector3(player.transform.position.x + 1, player.transform.position.y + 1, player.transform.position.z + 1);
                gameObject.transform.LookAt(new Vector3(player.transform.position.x + 2, player.transform.position.y + 2, player.transform.position.z + 2));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            indicatorMap.SetActive(true);
            if(itemText.text.ToLower() == "no items")
            {
                itemText.text = "Map|";
            }
            else
            {
                itemText.text += "Map|";
            }
            item.SetActive(true);
            treasure.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
