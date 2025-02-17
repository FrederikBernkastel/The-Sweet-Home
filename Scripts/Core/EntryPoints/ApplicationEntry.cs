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

        [BoxGroup("Resources")]
        [SerializeField] private ContextResourcesField[] listContextResources;

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
                    Debug.LogError(SceneManager.GetActiveScene().name);


                    var ert = SceneManager.GetActiveScene().name.Split("_")[1];

                    Debug.LogError(ert);

                    foreach (var s in list)
                    {
                        Debug.LogError(s.GetCustomAttribute<MainEntryAttribute>().TypeApplication);
                    }


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

                foreach (var s in listContextResources)
                {
                    s.Execute();
                }

                mainEntry?.OnInit(this);

                return true;
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() => 
            {
                LoadingSystem.Instance.OnDispose(default);
                IOSystem.Instance.OnDispose(default);
                UpdateSystem.Instance.OnDispose(default);
                CameraSystem.Instance.OnDispose(default);
                GraphicsSystem.Instance.OnDispose(default);

                ApplicationEntry.MainEntry = default;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                return true;
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