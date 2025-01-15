#if ENABLE_ERRORS

using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseOrderState : BaseState
    {
        private KeyValuePair<StateMachine, string>[] listStateMachine;

        private protected override void _OnInit()
        {
            listStateMachine = GetListStateMachine();

            foreach (var s in listStateMachine)
            {
                s.Key.OnInit();
            }
        }
        private protected override void _OnGUI()
        {
            foreach (var s in listStateMachine)
            {
                s.Key.OnGUI();
            }
        }
        private protected override void _OnDrawGizmos()
        {
            foreach (var s in listStateMachine)
            {
                s.Key.OnDrawGizmos();
            }
        }
        private protected override void _OnEnter()
        {
            foreach (var s in listStateMachine)
            {
                s.Key.SetState(s.Value);
            }
        }
        private protected override void _OnExit()
        {
            foreach (var s in listStateMachine)
            {
                s.Key.ExitMachine();
            }
        }
        private protected override void _OnUpdate()
        {
            foreach (var s in listStateMachine)
            {
                s.Key.OnUpdate();
            }
        }
        private protected override void _OnFixedUpdate()
        {
            foreach (var s in listStateMachine)
            {
                s.Key.OnFixedUpdate();
            }
        }
        public abstract KeyValuePair<StateMachine, string>[] GetListStateMachine();
        private protected override void _OnDispose()
        {
            foreach (var s in listStateMachine)
            {
                s.Key.Dispose();
            }
        }
    }
}
#endif