using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeCameraController : MonoBehaviour
{
    private int _currentIndex;
    
    [SerializeField] private InputActionReference inputChange;
    [SerializeField] private APlayerCamera[] listCameras;
    
    private void Update()
    {
        if (inputChange.action.triggered)
        {
            listCameras[_currentIndex].StopCamera();
            _currentIndex = _currentIndex + 1 > listCameras.Length - 1 ? default : _currentIndex + 1;
            listCameras[_currentIndex].StartCamera();
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
    public void SetDefaultCamera()
    {
        _currentIndex = default;
        foreach (var s in listCameras)
        {
            s.StopCamera();
        }
        listCameras[_currentIndex].StartCamera();
    }
    public void StopCameras()
    {
        foreach (var s in listCameras)
        {
            s.StopCamera();
        }
    }
}
