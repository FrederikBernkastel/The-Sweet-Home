#if !ENABLE_ERRORS

using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RAY_Core
{
    public interface IBaseView : ICoreObject
    {
        public bool IsVisible { get; }
        public bool IsInteractable { get; }
        public bool IsAuto { get; }

        public void EnableIO(bool flag);
        public void Show(bool flag);
    }
    public abstract class BaseView : BaseCoreObjectBehaviour, IBaseView
    {
        [BoxGroup("OnInit")]
        [SerializeField] private protected bool isAuto;
        [SerializeField] private protected bool isVisible;
        [SerializeField] private protected bool isInteractable;

        public bool IsAuto => isAuto;
        public bool IsVisible { get; private protected set; } = false;
        public bool IsInteractable { get; private protected set; } = false;

        [BoxGroup("ListView")]
        [SerializeField] private protected BaseView[] listView;

        private protected override void _OnInit()
        {
            LogSystem.Log(Name, LogSystem.LogType.Init);

            foreach (var s in listView.Where(u => !u.IsAuto))
            {
                s.OnInit();
            }

            __OnInit();

            CreateGlobalInstance();
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
            LogSystem.Log(Name, LogSystem.LogType.Dispose);

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
        private protected virtual void CreateGlobalInstance() { }
        private protected virtual void RemoveGlobalInstance() { }
    }
}
#endif