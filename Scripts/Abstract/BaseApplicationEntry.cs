#if !ENABLE_ERRORS

using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAY_Core
{
    public static class MainActor
    {
        public static event Action MainActorEvents = delegate { };

        public static void InputUpdate()
        {
            MainActorEvents?.Invoke();
        }
    }
    public interface IBaseApplicationEntry : ICoreObject
    {
        public bool IsStarted { get; }
        public MonoBehaviour Instance { get; }
    }
    public abstract class BaseApplicationEntry : BaseCoreObjectBehaviour, IBaseApplicationEntry
    {
        public static IBaseApplicationEntry MainEntry { get; private protected set; } = default;
        public static bool IsStartApplication { get; set; } = false;

        public bool IsStarted { get; private protected set; } = false;
        public MonoBehaviour Instance => this;

        private BaseMainStorage storage { get; set; } = default;

        public override void OnInit()
        {
            if (!IsInit)
            {
                _OnInit();

                IsInit = true;
                IsStarted = false;
                IsDisposed = false;
            }
        }
        private void OnStart()
        {
            if (IsInit && !IsStarted)
            {
                _OnStart();

                IsStarted = true;
                IsDisposed = false;
                IsInit = true;
            }
        }
        public override void OnDispose()
        {
            if (IsInit && !IsDisposed && IsStarted)
            {
                _OnDispose();

                IsDisposed = true;
                IsInit = false;
                IsStarted = false;
            }
        }
        private void OnUpdate()
        {
            if (IsInit && IsStarted)
            {
                MainActor.InputUpdate();

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

                __OnUpdate();
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

                __OnFixedUpdate();
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

                __OnGUI();
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

                    __OnDrawGizmos();
                }
            }
        }
        private void Awake()
        {
            OnInit();
        }
        private void Start()
        {
            OnStart();
        }
        private void Update()
        {
            OnUpdate();
        }
        private void FixedUpdate()
        {
            OnFixedUpdate();
        }
        private void OnGUI()
        {
            GUI();
        }
        private void OnDrawGizmos()
        {
            DrawGizmos();
        }
        private void OnDestroy()
        {
            OnDispose();
        }

        private protected sealed override void _OnInit()
        {
            LogSystem.Log(Name, LogSystem.LogType.Awake);

            MainEntry = this;

            storage = GameObject.FindFirstObjectByType<BaseMainStorage>();

            if (storage != default)
            {
                storage.OnInit();
            }
            else
            {
                throw new Exception();
            }

            __OnInit();
        }
        private protected sealed override void _OnDispose()
        {
            LogSystem.Log(Name, LogSystem.LogType.Dispose);

            __OnDispose();
            
            if (storage != default)
            {
                storage.OnDispose();
            }
            else
            {
                throw new Exception();
            }

            MainEntry = default;
        }
        private void _OnStart()
        {
            LogSystem.Log(Name, LogSystem.LogType.Start);

            __OnStart();
        }

        private protected virtual void __OnInit() { }
        private protected virtual void __OnDispose() { }
        private protected virtual void __OnStart() { }
        private protected virtual void __OnUpdate() { }
        private protected virtual void __OnFixedUpdate() { }
        private protected virtual void __OnGUI() { }
        private protected virtual void __OnDrawGizmos() { }
    }
}
#endif