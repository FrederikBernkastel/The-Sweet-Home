using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EscapeController : MonoBehaviour
{
    [SerializeField] private InputActionReference inputEscape;
    [SerializeField] private UIManager uiManager;
    
    private void Update()
    {
        if (inputEscape.action.triggered)
        {
            uiManager.StopGame();
        }
    }
    public void OnController()
    {
        this.enabled = true;
    }
    public void OffController()
    {
        this.enabled = false;
    }
}
