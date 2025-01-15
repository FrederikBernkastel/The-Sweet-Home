#if ENABLE_ERRORS

using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Lesson7
{
    public class ApplicationMenuEntry : BaseApplicationEntry<ApplicationMenuEntry>
    {
        private protected override void OnInit()
        {
            //MenuStorage.Instance.MainStateMachine.SetState("ContextMenu");
        }
        private protected override void OnDispose()
        {
            
        }
    }
}
#endif