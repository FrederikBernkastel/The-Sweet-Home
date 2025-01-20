#if !ENABLE_ERRORS

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public interface IBaseState : ICoreObject
    {
        public bool IsNeedRemove { get; }

        public void OnEnter();
        public void OnExit();
        public bool OnUpdate();
        public void OnFixedUpdate();
        public void OnGUI();
        public void OnDrawGizmos();
        public void OnReset();
    }
    public abstract class BaseState : BaseCoreObject, IBaseState
    {
        public bool IsNeedRemove { get; private protected set; } = false;
        public override string Name => default;

        public void OnEnter()
        {
            if (IsInit)
            {
                LogSystem.Log(Name ?? this.GetType().Name, LogType.Enter);

                __OnEnter();
            }
        }
        public void OnExit()
        {
            if (IsInit)
            {
                LogSystem.Log(Name ?? this.GetType().Name, LogType.Exit);

                __OnExit();
            }
        }
        public bool OnUpdate()
        {
            if (IsInit)
            {
                return __OnUpdate();
            }
            else
            {
                return true;
            }
        }
        public void OnFixedUpdate()
        {
            if (IsInit)
            {
                __OnFixedUpdate();
            }
        }
        private protected sealed override void _OnInit()
        {
            LogSystem.Log(Name ?? this.GetType().Name, LogType.Init);

            __OnInit();
        }
        public void OnGUI()
        {
            if (IsInit)
            {
                __OnGUI();
            }
        }
        public void OnDrawGizmos()
        {
            if (IsInit)
            {
                __OnDrawGizmos();
            }
        }
        private protected sealed override void _OnDispose()
        {
            LogSystem.Log(Name ?? this.GetType().Name, LogType.Dispose);

            __OnDispose();
        }
        public void OnReset()
        {
            if (IsInit)
            {
                LogSystem.Log(Name ?? this.GetType().Name, LogType.Reset);

                __OnReset();
            }
        }
        private protected virtual void __OnReset() { }
        private protected virtual void __OnEnter() { }
        private protected virtual void __OnExit() { }
        private protected virtual bool __OnUpdate() => true;
        private protected virtual void __OnFixedUpdate() { }
        private protected virtual void __OnInit() { }
        private protected virtual void __OnGUI() { }
        private protected virtual void __OnDrawGizmos() { }
        private protected virtual void __OnDispose() { }
    }
}
#endif