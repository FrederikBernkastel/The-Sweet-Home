#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RAY_Lesson7
{
    public class ViewGame : BaseView
    {
        private Image[] listUI;
        private Button[] listUIButton;

        public override string Name => "ViewGame";

        public void ActivatedUI(bool flag)
        {
            if (flag)
            {
                foreach (var s in listUI)
                {
                    s.raycastTarget = true;
                }
                foreach (var s in listUIButton)
                {
                    s.interactable = true;
                }
            }
            else
            {
                foreach (var s in listUI)
                {
                    s.raycastTarget = false;
                }
                foreach (var s in listUIButton)
                {
                    s.interactable = false;
                }
            }
        }
        private protected override void Awake()
        {
            base.Awake();

            listUI = this.GetComponentsInChildren<Image>();
            listUIButton = this.GetComponentsInChildren<Button>();
        }
    }
}
#endif