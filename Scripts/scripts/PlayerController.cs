using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _movement;
    private float _currentSpeed;

    [SerializeField] private Rigidbody controller;
    [SerializeField] private Camera mainCamera;

    [Space(15)]

    [SerializeField] public float playerMaxSpeedWalk = 2.0f;
    [SerializeField] public float playerMaxSpeedRun = 4.0f;
    [SerializeField] public float jumpHeight = 1.0f;

    [Space(15)]

    [SerializeField] public InputActionReference inputMovement;
    [SerializeField] public InputActionReference inputJump;
    [SerializeField] public InputActionReference inputRun;

    [Space(15)]

    [SerializeField] public bool freeCamera;

    private void Update()
    {
        _currentSpeed = default;

        _movement = inputMovement.action.ReadValue<Vector2>().normalized;

        if (_movement != Vector2.zero)
        {
            _currentSpeed = playerMaxSpeedWalk;

            if (inputRun.action.IsPressed())
            {
                _currentSpeed = playerMaxSpeedRun;
            }
        }

        if (freeCamera)
        {
            controller.transform.forward = mainCamera.transform.forward;
        }
        else
        {
            if (inputJump.action.WasPressedThisFrame())
            {
                controller.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }

            controller.transform.forward = new Vector3(mainCamera.transform.forward.x, controller.transform.forward.y, mainCamera.transform.forward.z);
        }
    }
    private void FixedUpdate()
    {
        if (freeCamera)
        {
            Vector3 forwardRelative = _movement.y * mainCamera.transform.forward;
            Vector3 rightRelative = _movement.x * mainCamera.transform.right;

            Vector3 MoveVector = (forwardRelative + rightRelative) * _currentSpeed * Time.fixedDeltaTime;
            controller.velocity = new Vector3(MoveVector.x, MoveVector.y, MoveVector.z);
        }
        else
        {
            Vector3 forwardRelative = _movement.y * new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z);
            Vector3 rightRelative = _movement.x * new Vector3(mainCamera.transform.right.x, 0, mainCamera.transform.right.z);

            Vector3 MoveVector = (forwardRelative + rightRelative) * _currentSpeed * Time.fixedDeltaTime;
            controller.velocity = new Vector3(MoveVector.x, controller.velocity.y, MoveVector.z);
        }
    }
}
