#if !ENABLE_ERRORS

using MapMagic.Nodes;
using MarkerMetro.Unity.WinLegacy.Reflection;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAY_Core
{
    public static class UserInput
    {
        public static event Action ListEvents = delegate { };

        public static void InputUpdate()
        {
            ListEvents.Invoke();
        }
        public static void AddEvent(Action action)
        {
            ListEvents += action;
        }
        public static void RemoveEvent(Action action)
        {
            ListEvents -= action;
        }
    }
    public class MainEntryAttribute : Attribute
    {
        public TypeApplication TypeApplication { get; set; }
    }
    public abstract class BaseMainEntryPoint
    {
        public abstract void OnInit(BaseApplicationEntry applicationEntry);
    }
    public abstract class BaseUpdateSystem : BaseCoreObject
    {
        public 
    }
    public abstract class BaseApplicationEntry : BaseCoreObjectBehaviour
    {
        private static Dictionary<TypeApplication, string> pairTypeApplication { get; } = new()
        {
            [TypeApplication.TheSweetHome] = "CuteHome",
        };
        
        public static BaseApplicationEntry MainEntry { get; private protected set; } = default;

        public virtual event Action EventInit = delegate { };
        public virtual event Action EventStart = delegate { };
        public virtual event Action EventDispose = delegate { };
        public virtual event Action EventUpdate = delegate { };
        public virtual event Action EventFixedUpdate = delegate { };
        public virtual event Action EventGUI = delegate { };
        public virtual event Action EventDrawGizmos = delegate { };

        private protected override BaseMethod<BaseCoreObjectBehaviour> InitEvent => base.InitEvent.SetBaseMethodNode(InitMethod.Instance);
        private protected override BaseMethod<BaseCoreObjectBehaviour> StartEvent => base.StartEvent.SetBaseMethodNode(StartMethod.Instance);
        private protected override BaseMethod<BaseCoreObjectBehaviour> DisposeEvent => base.DisposeEvent.SetBaseMethodNode(DisposeMethod.Instance);

        private class InitMethod : BaseMethod<BaseCoreObjectBehaviour, InitMethod>
        {
            public override void Execute(BaseCoreObjectBehaviour _object)
            {
                var obj = _object as BaseApplicationEntry;

                LogSystem.Log(_object.Name ?? _object.GetType().Name, LogType.Awake);

                Application.wantsToQuit += () =>
                {
                    Helper.ApplicationQuit(false);

                    return true;
                };

                var list = ReflectionHelper.GetTypesWith<MainEntryAttribute, BaseMainEntryPoint>();

                BaseMainEntryPoint mainEntryPoint = default;

                if (list.Any())
                {
                    var typeClass = list.FirstOrDefault(u => pairTypeApplication[u.GetCustomAttribute<MainEntryAttribute>().TypeApplication] ==
                        SceneManager.GetActiveScene().name.Split("_")[1]);

                    mainEntryPoint = typeClass != default ?
                        ReflectionHelper.CreateInstance<BaseMainEntryPoint, BaseMainEntryPoint>(typeClass) :
                        throw new Exception();
                }
                else
                {
                    throw new Exception();
                }

                BaseApplicationEntry.MainEntry = obj;

                GameObject.FindFirstObjectByType<BaseMainStorage>()?.OnInit();

                mainEntryPoint?.OnInit(obj);

                obj.EventInit.Invoke();
            }
        }
        private class StartMethod : BaseMethod<BaseCoreObjectBehaviour, StartMethod>
        {
            public override void Execute(BaseCoreObjectBehaviour _object)
            {
                var obj = _object as BaseApplicationEntry;

                LogSystem.Log(_object.Name ?? _object.GetType().Name, LogType.Start);

                obj.EventStart.Invoke();
            }
        }
        private class DisposeMethod : BaseMethod<BaseCoreObjectBehaviour, DisposeMethod>
        {
            public override void Execute(BaseCoreObjectBehaviour _object)
            {
                var obj = _object as BaseApplicationEntry;

                LogSystem.Log(_object.Name ?? _object.GetType().Name, LogType.Dispose);

                obj.EventDispose.Invoke();

                GameObject.FindFirstObjectByType<BaseMainStorage>()?.OnDispose();

                MainEntry = default;
            }
        }

        private protected virtual void Update()
        {
            if (IsInited && IsStarted)
            {
                UserInput.InputUpdate();

                BaseMainStorage.MainStorage.StateMachine.OnUpdate();

                foreach (var s in BaseMainStorage.MainStorage.ListState)
                {
                    if (s.OnUpdate())
                    {
                        s.OnExit();
                    }
                }

                BaseMainStorage.MainStorage.ListState.RemoveAll(u =>
                {
                    if (u.IsNeedRemove)
                    {
                        u.OnDispose();

                        return true;
                    }

                    return false;
                });

                EventUpdate.Invoke();
            }
        }
        private protected virtual void FixedUpdate()
        {
            if (IsInited && IsStarted)
            {
                BaseMainStorage.MainStorage.StateMachine.OnFixedUpdate();

                foreach (var s in BaseMainStorage.MainStorage.ListState)
                {
                    s.OnFixedUpdate();
                }

                EventFixedUpdate.Invoke();
            }
        }
        private protected virtual void OnGUI()
        {
            if (IsInited && IsStarted)
            {
                BaseMainStorage.MainStorage.StateMachine.OnGUI();

                foreach (var s in BaseMainStorage.MainStorage.ListState)
                {
                    s.OnGUI();
                }

                EventGUI.Invoke();
            }
        }
        private protected virtual void OnDrawGizmos()
        {
            if (IsInited && IsStarted)
            {
                if (EditorApplication.isPlaying)
                {
                    BaseMainStorage.MainStorage.StateMachine.OnDrawGizmos();

                    foreach (var s in BaseMainStorage.MainStorage.ListState)
                    {
                        s.OnDrawGizmos();
                    }

                    EventDrawGizmos.Invoke();
                }
            }
        }
        private void Awake() => InitEvent.Execute(this);
        private void Start() => StartEvent.Execute(this);
        private void OnDestroy() => DisposeEvent.Execute(this);
    }
}
#endif