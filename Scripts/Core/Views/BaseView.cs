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
    public abstract class BaseViewDrawing : BaseView
    {
        [BoxGroup("OnInit")]
        [SerializeField] private protected bool isVisible;

        public bool IsVisible { get; private protected set; } = false;

        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() =>
            {
                //foreach (var s in this.GetComponentsInChildren<BaseView>(true).Where(u => u != this))
                //{
                //    s.OnInit(default);
                //}

                return initEvent?.Invoke() ?? true;
            });
        }
        public override void OnStart(Action startEvent)
        {
            base.OnStart(() => 
            {
                //foreach (var s in this.GetComponentsInChildren<BaseView>(true).Where(u => u != this))
                //{
                //    s.OnStart(default);
                //}

                IsVisible = !isVisible;
                Show(isVisible);

                startEvent?.Invoke();
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() =>
            {
                var flag = disposeEvent?.Invoke() ?? true;

                //foreach (var s in GetComponentsInChildren<BaseView>(true).Where(u => u != this))
                //{
                //    s.OnDispose(default);
                //}

                return flag;
            });
        }

        public void Show(bool flag)
        {
            if (flag)
            {
                Show(null);
            }
            else
            {
                Hide(default);
            }
        }
        public virtual void Show(Action showEvent)
        {
            if (!IsVisible)
            {
                IsVisible = true;

                showEvent?.Invoke();
            }
        }
        public virtual void Hide(Action hideEvent)
        {
            if (IsVisible)
            {
                IsVisible = false;

                hideEvent?.Invoke();
            }
        }

        public virtual BaseCustomAnimation StartAnimation<T>() where T : BaseCustomAnimation => default;
        public virtual bool IsAvailableAnimation<T>() where T : BaseCustomAnimation => false;
    }
    public static class RefExtension
    {
        public static T Injection<T>(this T sourceObj, ref T destObj) where T : class
        {
            destObj = sourceObj;

            return sourceObj;
        }
        public static T Injection<T, E>(this T tempObj, Func<T, E> func, ref E destObj) where E : class
        {
            func?.Invoke(tempObj)?.Injection(ref destObj);

            return tempObj;
        }
    }
    public abstract class BaseView : BaseCoreObjectBehaviour
    {
        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                CreateGlobalInstance();

                return initEvent?.Invoke() ?? true;
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() => 
            {
                var flag = disposeEvent?.Invoke() ?? true;

                RemoveGlobalInstance();

                return flag;
            });
        }

        public virtual Transform GetTransform() => transform;

        private protected virtual void CreateGlobalInstance() { }
        private protected virtual void RemoveGlobalInstance() { }
    }
}
#endif