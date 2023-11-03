using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    public ScreenControl screenControl;
    [SerializeField] GameObject smokeParticle;
    [SerializeField] GameObject player;
    [SerializeField] GameObject monster;

    bool isSmoking = false;
    bool isPlayerStillLook = false;
    public bool isShowMonster = false;

    private void Start()
    {
        smokeParticle.SetActive(false);
        monster.SetActive(false);
    }

    private void Update()
    {
        if (screenControl.isActivateMonsterController)
        {
            if (!isSmoking && !isShowMonster)
            {
                isSmoking = true;
                smokeParticle.SetActive(true);
            }
            else
            {
                if (isShowMonster)
                {
                    smokeParticle.SetActive(false);
                }
            }
            if (!isShowMonster)
            {
                if (Physics.Raycast(player.transform.position, player.transform.forward, out RaycastHit hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject.name == "Smoke")
                    {
                        StartCoroutine(DisplayMonster());
                    }
                }
            }
        }
    }

    IEnumerator DisplayMonster()
    {
        yield return new WaitForSeconds(3);
        isShowMonster = true;
        monster.SetActive(true);
    }
}
