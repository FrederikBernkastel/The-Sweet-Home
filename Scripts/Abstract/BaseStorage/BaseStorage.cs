#if !ENABLE_ERRORS

using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseCoreObject
    {
        private static int instanceID = default;
        public readonly int InstanceID = instanceID++;
        public virtual string Name => default;

        public bool IsInited { get; private protected set; } = default;
        public bool IsDisposed { get; private protected set; } = default;

        private protected virtual BaseMethod<BaseCoreObject> InitEvent => InitMethod.Instance;
        private protected virtual BaseMethod<BaseCoreObject> DisposeEvent => DisposeMethod.Instance;

        private class InitMethod : BaseMethod<BaseCoreObject, InitMethod>
        {
            public override void Execute(BaseCoreObject _object)
            {
                if (!_object.IsInited)
                {
                    baseMethod?.Execute(_object);

                    _object.IsInited = true;
                    _object.IsDisposed = false;
                }
            }
        }
        private class DisposeMethod : BaseMethod<BaseCoreObject, DisposeMethod>
        {
            public override void Execute(BaseCoreObject _object)
            {
                if (!_object.IsDisposed)
                {
                    baseMethod?.Execute(_object);

                    _object.IsDisposed = true;
                    _object.IsInited = false;
                }
            }
        }

        internal void OnInit() => InitEvent.Execute(this);
        internal void OnDispose() => DisposeEvent.Execute(this);
    }
    public abstract class BaseMethod<T> where T : class
    {
        public abstract void Execute(T _object);
        private protected BaseMethod<T> baseMethod { get; private set; } = default;
        public BaseMethod<T> SetBaseMethodNode(BaseMethod<T> obj)
        {
            baseMethod = obj;

            return this;
        }
    }
    public abstract class BaseMethod<T, Y> : BaseMethod<T> where T : class where Y : BaseMethod<T, Y>, new()
    {
        public static Y Instance { get; } = new Y();
    }
    public abstract class BaseCoreObjectBehaviour : MonoBehaviour
    {
        private static int instanceID = default;
        public readonly int InstanceID = instanceID++;
        public virtual string Name => default;

        public bool IsInited { get; private protected set; } = default;
        public bool IsDisposed { get; private protected set; } = default;
        public bool IsStarted { get; private protected set; } = default;

        private protected virtual BaseMethod<BaseCoreObjectBehaviour> InitEvent => InitMethod.Instance;
        private protected virtual BaseMethod<BaseCoreObjectBehaviour> StartEvent => StartMethod.Instance;
        private protected virtual BaseMethod<BaseCoreObjectBehaviour> DisposeEvent => DisposeMethod.Instance;

        private class InitMethod : BaseMethod<BaseCoreObjectBehaviour, InitMethod>
        {
            public override void Execute(BaseCoreObjectBehaviour _object)
            {
                if (!_object.IsInited)
                {
                    baseMethod?.Execute(_object);

                    _object.IsInited = true;
                    _object.IsDisposed = false;
                    _object.IsStarted = false;
                }
            }
        }
        private class StartMethod : BaseMethod<BaseCoreObjectBehaviour, StartMethod>
        {
            public override void Execute(BaseCoreObjectBehaviour _object)
            {
                if (_object.IsInited && !_object.IsStarted)
                {
                    baseMethod?.Execute(_object);

                    _object.IsStarted = true;
                    _object.IsDisposed = false;
                    _object.IsInited = true;
                }
            }
        }
        private class DisposeMethod : BaseMethod<BaseCoreObjectBehaviour, DisposeMethod>
        {
            public override void Execute(BaseCoreObjectBehaviour _object)
            {
                if (_object.IsInited && !_object.IsDisposed && _object.IsStarted)
                {
                    baseMethod?.Execute(_object);

                    _object.IsDisposed = true;
                    _object.IsInited = false;
                    _object.IsStarted = false;
                }
            }
        }

        internal void OnInit() => InitEvent.Execute(this);
        internal void OnDispose() => DisposeEvent.Execute(this);
        internal void OnStart() => StartEvent.Execute(this);
    }
    [Serializable]
    public class ContextResourcesField
    {
        [BoxGroup("ResourcesField")]
        [SerializeField][Scene] private protected string nameResources;
        [BoxGroup("ResourcesField")]
        [SerializeField] private protected TypeResources typeResources;

        private protected override void _OnInit()
        {
            ContextResources context = new(nameResources);

            context.OnInit();

            BaseMainStorage.MainStorage.PairContextResources[typeResources] = context;
        }
    }
    public abstract class BaseStorage : BaseCoreObjectBehaviour
    {
        
    }
    public abstract class BaseMainStorage : BaseStorage
    {
        public static BaseMainStorage MainStorage { get; private protected set; } = default;

        public List<BaseStorage> ListStorage { get; private protected set; } = default;
        public Dictionary<TypeResources, IBaseContextResources> PairContextResources { get; private protected set; } = default;

        [BoxGroup("Resources")]
        [SerializeField] private protected ContextResourcesField[] listContextResources;

        private protected override BaseMethod<BaseCoreObjectBehaviour> InitEvent => base.InitEvent.SetBaseMethodNode(InitMethod.Instance);
        private protected override BaseMethod<BaseCoreObjectBehaviour> DisposeEvent => base.DisposeEvent.SetBaseMethodNode(DisposeMethod.Instance);

        private class InitMethod : BaseMethod<BaseCoreObjectBehaviour, InitMethod>
        {
            public override void Execute(BaseCoreObjectBehaviour _object)
            {
                var obj = _object as BaseMainStorage;

                LogSystem.Log(_object.Name ?? _object.GetType().Name, LogType.Init);

                BaseMainStorage.MainStorage = obj;

                obj.InitPairContextResources();
                obj.InitListContextResources();

                baseMethod?.Execute(_object);
            }
        }
        private class DisposeMethod : BaseMethod<BaseCoreObjectBehaviour, DisposeMethod>
        {
            public override void Execute(BaseCoreObjectBehaviour _object)
            {
                var obj = _object as BaseMainStorage;

                LogSystem.Log(_object.Name ?? _object.GetType().Name, LogType.Dispose);

                baseMethod?.Execute(_object);

                obj.DisposeListStorage();
                obj.DisposeInitListContextResources();
                obj.DisposePairContextResources();

                BaseMainStorage.MainStorage = default;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        internal void InitPairContextResources()
        {
            PairContextResources = new();

            foreach (var s in Enum.GetValues(typeof(TypeResources)))
            {
                PairContextResources.Add((TypeResources)s, default);
            }
        }
        internal void InitListContextResources()
        {
            foreach (var s in listContextResources)
            {
                s.OnInit();
            }
        }
        internal void DisposeInitListContextResources()
        {
            foreach (var s in listContextResources)
            {
                s.OnDispose();
            }
        }
        internal void DisposeListStorage()
        {
            foreach (var s in ListStorage)
            {
                s.OnDispose();
            }

            ListStorage.Clear();

            ListStorage = default;
        }
        internal void DisposePairContextResources()
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
    }
    public abstract class BaseAdditionalStorage : BaseStorage
    {
        private protected override BaseMethod<BaseCoreObjectBehaviour> InitEvent => base.InitEvent.SetBaseMethodNode(InitMethod.Instance);
        private protected override BaseMethod<BaseCoreObjectBehaviour> DisposeEvent => base.DisposeEvent.SetBaseMethodNode(DisposeMethod.Instance);

        private class InitMethod : BaseMethod<BaseCoreObjectBehaviour, InitMethod>
        {
            public override void Execute(BaseCoreObjectBehaviour _object)
            {
                var obj = _object as BaseAdditionalStorage;

                LogSystem.Log(_object.Name ?? _object.GetType().Name, LogType.Init);

                if (!BaseMainStorage.MainStorage.ListStorage.Contains(obj))
                {
                    BaseMainStorage.MainStorage.ListStorage.Add(obj);
                }

                foreach (var s in GameObject.FindObjectsOfType<BaseView>().Where(u => u.IsAuto))
                {
                    s.OnInit();
                }
                foreach (var s in GameObject.FindObjectsOfType<BaseView>().Where(u => u.IsAuto))
                {
                    s.InitPresent();
                }

                baseMethod?.Execute(_object);
            }
        }
        private class DisposeMethod : BaseMethod<BaseCoreObjectBehaviour, DisposeMethod>
        {
            public override void Execute(BaseCoreObjectBehaviour _object)
            {
                var obj = _object as BaseAdditionalStorage;

                LogSystem.Log(_object.Name ?? _object.GetType().Name, LogType.Dispose);

                baseMethod?.Execute(_object);

                foreach (var s in GameObject.FindObjectsOfType<BaseView>().Where(u => u.IsAuto))
                {
                    s.OnDispose();
                }
            }
        }

        private void Awake() => OnInit();
        private void OnDestroy() => OnDispose();
    }
}
#endif