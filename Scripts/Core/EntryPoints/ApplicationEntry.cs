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
    public sealed class ApplicationEntry : BaseCoreObjectBehaviour
    {
        private static Dictionary<TypeApplication, string> pairTypeApplication { get; } = new()
        {
            [TypeApplication.TheSweetHome] = "CuteHome",
        };
        
        public static ApplicationEntry MainEntry { get; private set; } = default;

        private BaseMainEntryPoint mainEntry { get; set; } = default;

        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                Application.wantsToQuit += () =>
                {
                    HelperApplication.ApplicationQuit(false);

                    return true;
                };

                var list = HelperReflection.GetTypesWith<MainEntryAttribute, BaseMainEntryPoint>();

                if (list.Any())
                {
                    var typeClass = list.FirstOrDefault(u => pairTypeApplication[u.GetCustomAttribute<MainEntryAttribute>().TypeApplication] ==
                        SceneManager.GetActiveScene().name.Split("_")[1]);

                    mainEntry = typeClass != default ?
                        HelperReflection.CreateInstance<BaseMainEntryPoint, BaseMainEntryPoint>(typeClass) :
                        throw new Exception();
                }
                else
                {
                    throw new Exception();
                }

                ApplicationEntry.MainEntry = this;

                LoadingSystem.Instance.OnInit(default);
                IOSystem.Instance.OnInit(default);
                UpdateSystem.Instance.OnInit(default);
                CameraSystem.Instance.OnInit(default);
                GraphicsSystem.Instance.OnInit(default);

                var storage = GameObject.FindFirstObjectByType<BaseMainStorage>(FindObjectsInactive.Include);
                if (storage == default)
                {
                    throw new Exception();
                }
                else if (!storage.OnInit(default))
                {
                    throw new Exception();
                }

                mainEntry?.OnInit(this);

                return initEvent?.Invoke() ?? true;
            });

            
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() => 
            {
                var flag = disposeEvent?.Invoke() ?? true;

                LoadingSystem.Instance.OnDispose(default);
                IOSystem.Instance.OnDispose(default);
                UpdateSystem.Instance.OnDispose(default);
                CameraSystem.Instance.OnDispose(default);
                GraphicsSystem.Instance.OnDispose(default);

                var storage = GameObject.FindFirstObjectByType<BaseMainStorage>(FindObjectsInactive.Include);
                if (storage == default)
                {
                    throw new Exception();
                }
                else if (!storage.OnDispose(default))
                {
                    throw new Exception();
                }

                ApplicationEntry.MainEntry = default;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                return flag;
            });
        }
        public override void OnStart(Action startEvent)
        {
            base.OnStart(() => 
            {
                mainEntry?.OnStart(this);
            });
        }
        private void Update()
        {
            if (IsStarted)
            {
                IOSystem.Instance.Update();

                UpdateSystem.Instance.Update();
            }
        }
        private void FixedUpdate()
        {
            if (IsStarted)
            {
                UpdateSystem.Instance.FixedUpdate();
            }
        }
        private void OnGUI()
        {
            if (IsStarted)
            {
                UpdateSystem.Instance.GUI();
            }
        }
        private void OnDrawGizmos()
        {
            if (IsStarted)
            {
                if (EditorApplication.isPlaying)
                {
                    UpdateSystem.Instance.DrawGizmos();
                }
            }
        }
        private void Awake()
        {
            if (!OnInit(default))
            {
                throw new Exception();
            }
        }
        private void Start() => OnStart(default);
        private void OnDestroy()
        {
            if (!OnDispose(default))
            {
                throw new Exception();
            }
        }
    }
}
#endif