using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APlayerCamera : MonoBehaviour
{
    public abstract void StartCamera();
    public abstract void StopCamera();
}
public class FirstPlayerCamera : APlayerCamera
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Camera mainCamera;
    
    public override void StartCamera()
    {
        playerManager.OnAnimations();
        playerManager.OnAudio();
        playerManager.OnInteractions();
        playerManager.OnMovement();
        playerManager.OnMovementCamera();

        virtualCamera.enabled = true;

        mainCamera.cullingMask = layerMask.value;
    }
    public override void StopCamera()
    {
        playerManager.OffAnimations();
        playerManager.OffAudio();
        playerManager.OffInteractions();
        playerManager.OffMovement();
        playerManager.OffMovementCamera();

        virtualCamera.enabled = false;
    }
}
