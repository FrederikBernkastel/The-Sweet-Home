#if !ENABLE_ERRORS

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAY_Core
{
    public class StateMachine
    {
        private Dictionary<string, BaseState> dicBaseState { get; }
        public string Name { get; }
        public BaseState CurrentState { get; private set; }

        public bool IsInited { get; private set; } = false;
        public bool IsDisposed { get; private set; } = false;

        public StateMachine(string name, params KeyValuePair<string, BaseState>[] list)
        {
            dicBaseState = new Dictionary<string, BaseState>(list.Length);
            Name = name;

            foreach (var s in list)
            {
                dicBaseState.Add(s.Key, s.Value);
            }
        }
    
        public void OnInit()
        {
            if (!IsInited)
            {
                LogSystem.Log(Name, LogType.Init);

                foreach (var s in dicBaseState.Values)
                {
                    s.OnInit();
                }

                IsInited = true;
            }
        }
        public void OnUpdate()
        {
            CurrentState?.OnUpdate();
        }
        public void OnFixedUpdate()
        {
            CurrentState?.OnFixedUpdate();
        }
        public void SetState(string state)
        {
            if (CurrentState == default || state != CurrentState.Name)
            {
                if (dicBaseState.TryGetValue(state, out var val))
                {
                    CurrentState?.OnExit();

                    CurrentState = val;

                    CurrentState.OnEnter();
                }
            }
        }
        public void SetState()
        {
            CurrentState?.OnExit();

            CurrentState = default;
        }
        public void OnGUI()
        {
            CurrentState?.OnGUI();
        }
        public void OnDrawGizmos()
        {
            CurrentState?.OnDrawGizmos();
        }
        public void Dispose()
        {
            if (!IsDisposed)
            {
                LogSystem.Log(Name, LogType.Dispose);

                foreach (var s in dicBaseState.Values)
                {
                    s.OnDispose();
                }

                IsDisposed = true;
            }
        }
    }
}
#endif