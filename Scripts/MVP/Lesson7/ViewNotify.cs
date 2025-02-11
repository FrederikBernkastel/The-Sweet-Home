#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RAY_Lesson7
{
    public class ViewNotify : BaseView
    {
        [Header("General")]
        [SerializeField][Required] private TMP_Text text;

        public override string Name => "ViewNotify";

        public void SetDescription(string str)
        {
            text.text = str;
        }
    }
}
#endif