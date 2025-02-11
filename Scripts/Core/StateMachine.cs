#if !ENABLE_ERRORS

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAY_Core
{
    public sealed class StateMachine : BaseState
    {
        private Dictionary<string, BaseState> dicBaseState { get; } = new();
        public KeyValuePair<string, BaseState> CurrentState { get; private set; } = default;

        public override void EnableUpdateType()
        {
            EnableUpdateType(TypeUpdate.Update | TypeUpdate.FixedUpdate | TypeUpdate.GUI | TypeUpdate.DrawGizmos);
        }
        public void SetStates(params KeyValuePair<string, BaseState>[] list)
        {
            if (IsInited)
            {
                foreach (var s in list)
                {
                    dicBaseState.Add(s.Key, s.Value);
                }
                foreach (var s in dicBaseState.Values)
                {
                    s.OnInit(default);
                }
            }
        }
        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                Reset(default);

                return true;
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() =>
            {
                Reset(default);

                return true;
            });
        }
        public override void Reset(Action resetEvent)
        {
            base.Reset(() =>
            {
                foreach (var s in dicBaseState.Values)
                {
                    s.OnDispose(default);
                }

                dicBaseState.Clear();

                CurrentState = default;
            });
        }
        public override void OnUpdate(Action updateEvent)
        {
            base.OnUpdate(() =>
            {
                CurrentState.Value?.OnUpdate(default);
            });
        }
        public override void OnFixedUpdate(Action fixedUpdateEvent)
        {
            base.OnFixedUpdate(() =>
            {
                CurrentState.Value?.OnFixedUpdate(default);
            });
        }
        public override void OnGUI(Action guiEvent)
        {
            base.OnGUI(() =>
            {
                CurrentState.Value?.OnGUI(default);
            });
        }
        public override void OnDrawGizmos(Action drawGizmosEvent)
        {
            base.OnDrawGizmos(() =>
            {
                CurrentState.Value?.OnDrawGizmos(default);
            });
        }
        public void SetState(string state)
        {
            if (CurrentState.Key?.Equals(state) ?? true)
            {
                if (dicBaseState.TryGetValue(state, out var val))
                {
                    CurrentState.Value?.OnExit(default);

                    CurrentState = new(state, val);

                    CurrentState.Value?.OnEnter(default);
                }
            }
        }
        public void SetState()
        {
            CurrentState.Value?.OnExit(default);

            CurrentState = default;
        }
    }
}
#endif