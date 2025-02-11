#if !ENABLE_ERRORS

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseState : BaseCoreObject
    {
        public bool IsEntered { get; private set; } = false;
        public bool IsExited { get; private set; } = false;

        private TypeUpdate typeUpdate { get; set; } = TypeUpdate.None;

        public event Action EnteredEvent = delegate { };
        public event Action ExitedEvent = delegate { };

        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                Reset(default);

                return initEvent?.Invoke() ?? false;
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() => 
            {
                var flag = disposeEvent?.Invoke() ?? true;

                Reset(default);

                return flag;
            });
        }
        public override void Reset(Action resetEvent)
        {
            base.Reset(() => 
            {
                typeUpdate = TypeUpdate.None;

                EnteredEvent = default;
                ExitedEvent = default;

                EnteredEvent = delegate { };
                ExitedEvent = delegate { };

                resetEvent?.Invoke();
            });
        }
        public virtual void OnEnter(Action enterEvent)
        {
            if (IsInited && !IsEntered)
            {
                HelperLog.Log(Name ?? GetType().Name, LogType.Enter);

                enterEvent?.Invoke();

                EnteredEvent.Invoke();

                IsEntered = true;
                IsExited = false;
            }
        }
        public virtual void OnExit(Action exitEvent)
        {
            if (IsInited && IsEntered && !IsExited)
            {
                HelperLog.Log(Name ?? GetType().Name, LogType.Exit);

                exitEvent?.Invoke();

                ExitedEvent.Invoke();

                IsEntered = false;
                IsExited = true;
            }
        }
        public virtual void OnUpdate(Action updateEvent)
        {
            if (IsInited && IsEntered)
            {
                updateEvent?.Invoke();
            }
        }
        public virtual void OnFixedUpdate(Action fixedUpdateEvent)
        {
            if (IsInited && IsEntered)
            {
                fixedUpdateEvent?.Invoke();
            }
        }
        public virtual void OnGUI(Action guiEvent)
        {
            if (IsInited && IsEntered)
            {
                guiEvent?.Invoke();
            }
        }
        public virtual void OnDrawGizmos(Action drawGizmosEvent)
        {
            if (IsInited && IsEntered)
            {
                drawGizmosEvent?.Invoke();
            }
        }

        public bool IsEnableUpdateType(TypeUpdate typeUpdate)
        {
            return (this.typeUpdate & typeUpdate) == typeUpdate;
        }
        public void EnableUpdateType(TypeUpdate typeUpdate, bool isEnable)
        {
            if (isEnable)
            {
                this.typeUpdate |= typeUpdate;
            }
            else
            {
                this.typeUpdate ^= typeUpdate;
            }
        }
        public void EnableUpdateType(TypeUpdate typeUpdate)
        {
            this.typeUpdate |= typeUpdate;
        }
        public void DisableUpdateType(TypeUpdate typeUpdate)
        {
            this.typeUpdate ^= typeUpdate;
        }
        public void DisableUpdateType()
        {
            typeUpdate = TypeUpdate.None;
        }

        public abstract void EnableUpdateType();
    }
}
#endif