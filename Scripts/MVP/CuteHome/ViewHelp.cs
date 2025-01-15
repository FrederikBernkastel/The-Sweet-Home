using NaughtyAttributes;
using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RAY_CuteHome
{
    public interface IViewHelp
    {
        public BaseView LastView { get; set; }

        public void SetCallbackButtonYes(UnityAction unityAction);
        public void SetCallbackButtonNo(UnityAction unityAction);
    }
    public class ViewHelp : BaseView, IViewHelp
    {
        public override string Name { get; } = "ViewSettings";
        public BaseView LastView { get; set; } = default;

        [BoxGroup("General")]
        [SerializeField][Required] private protected GameObject _gameObject;
        [SerializeField][Required] private protected Button buttonYes;
        [SerializeField][Required] private protected Button buttonNo;

        private protected override void __Hide()
        {
            _gameObject.gameObject.SetActive(false);

            buttonYes.onClick.RemoveAllListeners();
            buttonNo.onClick.RemoveAllListeners();
        }
        private protected override void __OnInit()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewHelp] ??= this;
        }
        private protected override void __OnDispose()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewHelp] = default;

            LastView = default;
        }
        private protected override void __Show()
        {
            _gameObject.gameObject.SetActive(true);

            SetCallbackButtonYes(() =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
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
