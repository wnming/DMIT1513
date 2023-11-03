using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenControl : MonoBehaviour
{
    [SerializeField] Material malfunctioningCameraMat;
    [SerializeField] Material functioningCameraMat;
    [SerializeField] GameObject switchCamera;
    [SerializeField] GameObject player;
    [SerializeField] Canvas switchCameraCanvas;
    [SerializeField] AudioSource crashAudio;

    float range = 5.5f;

    bool isSwitching = false;
    float delayTime;

    private RaycastHit hit;
    private Ray ray;
    public bool isEndGame = false;
    public bool isActivateMonsterController = false;
    private bool isPlayMulfunctioning = true;
    private bool isPlayCrash = true;
    float doubleClickTime = 0.2f, lastClickTime;

    private void Start()
    {
        switchCameraCanvas.gameObject.SetActive(false);
    }


    void Update()
    {
        if (Vector3.Distance(switchCamera.transform.position, player.transform.position) < range)
        {
            switchCameraCanvas.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                float timeSinceLastClick = Time.time - lastClickTime;
                if (timeSinceLastClick <= doubleClickTime)
                    Debug.Log("Double click");
                else
                    StartCoroutine(CheckClickSwitchCamera());

                lastClickTime = Time.time;
            }
        }
        else
        {
            switchCameraCanvas.gameObject.SetActive(false);
        }
        if (!isSwitching && isPlayMulfunctioning)
        {
            StartCoroutine(OnOffScreen());
        }
    }

    IEnumerator CheckClickSwitchCamera()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.name == "SwitchCamera")
            {
                isPlayMulfunctioning = false;
                if (gameObject.GetComponent<Renderer>().material.name.Contains("MalfunctioningCamera"))
                {
                    if (isEndGame)
                    {
                        if (isPlayCrash)
                        {
                            isActivateMonsterController = true;
                            crashAudio.Play();
                            isPlayCrash = false;
                        }
                    }
                    gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    gameObject.GetComponent<Renderer>().material = functioningCameraMat;
                }
                else
                {
                    isEndGame = true;
                    isPlayMulfunctioning = true;
                }
            }
        }
        yield return null;
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
