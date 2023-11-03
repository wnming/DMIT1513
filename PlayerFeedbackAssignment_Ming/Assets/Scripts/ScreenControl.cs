using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenControl : MonoBehaviour
{
    [SerializeField] GameObject malfunctioningCamera;
    [SerializeField] GameObject functioningCamera;
    [SerializeField] GameObject switchCamera;
    [SerializeField] GameObject player;
    [SerializeField] Canvas switchCameraCanvas;
    [SerializeField] AudioSource crashAudio;
    public MalfunctioningCamera malfunctioning;

    float range = 5.5f;

    bool isSwitching = false;
    float delayTime;

    private RaycastHit hit;
    private Ray ray;
    public bool isEndGame = false;
    public bool isActivateMonsterController = false;
    private bool isPlayMulfunctioning = true;
    private bool isPlayCrash = true;
    private bool isOkToSwitch = true;

    private void Start()
    {
        switchCameraCanvas.gameObject.SetActive(false);
        malfunctioningCamera.SetActive(true);
        functioningCamera.SetActive(false);
    }


    void Update()
    {
        if (Vector3.Distance(switchCamera.transform.position, player.transform.position) < range)
        {
            switchCameraCanvas.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0) && isOkToSwitch)
            {
                isOkToSwitch = false;
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.name == "SwitchCamera")
                    {
                        if (malfunctioningCamera.activeSelf)
                        {
                            malfunctioningCamera.SetActive(false);
                            functioningCamera.SetActive(true);
                            if (isEndGame)
                            {
                                if (isPlayCrash)
                                {
                                    isActivateMonsterController = true;
                                    crashAudio.Play();
                                    isPlayCrash = false;
                                }
                            }
                        }
                        else
                        {
                            isEndGame = true;
                            functioningCamera.SetActive(false);
                            malfunctioningCamera.SetActive(true);
                            malfunctioning.isSwitching = false;
                        }

                        //if (gameObject.GetComponent<Renderer>().material.name.Contains("Malfunctioning"))
                        //{
                        //    isPlayMulfunctioning = false;
                        //    if (isEndGame)
                        //    {
                        //        if (isPlayCrash)
                        //        {
                        //            isActivateMonsterController = true;
                        //            crashAudio.Play();
                        //            isPlayCrash = false;
                        //        }
                        //    }
                        //    gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        //    gameObject.GetComponent<Renderer>().material = functioningCameraMat;
                        //}
                        //else
                        //{
                        //    //Debug.Log(gameObject.GetComponent<Renderer>().material.name);
                        //    isEndGame = true;
                        //    isPlayMulfunctioning = true;
                        //}
                    }
                }
                //float timeSinceLastClick = Time.time - lastClickTime;
                //if (timeSinceLastClick <= doubleClickTime)
                //    Debug.Log("Double click");
                //else
                //    StartCoroutine(CheckClickSwitchCamera());

                //lastClickTime = Time.time;
                isOkToSwitch = true;
            }
            //else
            //{
            //    if (!isSwitching && isPlayMulfunctioning)
            //    {
            //        StartCoroutine(OnOffScreen());
            //    }
            //}
        }
        //else
        //{
        //    switchCameraCanvas.gameObject.SetActive(false);
        //    //&& isPlayMulfunctioning && isOkToSwitch
        //    if (!isSwitching)
        //    {
        //        StartCoroutine(OnOffScreen());
        //    }
        //}
    }
}
