#if ENABLE_ERRORS

using RAY_Core;
using RAY_Lesson7;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Lesson7
{
    public class ApplicationGameEntry : BaseApplicationEntry<ApplicationGameEntry>
    {
        private protected override void OnInit()
        {
            //GameStorage.Instance.StateMachine.SetState("ContextMessageBox");
        }
        private protected override void OnDispose()
        {

        }
    }
}
#endif