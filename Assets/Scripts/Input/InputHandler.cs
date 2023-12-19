using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private ActionMap actionMap;
    private void Awake()
    {
        actionMap = new ActionMap();
        actionMap.PlayerMovement.Move.performed += ctx => Debug.Log("Move " + ctx.ReadValue<float>());
        actionMap.PlayerMovement.Move.canceled += ctx => Debug.Log("Move Stopped");
        actionMap.PlayerMovement.Jump.performed += ctx => Debug.Log("Jump");
        actionMap.PlayerMovement.Jump.canceled += ctx => Debug.Log("Jump Stopped");
        actionMap.PlayerMovement.Sprint.performed += ctx => Debug.Log("Sprinting");
        actionMap.PlayerMovement.Sprint.canceled += ctx => Debug.Log("Sprint Canceled");
        actionMap.PlayerMovement.Dash.performed += ctx => Debug.Log("Dash");
        actionMap.PlayerMovement.Slash.performed += ctx => Debug.Log("Attack");
    }

    private void OnEnable()
    {
        actionMap.Enable();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        actionMap.Disable();
    }
}
