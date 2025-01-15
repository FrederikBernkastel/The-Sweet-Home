using NaughtyAttributes;
using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RAY_CuteHome
{
    public interface IViewSettings
    {
        public BaseView LastView { get; set; }
        
        public void SetCallbackButtonApply(UnityAction unityAction);
        public void SetCallbackButtonDefault(UnityAction unityAction);
        public void SetCallbackButtonExit(UnityAction unityAction);
    }
    public class ViewSettings : BaseView, IViewSettings
    {
        public override string Name { get; } = "ViewSettings";
        public BaseView LastView { get; set; } = default;

        [BoxGroup("General")]
        [SerializeField][Required] private protected GameObject _gameObject;
        [SerializeField][Required] private protected Button buttonApply;
        [SerializeField][Required] private protected Button buttonDefault;
        [SerializeField][Required] private protected Button buttonExit;

        private protected override void __Hide()
        {
            _gameObject.gameObject.SetActive(false);

            buttonApply.onClick.RemoveAllListeners();
            buttonDefault.onClick.RemoveAllListeners();
            buttonExit.onClick.RemoveAllListeners();
        }
        private protected override void __OnInit()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewSettingsDefault] ??= this;
        }
        private protected override void __OnDispose()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewSettingsDefault] = default;

            LastView = default;
        }
        private protected override void __Show()
        {
            _gameObject.gameObject.SetActive(true);

            SetCallbackButtonApply(() =>
            {
                Debug.Log("Apply");
            });
            SetCallbackButtonDefault(() =>
            {
                Debug.Log("Default");
            });
            SetCallbackButtonExit(() =>
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
        public void SetCallbackButtonApply(UnityAction unityAction)
        {
            if (unityAction != default)
            {
                buttonApply.onClick.RemoveAllListeners();

                buttonApply.onClick.AddListener(unityAction);
            }
        }
        public void SetCallbackButtonDefault(UnityAction unityAction)
        {
            if (unityAction != default)
            {
                buttonDefault.onClick.RemoveAllListeners();

                buttonDefault.onClick.AddListener(unityAction);
            }
        }
        public void SetCallbackButtonExit(UnityAction unityAction)
        {
            if (unityAction != default)
            {
                buttonExit.onClick.RemoveAllListeners();

                buttonExit.onClick.AddListener(unityAction);
            }
        }
    }
}
