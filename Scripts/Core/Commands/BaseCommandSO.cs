using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseCommandSO : ScriptableObject, IBaseCommand
    {
        public abstract void Execute();
    }
    public abstract class BaseCommandMachineAnimatorSO : BaseCommandSO, IBaseCommandMachineAnimator
    {
        public abstract void Execute(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
    }
}
