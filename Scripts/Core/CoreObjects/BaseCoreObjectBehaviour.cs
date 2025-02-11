using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseCoreObjectBehaviour : MonoBehaviour
    {
        private static int instanceID = 0;

        public int InstanceID { get; } = instanceID++;
        public virtual string Name => default;

        public bool IsInited { get; private set; } = false;
        public bool IsDisposed { get; private set; } = true;
        public bool IsStarted { get; private set; } = false;

        public virtual void Reset(Action resetEvent)
        {
            if (IsInited)
            {
                HelperLog.Log(Name ?? GetType().Name, LogType.Reset);

                resetEvent?.Invoke();
            }
        }
        public virtual bool OnInit(Func<bool> initEvent)
        {
            if (!IsInited)
            {
                HelperLog.Log(Name ?? GetType().Name, LogType.Init);

                var flag = initEvent?.Invoke() ?? true;

                IsInited = true;
                IsDisposed = false;

                return flag;
            }

            return true;
        }
        public virtual void OnStart(Action startEvent)
        {
            if (IsInited && !IsStarted)
            {
                HelperLog.Log(Name ?? GetType().Name, LogType.Start);

                startEvent?.Invoke();

                IsInited = true;
                IsStarted = true;
                IsDisposed = false;
            }
        }
        public virtual bool OnDispose(Func<bool> disposeEvent)
        {
            if (IsStarted && !IsDisposed)
            {
                HelperLog.Log(Name ?? GetType().Name, LogType.Dispose);

                var flag = disposeEvent?.Invoke() ?? true;

                IsInited = false;
                IsStarted = false;
                IsDisposed = true;

                return flag;
            }

            return true;
        }
    }
}
