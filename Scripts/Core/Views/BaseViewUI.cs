#if !ENABLE_ERRORS

using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseViewUI : BaseCoreObjectBehaviour
    {
        [BoxGroup("OnInit")]
        [SerializeField] private protected bool isVisible;
        [BoxGroup("OnInit")]
        [SerializeField] private protected bool isInteractable;

        [BoxGroup("General")]
        [SerializeField] private protected TypeViewUI typeView;

        public bool IsVisible { get; private protected set; } = false;
        public bool IsInteractable { get; private protected set; } = false;

        public TypeViewUI TypeView => typeView;

        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() =>
            {
                CreateGlobalInstance();

                foreach (var s in GetComponentsInChildren<BaseViewUI>())
                {
                    s.OnInit(default);
                }

                IsVisible = !isVisible;
                Show(isVisible);

                IsInteractable = !isInteractable;
                EnableIO(isInteractable);

                return initEvent?.Invoke() ?? true;
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() =>
            {
                var flag = disposeEvent?.Invoke() ?? true;

                foreach (var s in GetComponentsInChildren<BaseViewUI>())
                {
                    s.OnDispose(default);
                }

                RemoveGlobalInstance();

                return flag;
            });
        }

        public void Show(bool flag)
        {
            if (flag)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
        public void Show()
        {
            if (!IsVisible)
            {
                OnShow();

                IsVisible = true;
            }
        }
        public void Hide()
        {
            if (IsVisible)
            {
                OnHide();

                IsVisible = false;
            }
        }

        public void EnableIO(bool flag)
        {
            if (flag)
            {
                EnableIO();
            }
            else
            {
                DisableIO();
            }
        }
        public void EnableIO()
        {
            if (!IsInteractable)
            {
                OnEnableIO();

                IsInteractable = true;
            }
        }
        public void DisableIO()
        {
            if (IsInteractable)
            {
                OnDisableIO();

                IsInteractable = false;
            }
        }

        public virtual BaseCustomAnimation StartAnimation<T>() where T : BaseCustomAnimation => default;
        public virtual bool IsAvailableAnimation<T>() where T : BaseCustomAnimation => false;

        private protected virtual void OnShow() { }
        private protected virtual void OnHide() { }

        private protected virtual void OnEnableIO() { }
        private protected virtual void OnDisableIO() { }

        public virtual Transform GetTransform() => transform;

        private void CreateGlobalInstance()
        {
            if (typeView != TypeViewUI.Ignore)
            {
                GraphicsSystem.Instance.AddViewUI(typeView, this);
            }
        }
        private void RemoveGlobalInstance()
        {
            if (typeView != TypeViewUI.Ignore)
            {
                GraphicsSystem.Instance.RemoveViewUI(typeView);
            }
        }
    }
}
#endif