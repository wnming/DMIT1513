using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    bool isFlickering = false;
    float delayTime;
    [SerializeField] GameObject light;
    [SerializeField] AudioSource lightSwitchingSound;
    public MonsterObject monsterObject;

    void Update()
    {
        if (!isFlickering && !monsterObject.isEnd)
        {
            StartCoroutine(FlickeringLight());
        }
        else
        {
            if (monsterObject.isEnd)
            {
                TurnAllTheLights();
            }
        }
    }

    void TurnAllTheLights()
    {
        light.GetComponent<Renderer>().materials[1].color = new Color(1.0f, 0.96f, 0, 1.0f);
        gameObject.GetComponent<Light>().enabled = true;
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        if(lightSwitchingSound != null)
        {
            lightSwitchingSound.Stop();
        }
        light.GetComponent<Renderer>().materials[1].color = new Color(0.33f, 0.33f, 0.33f, 1.0f);
        gameObject.GetComponent<Light>().enabled = false;
        delayTime = Random.Range(0.2f, 0.8f);
        yield return new WaitForSeconds(delayTime);
        if (lightSwitchingSound != null)
        {
            lightSwitchingSound.Play();
        }
        light.GetComponent<Renderer>().materials[1].color = new Color(1.0f, 0.96f, 0, 1.0f);
        gameObject.GetComponent<Light>().enabled = true;
        delayTime = Random.Range(0.08f, 0.5f);
        yield return new WaitForSeconds(delayTime);
        isFlickering = false;
    }
}
