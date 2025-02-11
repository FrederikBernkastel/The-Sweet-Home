using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseCoreObject
    {
        private static int instanceID = 0;

        public int InstanceID { get; } = instanceID++;
        public virtual string Name => default;

        public bool IsInited { get; private set; } = false;
        public bool IsDisposed { get; private set; } = false;

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
                IsDisposed = false;

                return initEvent?.Invoke() ?? true;
            }

            return true;
        }
        public virtual bool OnDispose(Func<bool> disposeEvent)
        {
            if (IsInited && !IsDisposed)
            {
                HelperLog.Log(Name ?? GetType().Name, LogType.Dispose);

                IsInited = false;
                IsDisposed = true;

                return disposeEvent?.Invoke() ?? true;
            }

            return true;
        }
    }
}
