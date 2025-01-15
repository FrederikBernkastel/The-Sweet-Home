using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreePlayerCamera : APlayerCamera
{
    [SerializeField] private FreePlayerManager playerManager;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerManager firstPlayerManager;

    public override void StartCamera()
    {
        playerManager.OnAudio();
        playerManager.OnMovement();

        virtualCamera.enabled = true;

        mainCamera.cullingMask = layerMask.value;

        playerManager.SetPosition(firstPlayerManager.GetPosition());
    }
    public override void StopCamera()
    {
        playerManager.OffAudio();
        playerManager.OffMovement();

        virtualCamera.enabled = false;
    }
}
