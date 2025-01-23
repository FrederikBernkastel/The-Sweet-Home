using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseGraphicsSystem : BaseCoreObject
    {
        public static BaseGraphicsSystem Instance { get; } = new GraphicsSystem();

        public abstract void AddView(TypeView typeView, BaseView view);
        public abstract void RemoveView(TypeView typeView);
        public abstract IReadOnlyDictionary<TypeView, BaseView> DictionaryViews { get; }
    }
    public class GraphicsSystem : BaseGraphicsSystem
    {
        private Dictionary<TypeView, BaseView> dictionaryViews { get; set; } = new();

        public override IReadOnlyDictionary<TypeView, BaseView> DictionaryViews => dictionaryViews;

        private protected override BaseGraphicsSystem Init()
        {
            dictionaryViews.Init();

            return this;
        }
        private protected override void Dispose()
        {
            
        }
    }
    public abstract class BaseCustomAnimation<T> where T : BaseCustomAnimation<T>, new()
    {
        public const int PoolingSize = 50;
        public static PoolingArray<T> Factory { get; private protected set; } = new(PoolingSize,
            () => new T().OnInit().OnReset(),
            u => u,
            u => { },
            u => u.OnDispose());

        private protected virtual T OnReset() => default;
        private protected virtual T OnInit() => default;
        private protected virtual void OnDispose() { }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
    public class ForceShowAnimation : BaseCustomAnimation<ForceShowAnimation>
    {
        
    }
    public class ForceHideAnimation : BaseCustomAnimation<ForceHideAnimation>
    {
        
    }
    public class DissolveShowAnimation : BaseCustomAnimation<DissolveShowAnimation>
    {
        
    }
    public class DissolveHideAnimation : BaseCustomAnimation<DissolveHideAnimation>
    {
        
    }
    public class ScaleShowAnimation : BaseCustomAnimation<ScaleShowAnimation>
    {
        
    }
    public class ScaleHideAnimation : BaseCustomAnimation<ScaleHideAnimation>
    {
        
    }
}
