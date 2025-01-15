#if ENABLE_ERRORS

using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseViewUI : BaseCoreObjectBehaviour, IBaseView
    {
        [BoxGroup("OnInit")]
        [SerializeField] private protected bool isAuto;
        [BoxGroup("OnInit")]
        [SerializeField] private protected bool isVisible;
        [BoxGroup("OnInit")]
        [SerializeField] private protected bool isInteractable;

        [BoxGroup("ListViewUI")]
        [SerializeField] private protected BaseViewUI[] listViewUI;

        public bool IsVisible { get; private protected set; } = false;
        public bool IsInteractable { get; private protected set; } = false;

        private protected override void _OnInit()
        {
            LogSystem.Log(Name, LogSystem.LogType.Init);



            if (isAuto)
            {
                IsVisible = !isVisible;
                Show(isVisible);

                IsInteractable = !isInteractable;
                EnableIO(isInteractable);
            }

            __OnInit();
        }
        private protected override void _OnDispose()
        {
            LogSystem.Log(Name, LogSystem.LogType.Dispose);

            __OnDispose();
        }
        public void EnableIO(bool flag)
        {
            if (flag)
            {
                if (!IsInteractable)
                {
                    __EnableIO();

                    IsInteractable = true;
                }
            }
            else
            {
                if (IsInteractable)
                {
                    __DisableIO();

                    IsInteractable = false;
                }
            }
        }
        public void Show(bool flag)
        {
            if (flag)
            {
                if (!IsVisible)
                {
                    __Show();

                    IsVisible = true;
                }
            }
            else
            {
                if (IsVisible)
                {
                    __Hide();

                    IsVisible = false;
                }
            }
        }

        private protected virtual void __OnInit() { }
        private protected virtual void __OnDispose() { }
        private protected virtual void __EnableIO() { }
        private protected virtual void __DisableIO() { }
        private protected virtual void __Show() { }
        private protected virtual void __Hide() { }
    }
}
#endif