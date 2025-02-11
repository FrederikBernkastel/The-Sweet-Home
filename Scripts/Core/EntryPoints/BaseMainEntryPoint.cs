using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseMainEntryPoint
    {
        public virtual void OnInit(ApplicationEntry applicationEntry) { }
        public virtual void OnStart(ApplicationEntry applicationEntry) { }
    }
}
