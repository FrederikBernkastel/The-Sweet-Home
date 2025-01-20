using RAY_Core;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public class MainState : BaseState
    {
        //private StateMachine dadAllowMachine;

        private protected override void __OnInit()
        {
            //dadAllowMachine = new("DADAllowMachine",
            //    new KeyValuePair<string, BaseState>("Default", new StateDefault()),
            //    new KeyValuePair<string, BaseState>("Active", new StateAllowDragAndDrop()));

            //dadAllowMachine.OnInit();
        }
        private protected override void __OnEnter()
        {
            //dadAllowMachine.SetState("Active");
        }
        private protected override bool __OnUpdate()
        {
            //dadAllowMachine.OnUpdate();

            return false;
        }
        private protected override void __OnExit()
        {
            //dadAllowMachine.SetState();
        }
    }
}
