using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] InputAction moveAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void Ondisable()
    {
        moveAction.Enable();
    }
}
