using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RAY_Core
{
    public static class CameraHelper
    {
        public static bool BindingCameraWithCanvas(IViewCamera viewCamera, IViewCanvas viewCanvas)
        {
            if (viewCamera.Camera == default || viewCanvas.Canvas == default)
            {
                return false;
            }

            viewCanvas.Canvas.renderMode = RenderMode.ScreenSpaceCamera;
            viewCanvas.Canvas.worldCamera = viewCamera.Camera;

            return true;
        }
    }
    public static class UIHelper
    {
        public static T CreateUIObject<T>(T script, string tag) where T : MonoBehaviour
        {
            foreach (Transform s in GameObject.FindGameObjectWithTag(tag).transform)
            {
                GameObject.Destroy(s.gameObject);
            }

            return GameObject.Instantiate(script, GameObject.FindGameObjectWithTag(tag).transform);
        }
    }
    public static class LogSystem
    {
        public enum LogType
        {
            Enter,
            Exit,
            Awake,
            Start,
            Update,
            FixedUpdate,
            Init,
            GUI,
            DrawGizmos,
            Dispose,
            Reset,
        }

        private static Dictionary<LogType, string> dicLogType = new()
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
            if (BaseApplicationEntry.IsStartApplication)
            {
                Debug.Log(DateTime.Now + "||" + name + dicLogType[logType]);
            }
        }
    }
}
