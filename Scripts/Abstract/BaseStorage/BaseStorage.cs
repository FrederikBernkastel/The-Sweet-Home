#if !ENABLE_ERRORS

using NaughtyAttributes;
using RAY_CuteHome;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UnityEngine;
using UnityEngine.Rendering;

namespace RAY_Core
{
    public interface ICoreObject
    {
        public int InstanceID { get; }
        public string Name { get; }
        public bool IsInit { get; }
        public bool IsDisposed { get; }

        public void OnInit();
        public void OnDispose();
    }
    public interface IBaseStorage : ICoreObject
    {
        
    }
    public interface IBaseMainStorage : IBaseStorage
    {
        public StateMachine StateMachine { get; }
        public Dictionary<TypeView, BaseView> PairView { get; }
        public List<BaseState> ListState { get; }
        public List<BaseStorage> ListStorage { get; }
        public Dictionary<TypeResources, IBaseContextResources> PairContextResources { get; }
    }
    public interface IBaseAdditionalStorage : IBaseStorage
    {

    }
    public abstract class BaseCoreObject : ICoreObject
    {
        private static int instanceID { get; set; } = default;

        public int InstanceID { get; } = instanceID++;
        public abstract string Name { get; }

        public bool IsInit { get; private protected set; } = false;
        public bool IsDisposed { get; private protected set; } = false;

        public void OnInit()
        {
            if (!IsInit)
            {
                _OnInit();

                IsInit = true;
                IsDisposed = false;
            }
        }
        public void OnDispose()
        {
            if (IsInit && !IsDisposed)
            {
                _OnDispose();

                IsDisposed = true;
                IsInit = false;
            }
        }

        private protected virtual void _OnInit() { }
        private protected virtual void _OnDispose() { }
    }
    public abstract class BaseCoreObjectBehaviour : MonoBehaviour, ICoreObject
    {
        private static int instanceID { get; set; } = default;

        public int InstanceID { get; } = instanceID++;
        public abstract string Name { get; }

        public bool IsInit { get; private protected set; } = false;
        public bool IsDisposed { get; private protected set; } = false;

        public virtual void OnInit()
        {
            if (!IsInit)
            {
                _OnInit();

                IsInit = true;
                IsDisposed = false;
            }
        }
        public virtual void OnDispose()
        {
            if (IsInit && !IsDisposed)
            {
                _OnDispose();

                IsDisposed = true;
                IsInit = false;
            }
        }

        private protected virtual void _OnInit() { }
        private protected virtual void _OnDispose() { }
    }
    public abstract class BaseStorage : BaseCoreObjectBehaviour, IBaseStorage
    {

    }
    public abstract class BaseMainStorage : BaseStorage, IBaseMainStorage
    {
        public static IBaseMainStorage MainStorage { get; private protected set; } = default;

        public StateMachine StateMachine { get; private protected set; } = default;

        public Dictionary<TypeView, BaseView> PairView { get; private protected set; } = default;
        public List<BaseState> ListState { get; private protected set; } = default;
        public List<BaseStorage> ListStorage { get; private protected set; } = default;
        public Dictionary<TypeResources, IBaseContextResources> PairContextResources { get; private protected set; } = default;

        [BoxGroup("Resources")]
        [SerializeField] private protected ContextResourcesField[] listContextResources;

        private protected abstract StateMachine CreateStateMachine();

        private void InitPairContextResources()
        {
            PairContextResources = new();

            foreach (var s in Enum.GetValues(typeof(TypeResources)))
            {
                PairContextResources.Add((TypeResources)s, default);
            }
        }
        private void InitPairView()
        {
            PairView = new();

            foreach (var s in Enum.GetValues(typeof(TypeView)))
            {
                PairView.Add((TypeView)s, default);
            }
        }
        private void InitListContextResources()
        {
            foreach (var s in listContextResources)
            {
                s.OnInit();
            }
        }
        private void InitStateMachine()
        {
            StateMachine = CreateStateMachine();
            StateMachine.OnInit();
        }
        private void DisposeInitListContextResources()
        {
            foreach (var s in listContextResources)
            {
                s.OnDispose();
            }
        }
        private void DisposeStateMachine()
        {
            StateMachine.Dispose();

            StateMachine = default;
        }
        private void DisposeListState()
        {
            foreach (var s in ListState)
            {
                s.OnDispose();
            }

            ListState.Clear();

            ListState = default;
        }
        private void DisposePairView()
        {
            foreach (var s in PairView.Values)
            {
                if (s != default)
                {
                    s.OnDispose();
                }
            }

            PairView.Clear();

            PairView = default;
        }
        private void DisposeListStorage()
        {
            foreach (var s in ListStorage)
            {
                s.OnDispose();
            }

            ListStorage.Clear();

            ListStorage = default;
        }
        private void DisposePairContextResources()
        {
            foreach (var s in PairContextResources.Values)
            {
                if (s != default)
                {
                    
                }
            }

            PairContextResources.Clear();

            PairContextResources = default;
        }
        private protected sealed override void _OnInit()
        {
            LogSystem.Log(Name, LogSystem.LogType.Init);

            MainStorage = this;

            InitPairContextResources();
            InitPairView();
            ListState = new();
            ListStorage = new();
            InitListContextResources();
            InitStateMachine();

            __OnInit();
        }
        private protected sealed override void _OnDispose()
        {
            LogSystem.Log(Name, LogSystem.LogType.Dispose);

            __OnDispose();

            DisposeListStorage();
            DisposeInitListContextResources();
            DisposePairContextResources();
            DisposeStateMachine();
            DisposeListState();
            DisposePairView();

            MainStorage = default;

            GC.Collect(2, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
        }

        private protected virtual void __OnInit() { }
        private protected virtual void __OnDispose() { }
    }
    public abstract class BaseAdditionalStorage : BaseStorage, IBaseAdditionalStorage
    {
        private void Awake()
        {
            OnInit();
        }
        private void OnDestroy()
        {
            OnDispose();
        }

        private protected sealed override void _OnInit()
        {
            LogSystem.Log(Name, LogSystem.LogType.Init);

            if (!BaseMainStorage.MainStorage.ListStorage.Contains(this))
            {
                BaseMainStorage.MainStorage.ListStorage.Add(this);
            }

            foreach (var s in GameObject.FindObjectsOfType<BaseView>().Where(u => u.IsAuto))
            {
                s.OnInit();
            }
            foreach (var s in GameObject.FindObjectsOfType<BaseView>().Where(u => u.IsAuto))
            {
                s.InitPresent();
            }

            __OnInit();
        }
        private protected sealed override void _OnDispose()
        {
            LogSystem.Log(Name, LogSystem.LogType.Dispose);

            __OnDispose();

            foreach (var s in GameObject.FindObjectsOfType<BaseView>().Where(u => u.IsAuto))
            {
                s.OnDispose();
            }
        }

        private protected virtual void __OnInit() { }
        private protected virtual void __OnDispose() { }
    }
}
#endif