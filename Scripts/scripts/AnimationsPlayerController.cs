using UnityEngine;

public class AnimationsPlayerController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator animator;

    [Space(15)]

    [SerializeField] public string nameParRun;
    [SerializeField] public string nameParWalk;

    private void Update()
    {
        animator.SetBool(nameParWalk, false);
        animator.SetBool(nameParRun, false);

        if (playerController.inputMovement.action.IsPressed())
        {
            animator.SetBool(nameParWalk, true);
        }
        if (playerController.inputRun.action.IsPressed())
        {
            animator.SetBool(nameParRun, true);
        }
    }
}
