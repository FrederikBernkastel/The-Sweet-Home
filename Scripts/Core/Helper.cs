using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RAY_Core
{
    public static class HelperApplication
    {
        public static void ApplicationQuit(bool flag)
        {
            LoadingSystem.Instance.StartNonAsyncLoadUnloadResources(u =>
            {
                if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.StoryUIResources, out var story))
                {
                    u.AddUnloadContext(story);
                }
                if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.GameUIResources, out var gameUI))
                {
                    u.AddUnloadContext(gameUI);
                }
                if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.AdditionalUIResources, out var additionalUI))
                {
                    u.AddUnloadContext(additionalUI);
                }
                if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.LoadingResources, out var loading))
                {
                    u.AddUnloadContext(loading);
                }
                if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.PrologeResources, out var prologe))
                {
                    u.AddUnloadContext(prologe);
                }
                if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.MainActorResources, out var mainActor))
                {
                    u.AddUnloadContext(mainActor);
                }
                if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.GameResources, out var game))
                {
                    u.AddUnloadContext(game);
                }
                if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.DebugTest, out var debugTest))
                {
                    u.AddUnloadContext(debugTest);
                }
                if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.CameraResources, out var camera))
                {
                    u.AddUnloadContext(camera);
                }
            });

            if (flag)
            {
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#else
                Application.Quit();
#endif
            }
        }
    }
    public static class HelperReflection
    {
        public static IEnumerable<Type> GetTypesWith<TAttribute, YClass>(bool inherit = true) where TAttribute : Attribute where YClass : class
        {
            var output = new List<Type>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var assembly_types = assembly.GetTypes();

                foreach (var type in assembly_types)
                {
                    if (type.IsDefined(typeof(TAttribute), inherit) && type.IsSubclassOf(typeof(YClass)) && !type.IsAbstract)
                        output.Add(type);
                }
            }

            return output;
        }
        public static TOut CreateInstance<TOut, YIn>(Type type) where TOut : class where YIn : class
        {
            return (TOut)Activator.CreateInstance(type.IsSubclassOf(typeof(YIn)) ? type : throw new Exception());
        }
    }
    public static class HelperLog
    {
        private static Dictionary<LogType, string> dicLogType { get; } = new()
        {
            [LogType.Enter] = "Enter",
            [LogType.Exit] = "Exit",
            [LogType.Awake] = "Awake",
            [LogType.Start] = "Start",
            [LogType.Update] = "Update",
            [LogType.FixedUpdate] = "FixedUpdate",
            [LogType.Init] = "Init",
            [LogType.GUI] = "GUI",
            [LogType.DrawGizmos] = "DrawGizmos",
            [LogType.Dispose] = "Dispose",
            [LogType.Reset] = "Reset",
        };

        public static void Log(string name, LogType logType)
        {
            Debug.Log(name + dicLogType[logType]);
        }
    }
}
