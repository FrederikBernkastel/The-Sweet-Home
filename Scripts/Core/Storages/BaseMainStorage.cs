#if !ENABLE_ERRORS

using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseMainStorage : BaseStorage
    {
        [BoxGroup("Resources")]
        [SerializeField] private protected ContextResourcesField[] listContextResources;

        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                foreach (var s in listContextResources)
                {
                    s.Execute();
                }

                return initEvent?.Invoke() ?? true;
            });
        }
    }
}
#endif