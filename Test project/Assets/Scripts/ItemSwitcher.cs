using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemSwitcher : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    [SerializeField] GameObject spawnLocation, currentObject;
    Keyboard keyboard;

    // Start is called before the first frame update
    void Start()
    {
        keyboard = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        if(keyboard != null)
        {
            if (keyboard.digit1Key.wasPressedThisFrame)
            {
                Destroy(currentObject);
                currentObject = Instantiate(items[0], spawnLocation.transform);
            }
            if (keyboard.digit2Key.wasPressedThisFrame)
            {
                Destroy(currentObject);
                currentObject = Instantiate(items[1], spawnLocation.transform);
            }
        }
    }
}
