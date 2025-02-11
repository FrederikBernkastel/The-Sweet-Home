using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAY_Core
{
    public sealed class StateMachineAnimation : StateMachineBehaviour
    {
        [BoxGroup("Commands")]
        [SerializeField] private BaseCommandMachineAnimatorSO commandEnterSO;
        [SerializeField] private BaseCommandMachineAnimatorSO commandExitSO;

        public IBaseCommandMachineAnimator CommandEnter { get; private set; } = default;
        public IBaseCommandMachineAnimator CommandExit { get; private set; } = default;

        public void SetCommandEnter(IBaseCommandMachineAnimator baseCommand)
        {
            CommandEnter = baseCommand;
        }
        public void SetCommandExit(IBaseCommandMachineAnimator baseCommand)
        {
            CommandExit = baseCommand;
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (commandExitSO != default)
            {
                commandExitSO.Execute(animator, stateInfo, layerIndex);
            }

            if (CommandExit != default)
            {
                CommandExit.Execute(animator, stateInfo, layerIndex);
            }
        }
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (commandEnterSO != default)
            {
                commandEnterSO.Execute(animator, stateInfo, layerIndex);
            }

            if (CommandEnter != default)
            {
                CommandEnter.Execute(animator, stateInfo, layerIndex);
            }
        }
    }
}
