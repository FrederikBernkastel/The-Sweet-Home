#if !ENABLE_ERRORS

using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAY_Core
{
    public static class LoadingSystem
    {
        public static void StartLoadUnloadResources(Action<IBaseLoadingState> action)
        {
            if (BaseApplicationEntry.MainEntry.IsInit)
            {
                LoadingState state = new();

                state.OnInit();

                state.OnReset();

                action.Invoke(state);

                BaseMainStorage.MainStorage.ListState.Add(state);

                state.OnEnter();
            }
            else
            {
                throw new Exception();
            }
        }
        public static void StartNonAsyncLoadUnloadResources(Action<IBaseNonAsyncLoadingState> action)
        {
            NonAsyncLoadingState state = new();

            state.OnInit();

            state.OnReset();

            action.Invoke(state);

            state.OnEnter();

            state.OnExit();

            state.OnDispose();
        }
    }
}
#endif