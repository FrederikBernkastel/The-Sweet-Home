#if ENABLE_ERRORS

using NaughtyAttributes;
using NaughtyAttributes.Test;
using PracticeProject_Lesson7;
using RAY_Core;
using RAY_Cossacks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RAY_Core
{
    public class MessageBoxState : BaseState
    {
        public override string NameState => "MessageBox";

        public string Message { get; set; }
        public UnityAction ExitAction { get; set; }
        public ViewMessageBox ViewMessageBox { get; set; }

        private protected override void Init()
        {
            ViewMessageBox.Show(false);
        }
        private protected override void Enter()
        {
            ViewMessageBox.Show(true);

            ViewMessageBox.SetDefaultSize();

            ViewMessageBox.SetMessage(Message);

            ViewMessageBox.SetClickButton(ExitAction);
        }
        private protected override void Exit()
        {
            ViewMessageBox.Show(false);
        }
    }
}
#endif