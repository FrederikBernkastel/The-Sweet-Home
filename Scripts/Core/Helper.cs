using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RAY_Core
{
    public static class Helper
    {
        public static void ApplicationQuit(bool flag)
        {
            LoadingSystem.StartNonAsyncLoadUnloadResources(u =>
            {
                u.AddUnloadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.StoryUIResources]);
                u.AddUnloadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.GameUIResources]);
                u.AddUnloadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.AdditionalUIResources]);
                u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.LoadingResources]);
                u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.PrologeResources]);
                u.AddUnloadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.MainActorResources]);
                u.AddUnloadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.GameResources]);
                u.AddUnloadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.CameraResources]);
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
    public static class ReflectionHelper
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
    //public static class UIHelper
    //{
    //    public static T CreateUIObject<T>(T script, string tag) where T : MonoBehaviour
    //    {
    //        foreach (Transform s in GameObject.FindGameObjectWithTag(tag).transform)
    //        {
    //            GameObject.Destroy(s.gameObject);
    //        }

    //        return GameObject.Instantiate(script, GameObject.FindGameObjectWithTag(tag).transform);
    //    }
    //}
    public static class LogSystem
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
            //if (BaseApplicationEntry.IsStartApplication)
            //{
            //    Debug.Log(DateTime.Now + "||" + name + dicLogType[logType]);
            //}

            Debug.Log(DateTime.Now + "||" + name + dicLogType[logType]);
        }
    }
}
