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
    public sealed class ViewHelp : BaseViewUI
    {
        [BoxGroup("General")]
        [SerializeField][Required] private GameObject _gameObject;
        [BoxGroup("General")]
        [SerializeField][Required] private Button buttonYes;
        [BoxGroup("General")]
        [SerializeField][Required] private Button buttonNo;

        public BaseViewUI LastView { get; set; } = default;

        private protected override void OnHide()
        {
            _gameObject.gameObject.SetActive(false);

            buttonYes.onClick.RemoveAllListeners();
            buttonNo.onClick.RemoveAllListeners();
        }
        private protected override void OnShow()
        {
            _gameObject.gameObject.SetActive(true);

            SetCallbackButtonYes(() =>
            {
                HelperApplication.ApplicationQuit(true);
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
