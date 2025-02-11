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
    public sealed class ViewMessageBox : BaseViewUI
    {
        [BoxGroup("General")]
        [SerializeField][Required] private TMP_Text content;
        [BoxGroup("General")]
        [SerializeField][Required] private Button buttonExit;
        [BoxGroup("General")]
        [SerializeField][Required] private GameObject _object;

        private protected override void OnShow()
        {
            _object.SetActive(true);
        }
        private protected override void OnHide()
        {
            _object.SetActive(false);
        }

        public void ShowMessage(string text, Action enterEvent, Action<ViewMessageBox> exitEvent)
        {
            enterEvent?.Invoke();

            Show(true);
            EnableIO(true);

            content.SetText(text);

            buttonExit.onClick.RemoveAllListeners();
            buttonExit.onClick.AddListener(exitEvent != default ? () => exitEvent.Invoke(this) : () => DefaultExitEvent.Invoke(this));
        }

        public static Action DefaultEnterEvent => () =>
        {
            foreach (var s in UpdateSystem.Instance.PairContextUpdate)
            {
                UpdateSystem.TempList.Push(new(s.Key, s.Value.IsEnable));
            }
            foreach (var s in IOSystem.Instance.PairContextIO)
            {
                IOSystem.TempList.Push(new(s.Key, s.Value.IsEnable));
            }

            UpdateSystem.Instance.DisableUpdate();
            IOSystem.Instance.DisableIO();
        };
        public static Action<ViewMessageBox> DefaultExitEvent => u =>
        {
            u.Hide();
            u.DisableIO();

            while (UpdateSystem.TempList.TryPop(out var pair))
            {
                if (pair.Value)
                {
                    UpdateSystem.Instance.EnableUpdate(pair.Key);
                }
            }
            while (IOSystem.TempList.TryPop(out var pair))
            {
                if (pair.Value)
                {
                    IOSystem.Instance.EnableIO(pair.Key);
                }
            }
        };
    }
}
#endif