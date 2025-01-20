using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreePlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerController controllerPlayer;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private AudioListener audioListenerPlayer;
    [SerializeField] private Rigidbody rb;

    public void SetPosition(Vector3 position)
    {
        rb.position = position;
    }
    public void OnMovement()
    {
        controllerPlayer.enabled = true;
    }
    public void OffMovement()
    {
        controllerPlayer.enabled = false;
    }
    public void OnCamera()
    {
        virtualCamera.enabled = true;
    }
    public void OffCamera()
    {
        virtualCamera.enabled = false;
    }
    public void OnAudio()
    {
        audioListenerPlayer.enabled = true;
    }
    public void OffAudio()
    {
        audioListenerPlayer.enabled = false;
    }
}
