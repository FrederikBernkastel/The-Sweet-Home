using Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerController controllerPlayer;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private AudioListener audioListenerPlayer;
    [SerializeField] private SelectedObjectEmission controllerInteractions;
    [SerializeField] private AnimationsPlayerController animationsPlayerController;
    [SerializeField] private Rigidbody rb;

    public void SetFoV(float fov)
    {
        virtualCamera.m_Lens.FieldOfView = fov;
    }
    public Vector3 GetPosition()
    {
        return rb.position;
    }
    public void OnMovementCamera()
    {
        virtualCamera.GetComponent<CinemachineInputProvider>().enabled = true;
    }
    public void OffMovementCamera()
    {
        virtualCamera.GetComponent<CinemachineInputProvider>().enabled = false;
    }
    public void OnAnimations()
    {
        animationsPlayerController.enabled = true;
    }
    public void OffAnimations()
    {
        animationsPlayerController.enabled = false;
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
    public void OnInteractions()
    {
        controllerInteractions.enabled = true;
    }
    public void OffInteractions()
    {
        controllerInteractions.enabled = false;
    }
}
