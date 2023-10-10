using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI instructionText;
    [SerializeField] TextMeshProUGUI npcText;

    [SerializeField] GameObject player;

    [SerializeField] Camera firstPersonCamera;
    [SerializeField] Camera thirdPersonCamera;
    [SerializeField] Camera interactPersonCamera;

    float range = 3.3f;

    // Start is called before the first frame update
    void Start()
    {
        //instructionText = GetComponent<TextMeshProUGUI>();
        instructionText.gameObject.SetActive(false);
        npcText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (npcText.gameObject.activeSelf)
                {
                    npcText.text = "";
                    interactPersonCamera.enabled = false;
                    thirdPersonCamera.enabled = true;
                    firstPersonCamera.enabled = false;
                }
                else
                {
                    instructionText.text = "";
                    npcText.gameObject.SetActive(true);
                    interactPersonCamera.enabled = true;
                    thirdPersonCamera.enabled = false;
                    firstPersonCamera.enabled = false;
                }
            }
            instructionText.gameObject.SetActive(true);
        }
        else
        {
            instructionText.gameObject.SetActive(false);
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        Debug.Log("yesy");
    //        instructionText.gameObject.SetActive(true);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        instructionText.gameObject.SetActive(false);
    //    }
    //}

}
