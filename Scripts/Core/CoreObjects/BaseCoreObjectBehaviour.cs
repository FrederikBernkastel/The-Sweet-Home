using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseCoreObjectBehaviour : MonoBehaviour
    {
        private static int instanceID { get; set; } = 0;

        public int InstanceID { get; } = instanceID++;
        public virtual string Name => default;

        public bool IsInited { get; private set; } = false;
        public bool IsDisposed { get; private set; } = false;
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

                IsInited = true;
                IsStarted = false;
                IsDisposed = false;

                return initEvent?.Invoke() ?? true;
            }

            return true;
        }
        public virtual void OnStart(Action startEvent)
        {
            if (IsInited && !IsStarted)
            {
                HelperLog.Log(Name ?? GetType().Name, LogType.Start);

                IsInited = true;
                IsStarted = true;
                IsDisposed = false;

                startEvent?.Invoke();
            }
        }
        public virtual bool OnDispose(Func<bool> disposeEvent)
        {
            if (IsStarted && !IsDisposed)
            {
                HelperLog.Log(Name ?? GetType().Name, LogType.Dispose);

                IsInited = false;
                IsStarted = false;
                IsDisposed = true;

                return disposeEvent?.Invoke() ?? true;
            }

            return true;
        }
    }
}
