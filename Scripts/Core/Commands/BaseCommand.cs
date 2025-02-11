using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public interface IBaseCommand
    {
        public void Execute();
    }
    public interface IBaseCommandMachineAnimator
    {
        public void Execute(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
    }
}
