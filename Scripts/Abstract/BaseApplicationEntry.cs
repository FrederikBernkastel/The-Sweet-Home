#if !ENABLE_ERRORS

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
    public abstract class BaseApplicationEntry : BaseCoreObjectBehaviour
    {
        private Dictionary<TypeApplication, string> pairTypeApplication { get; } = new()
        {
            [TypeApplication.TheSweetHome] = "CuteHome",
        };
        
        public static BaseApplicationEntry MainEntry { get; private protected set; } = default;
        public static bool IsStartApplication { get; set; } = false;

        public override string Name => default;
        public bool IsStarted { get; private protected set; } = false;

        public event Action EventInit = delegate { };
        public event Action EventStart = delegate { };
        public event Action EventDispose = delegate { };
        public event Action EventUpdate = delegate { };
        public event Action EventFixedUpdate = delegate { };
        public event Action EventGUI = delegate { };
        public event Action EventDrawGizmos = delegate { };

        public override void OnInit()
        {
            if (!IsInit)
            {
                LogSystem.Log(Name ?? this.GetType().Name, LogType.Awake);

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

                MainEntry = this;

                GameObject.FindFirstObjectByType<BaseMainStorage>()?.OnInit();

                mainEntryPoint?.OnInit(this);

                EventInit.Invoke();

                IsInit = true;
                IsStarted = false;
                IsDisposed = false;
            }
        }
        private void OnStart()
        {
            if (IsInit && !IsStarted)
            {
                LogSystem.Log(Name ?? this.GetType().Name, LogType.Start);

                EventStart.Invoke();

                IsStarted = true;
                IsDisposed = false;
                IsInit = true;
            }
        }
        public override void OnDispose()
        {
            if (IsInit && !IsDisposed && IsStarted)
            {
                LogSystem.Log(Name ?? this.GetType().Name, LogType.Dispose);

                EventDispose.Invoke();

                GameObject.FindFirstObjectByType<BaseMainStorage>()?.OnDispose();

                MainEntry = default;

                IsDisposed = true;
                IsInit = false;
                IsStarted = false;
            }
        }
        private void OnUpdate()
        {
            if (IsInit && IsStarted)
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
        private void OnFixedUpdate()
        {
            if (IsInit && IsStarted)
            {
                BaseMainStorage.MainStorage.StateMachine.OnFixedUpdate();

                foreach (var s in BaseMainStorage.MainStorage.ListState)
                {
                    s.OnFixedUpdate();
                }

                EventFixedUpdate.Invoke();
            }
        }
        private void GUI()
        {
            if (IsInit && IsStarted)
            {
                BaseMainStorage.MainStorage.StateMachine.OnGUI();

                foreach (var s in BaseMainStorage.MainStorage.ListState)
                {
                    s.OnGUI();
                }

                EventGUI.Invoke();
            }
        }
        private void DrawGizmos()
        {
            if (IsInit && IsStarted)
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
        private void Awake() => OnInit();
        private void Start() => OnStart();
        private void Update() => OnUpdate();
        private void FixedUpdate() => OnFixedUpdate();
        private void OnGUI() => GUI();
        private void OnDrawGizmos() => DrawGizmos();
        private void OnDestroy() => OnDispose();
    }
}
#endif