#if !ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RAY_Core
{
    public class ViewMessageBox : BaseView
    {
        [BoxGroup("General")]
        [SerializeField][Required] private protected TMP_Text content;
        [SerializeField][Required] private protected Button buttonExit;
        [SerializeField][Required] private protected GameObject _object;

        private BaseViewVirtualCamera _virtualCamera;

        private Action<ViewMessageBox> defaultExitEvent { get; } = u =>
        {
            u.Show(false);
            u.EnableIO(false);
        };

        private protected override void __Show()
        {
            _object.SetActive(true);
        }
        private protected override void __Hide()
        {
            _object.SetActive(false);
        }
        public void ShowMessage(string text, Action<ViewMessageBox> exitEvent)
        {
            _virtualCamera = BaseCameraSystem.Instance.CurrentVirtualCamera;

            Show(true);
            EnableIO(true);
            
            content.SetText(text);

            buttonExit.onClick.RemoveAllListeners();
            buttonExit.onClick.AddListener(exitEvent != default ? () => exitEvent?.Invoke(this) : () => defaultExitEvent?.Invoke(this));
        }
        private protected override void __EnableIO()
        {
            BaseCameraSystem.Instance.ChangeVirtualCamera(TypeVirtualCamera.None);

            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewTV, out var viewTV))
            {
                viewTV.EnableIO(false);
            }
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewNPC, out var viewNPC))
            {
                viewNPC.EnableIO(false);
            }
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewGhost, out var viewGhostNPC))
            {
                viewGhostNPC.EnableIO(false);
            }
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewMainCharacter, out var viewMainCharacter))
            {
                viewMainCharacter.EnableIO(false);
            }
        }
        private protected override void __DisableIO()
        {
            BaseCameraSystem.Instance.ChangeVirtualCamera(_virtualCamera);
            
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewTV, out var viewTV))
            {
                viewTV.EnableIO(true);
            }
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewNPC, out var viewNPC))
            {
                viewNPC.EnableIO(true);
            }
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewGhost, out var viewGhostNPC))
            {
                viewGhostNPC.EnableIO(true);
            }
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewMainCharacter, out var viewMainCharacter))
            {
                viewMainCharacter.EnableIO(true);
            }
        }
    }
}
#endif