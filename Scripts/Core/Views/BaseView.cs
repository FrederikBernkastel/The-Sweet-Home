#if !ENABLE_ERRORS

using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RAY_Core
{
    public abstract class BaseView : BaseCoreObjectBehaviour
    {
        [BoxGroup("OnInit")]
        [SerializeField] private protected bool isVisible;

        [BoxGroup("General")]
        [SerializeField] private protected TypeView typeView;

        public bool IsVisible { get; private protected set; } = false;

        public TypeView TypeView => typeView;

        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                CreateGlobalInstance();

                foreach (var s in GetComponentsInChildren<BaseView>(true).Where(u => u != this))
                {
                    s.OnInit(default);
                }

                IsVisible = !isVisible;
                Show(isVisible);

                return initEvent?.Invoke() ?? true;
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() => 
            {
                var flag = disposeEvent?.Invoke() ?? true;

                foreach (var s in GetComponentsInChildren<BaseView>(true).Where(u => u != this))
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

        public virtual BaseCustomAnimation StartAnimation<T>() where T : BaseCustomAnimation => default;
        public virtual bool IsAvailableAnimation<T>() where T : BaseCustomAnimation => false;

        private protected virtual void OnShow() { }
        private protected virtual void OnHide() { }

        public virtual Transform GetTransform() => transform;

        private protected virtual void CreateGlobalInstance()
        {
            if (typeView != TypeView.Ignore)
            {
                GraphicsSystem.Instance.AddView(typeView, this);
            }
        }
        private protected virtual void RemoveGlobalInstance()
        {
            if (typeView != TypeView.Ignore)
            {
                GraphicsSystem.Instance.RemoveView(typeView);
            }
        }
    }
}
#endif