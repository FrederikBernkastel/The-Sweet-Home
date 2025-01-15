#if ENABLE_ERRORS

using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RAY_Core
{
    public class ViewMenu : BaseView
    {
        [Header("General")]
        [SerializeField][Required] private Button buttonGame;
        [SerializeField][Required] private Button buttonExit;

        public override string Name => "ViewMenu";

        public void SetClickButtonGame(UnityAction unityAction)
        {
            buttonGame.onClick.RemoveAllListeners();
            buttonGame.onClick.AddListener(unityAction);
        }
        public void SetClickButtonExit(UnityAction unityAction)
        {
            buttonExit.onClick.RemoveAllListeners();
            buttonExit.onClick.AddListener(unityAction);
        }
    }
}
#endif