using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InteractCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject interactSpot;
    [SerializeField] Camera interactPersonCamera;

    [SerializeField] TextMeshProUGUI instructionText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (interactPersonCamera.enabled)
        {
            player.transform.position = interactSpot.transform.position;
            player.transform.rotation = interactSpot.transform.rotation;
        }
        transform.LookAt(player.transform);
    }
}
