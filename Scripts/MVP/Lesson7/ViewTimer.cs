#if ENABLE_ERRORS

using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static RAY_Core.ViewResources;

namespace RAY_Core
{
    public class ViewTimer : BaseView
    {
        [Header("Ref")]
        [SerializeField][Required] private TMP_Text timerText;
        [SerializeField][Min(0)] private int defaultValue_Seconds;

        private DateTime currentValue;
        private int currentValue_int;

        public override string Name => "ViewTimer";

        private protected override void Awake()
        {
            base.Awake();

            SetValue(defaultValue_Seconds);
        }
        public void SetValue(int val)
        {
            currentValue_int = val < 0 ? 0 : val > 3600 ? 3600 : val;

            currentValue = new DateTime().AddSeconds(currentValue_int);

            timerText.text = currentValue.Minute + ":" + currentValue.Second;
        }
        public void AddValue(int val)
        {
            currentValue_int += val;
            currentValue_int = currentValue_int < 0 ? 0 : currentValue_int > 3600 ? 3600 : currentValue_int;

            SetValue(currentValue_int);
        }
        public void SubValue(int val)
        {
            currentValue_int -= val;
            currentValue_int = currentValue_int < 0 ? 0 : currentValue_int > 3600 ? 3600 : currentValue_int;

            SetValue(currentValue_int);
        }
    }
}
#endif