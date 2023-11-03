using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalfunctioningCamera : MonoBehaviour
{
    float delayTime;
    public bool isSwitching = false;
    [SerializeField] Material malfunctioningCameraMat;

    void Update()
    {
        if (!isSwitching)
        {
            Debug.Log("ss");
            StartCoroutine(OnOffScreen());
        }
    }

    IEnumerator OnOffScreen()
    {
        isSwitching = true;
        gameObject.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 1.0f);
        delayTime = Random.Range(0.2f, 1.1f);
        yield return new WaitForSeconds(delayTime);
        gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        gameObject.GetComponent<Renderer>().material = malfunctioningCameraMat;
        delayTime = Random.Range(0.3f, 2.2f);
        yield return new WaitForSeconds(delayTime);
        isSwitching = false;
    }
}
