using NaughtyAttributes;
using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RAY_CuteHome
{
    public class ViewHelp : BaseView
    {
        [BoxGroup("General")]
        [SerializeField][Required] private protected GameObject _gameObject;
        [SerializeField][Required] private protected Button buttonYes;
        [SerializeField][Required] private protected Button buttonNo;

        public BaseView LastView { get; set; } = default;

        private protected override void __Hide()
        {
            _gameObject.gameObject.SetActive(false);

            buttonYes.onClick.RemoveAllListeners();
            buttonNo.onClick.RemoveAllListeners();
        }
        private protected override void __OnDispose()
        {
            LastView = default;
        }
        private protected override void __Show()
        {
            _gameObject.gameObject.SetActive(true);

            SetCallbackButtonYes(() =>
            {
                Helper.ApplicationQuit(true);
            });
            SetCallbackButtonNo(() =>
            {
                Show(false);
                EnableIO(false);

                if (LastView != default)
                {
                    LastView.Show(true);
                    LastView.EnableIO(true);
                }
            });
        }
        public void SetCallbackButtonYes(UnityAction unityAction)
        {
            if (unityAction != default)
            {
                buttonYes.onClick.RemoveAllListeners();

                buttonYes.onClick.AddListener(unityAction);
            }
        }
        public void SetCallbackButtonNo(UnityAction unityAction)
        {
            if (unityAction != default)
            {
                buttonNo.onClick.RemoveAllListeners();

                buttonNo.onClick.AddListener(unityAction);
            }
        }
    }
}
