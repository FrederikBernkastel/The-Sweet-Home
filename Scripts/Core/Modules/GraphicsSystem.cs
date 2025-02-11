using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RAY_Core
{
    public sealed class GraphicsSystem : BaseCoreObject
    {
        public static GraphicsSystem Instance { get; } = new();

        private Dictionary<TypeView, BaseView> dictionaryViews { get; } = new();
        private Dictionary<TypeViewUI, BaseViewUI> dictionaryViewsUI { get; } = new();

        public IReadOnlyDictionary<TypeView, BaseView> PairViews => dictionaryViews;
        public IReadOnlyDictionary<TypeViewUI, BaseViewUI> PairViewsUI => dictionaryViewsUI;

        private GraphicsSystem() { }

        public override void Reset(Action resetEvent)
        {
            base.Reset(() => 
            {
                dictionaryViews.Clear();
                dictionaryViewsUI.Clear();

                dictionaryViews.Init();
                dictionaryViewsUI.Init();
            });
        }
        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                Reset(default);

                return true;
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() => 
            {
                Reset(default);

                return true;
            });
        }

        public void AddView(TypeView typeView, BaseView view)
        {
            if (IsInited)
            {
                dictionaryViews.Add(typeView, view);
            }
        }
        public void AddViewUI(TypeViewUI typeView, BaseViewUI view)
        {
            if (IsInited)
            {
                dictionaryViewsUI.Add(typeView, view);
            }
        }
        public void RemoveView(TypeView typeView)
        {
            if (IsInited)
            {
                dictionaryViews[typeView] = default;
            }
        }
        public void RemoveViewUI(TypeViewUI typeView)
        {
            if (IsInited)
            {
                dictionaryViewsUI[typeView] = default;
            }
        }
    }
    public abstract class BaseCustomAnimation : BaseState
    {

    }
    //public class ForceShowAnimation : BaseCustomAnimation
    //{

    //}
    //public class ForceHideAnimation : BaseCustomAnimation
    //{
        
    //}
    //public class DissolveShowAnimation : BaseCustomAnimation
    //{
        
    //}
    //public class DissolveHideAnimation : BaseCustomAnimation
    //{
        
    //}
}
