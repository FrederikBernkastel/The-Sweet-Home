using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RAY_CuteHome
{
    public interface IViewMainMenu
    {
        public void SetCallbackButtonGameStart(UnityAction unityAction);
        public void SetCallbackButtonSettings(UnityAction unityAction);
        public void SetCallbackButtonExit(UnityAction unityAction);
    }
    public class ViewMainMenu : BaseView, IViewMainMenu
    {
        public override string Name { get; } = "ViewMainMenu";

        [BoxGroup("General")]
        [SerializeField][Required] private protected GameObject _gameObject;
        [SerializeField][Required] private protected Button buttonGameStart;
        [SerializeField][Required] private protected Button buttonSettings;
        [SerializeField][Required] private protected Button buttonExit;

        private protected override void __Hide()
        {
            _gameObject.gameObject.SetActive(false);

            buttonGameStart.onClick.RemoveAllListeners();
            buttonSettings.onClick.RemoveAllListeners();
            buttonExit.onClick.RemoveAllListeners();
        }
        private protected override void __OnInit()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewMainMenuDefault] ??= this;
        }
        private protected override void __OnDispose()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewMainMenuDefault] = default;
        }
        private protected override void __Show()
        {
            _gameObject.gameObject.SetActive(true);

            var mainCamera = (IViewCamera)BaseMainStorage.MainStorage.PairView[TypeView.ViewMainCamera];

            mainCamera.ChangeVirtualCamera(TypeVirtualCamera.CameraMenu);

            SetCallbackButtonGameStart(() =>
            {
                Show(false);
                EnableIO(false);

                var mainCamera = (IViewCamera)BaseMainStorage.MainStorage.PairView[TypeView.ViewMainCamera];

                mainCamera.ChangeVirtualCamera(TypeVirtualCamera.CameraMainCharacter);
            });
            SetCallbackButtonSettings(() => 
            {
                Show(false);
                EnableIO(false);

                var viewSettings = BaseMainStorage.MainStorage.PairView[TypeView.ViewSettingsDefault];

                (viewSettings as IViewSettings).LastView = this;

                viewSettings.Show(true);
                viewSettings.EnableIO(true);
            });
            SetCallbackButtonExit(() => 
            {
                Show(false);
                EnableIO(false);

                var viewHelp = BaseMainStorage.MainStorage.PairView[TypeView.ViewHelp];

                (viewHelp as IViewHelp).LastView = this;

                viewHelp.Show(true);
                viewHelp.EnableIO(true);
            });
        }
        public void SetCallbackButtonGameStart(UnityAction unityAction)
        {
            if (unityAction != default)
            {
                buttonGameStart.onClick.RemoveAllListeners();

                buttonGameStart.onClick.AddListener(unityAction);
            }
        }
        public void SetCallbackButtonSettings(UnityAction unityAction)
        {
            if (unityAction != default)
            {
                buttonSettings.onClick.RemoveAllListeners();

                buttonSettings.onClick.AddListener(unityAction);
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
