using Fungus;
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
    public class ViewMainMenu : BaseView
    {
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
        private protected override void __Show()
        {
            _gameObject.gameObject.SetActive(true);
            
            BaseCameraSystem.Instance.ChangeVirtualCamera(TypeVirtualCamera.CameraMenu);

            SetCallbackButtonGameStart(StartGame);
            SetCallbackButtonSettings(StartSettings);
            SetCallbackButtonExit(StartExit);
        }
        private void StartGame()
        {
            Show(false);
            EnableIO(false);

            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewMessageBox, out var viewMessageBox))
            {
                (viewMessageBox as ViewMessageBox).ShowMessage("бяел днапю", u => 
                {
                    u.Show(false);
                    u.EnableIO(false);

                    if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewMainCharacter, out var viewCharacter))
                    {
                        (viewCharacter as ViewMainCharacter).StartDialoge();
                    }
                });
            }

            //if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewMainCharacter, out var viewCharacter))
            //{
            //    (viewCharacter as ViewMainCharacter).EnableIO(true);
            //    (viewCharacter as ViewMainCharacter).BindingVirtualCamera();
            //}
            //if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewTV, out var viewTV))
            //{
            //    (viewTV as ViewTV).EnableIO(true);
            //}
            //if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewNPC, out var viewNPC))
            //{
            //    (viewNPC as ViewNPC).EnableIO(true);
            //}
            //if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewNPC, out var viewGhostNPC))
            //{
            //    (viewGhostNPC as ViewGhostNPC).EnableIO(true);
            //}
        }
        private void StartSettings()
        {
            Show(false);
            EnableIO(false);

            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewSettingsDefault, out var view))
            {
                ViewSettings settings = (ViewSettings)view;

                settings.LastView = this;

                settings.Show(true);
                settings.EnableIO(true);
            }
        }
        private void StartExit()
        {
            Show(false);
            EnableIO(false);

            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewHelp, out var view))
            {
                ViewHelp help = (ViewHelp)view;

                help.LastView = this;

                help.Show(true);
                help.EnableIO(true);
            }
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
