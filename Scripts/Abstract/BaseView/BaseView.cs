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
        [SerializeField] private protected bool isAuto;
        [BoxGroup("OnInit")]
        [SerializeField] private protected bool isVisible;
        [BoxGroup("OnInit")]
        [SerializeField] private protected bool isInteractable;

        [BoxGroup("General")]
        [SerializeField] private protected TypeView typeView;

        public override string Name => default;

        public bool IsAuto => isAuto;
        public bool IsVisible { get; private protected set; } = false;
        public bool IsInteractable { get; private protected set; } = false;

        [BoxGroup("ListView")]
        [SerializeField] private protected BaseView[] listView;

        private protected override void _OnInit()
        {
            LogSystem.Log(Name ?? this.GetType().Name, LogType.Init);

            foreach (var s in listView.Where(u => !u.IsAuto))
            {
                s.OnInit();
            }

            CreateGlobalInstance();

            __OnInit();
        }
        public void InitPresent()
        {
            IsVisible = !isVisible;
            Show(isVisible);

            IsInteractable = !isInteractable;
            EnableIO(isInteractable);

            foreach (var s in listView.Where(u => !u.IsAuto))
            {
                s.InitPresent();
            }
        }
        private protected override void _OnDispose()
        {
            LogSystem.Log(Name ?? this.GetType().Name, LogType.Dispose);

            __OnDispose();

            RemoveGlobalInstance();

            foreach (var s in listView.Where(u => !u.IsAuto))
            {
                s.OnDispose();
            }
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
        private protected virtual void CreateGlobalInstance()
        {
            if (typeView != TypeView.Ignore)
            {
                BaseMainStorage.MainStorage.PairView[typeView] ??= this;
            }
        }
        private protected virtual void RemoveGlobalInstance()
        {
            if (typeView != TypeView.Ignore)
            {
                BaseMainStorage.MainStorage.PairView[typeView] = default;
            }
        }
    }
}
#endif