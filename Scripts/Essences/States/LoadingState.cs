#if !ENABLE_ERRORS

using RAY_CuteHome;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAY_Core
{
    public interface IBaseLoadingState : IBaseNonAsyncLoadingState
    {
        public float WaitTime { get; set; }
        public float SpeedLoadingBar { get; set; }
        public ViewLoading ViewLoading { get; set; }
    }
    public interface IBaseNonAsyncLoadingState : IBaseState
    {
        public void AddUnloadContext(IBaseContextResources baseContextResources);
        public void AddLoadContext(IBaseContextResources baseContextResources);
    }
    public class NonAsyncLoadingState : BaseState, IBaseNonAsyncLoadingState
    {
        public override string Name { get; } = "NonAsyncLoadingState";

        private protected List<IBaseContextResources> listUnloadContext { get; set; } = default;
        private protected List<IBaseContextResources> listLoadContext { get; set; } = default;

        private void NonAsyncLoading()
        {
            AsyncOperation asyncOperation;

            foreach (var s in listUnloadContext)
            {
                if (s.IsLoaded())
                {
                    asyncOperation = SceneManager.UnloadSceneAsync(s.Name);

                    while (!asyncOperation.isDone)
                    {

                    }
                }
            }

            foreach (var s in listLoadContext)
            {
                if (!s.IsLoaded())
                {
                    SceneManager.LoadScene(s.NameResources, LoadSceneMode.Additive);
                }
            }
        }
        private protected override void __OnEnter()
        {
            NonAsyncLoading();
        }
        private protected override void __OnInit()
        {
            listUnloadContext = new();
            listLoadContext = new();
        }
        private protected override void __OnDispose()
        {
            listUnloadContext = default;
            listLoadContext = default;
        }
        private protected override void __OnReset()
        {
            listUnloadContext.Clear();
            listLoadContext.Clear();
        }
        public void AddUnloadContext(IBaseContextResources baseContextResources)
        {
            if (IsInit)
            {
                listUnloadContext.Add(baseContextResources);
            }
        }
        public void AddLoadContext(IBaseContextResources baseContextResources)
        {
            if (IsInit)
            {
                listLoadContext.Add(baseContextResources);
            }
        }
    }
    public class LoadingState : BaseState, IBaseLoadingState
    {
        public override string Name { get; } = "LoadingState";

        public float WaitTime { get; set; } = default;
        public float SpeedLoadingBar { get; set; } = default;
        private float currentValue { get; set; } = default;
        private float targetValue { get; set; } = default;
        public ViewLoading ViewLoading { get; set; } = default;

        private protected List<IBaseContextResources> listUnloadContext { get; set; } = default;
        private protected List<IBaseContextResources> listLoadContext { get; set; } = default;

        private bool isCompleted { get; set; } = false;

        private protected override void __OnInit()
        {
            listUnloadContext = new List<IBaseContextResources>();
            listLoadContext = new List<IBaseContextResources>();
        }
        private protected override void __OnDispose()
        {
            listUnloadContext = default;
            listLoadContext = default;
            ViewLoading = default;
        }
        public void AddUnloadContext(IBaseContextResources baseContextResources)
        {
            listUnloadContext.Add(baseContextResources);
        }
        public void AddLoadContext(IBaseContextResources baseContextResources)
        {
            listLoadContext.Add(baseContextResources);
        }
        private protected override void __OnReset()
        {
            listUnloadContext.Clear();
            listLoadContext.Clear();

            isCompleted = false;
            IsNeedRemove = false;
            ViewLoading = default;
            WaitTime = default;
            SpeedLoadingBar = default;
            currentValue = default;
            targetValue = default;
        }
        private protected override void __OnEnter()
        {
            BaseApplicationEntry.MainEntry.Instance.StartCoroutine(Loading());

            if (ViewLoading != default)
            {
                ViewLoading.Show(true);
                ViewLoading.EnableIO(true);
            }
        }
        private protected override bool __OnUpdate()
        {
            currentValue = Mathf.MoveTowards(currentValue, targetValue, SpeedLoadingBar * Time.deltaTime);

            if (ViewLoading != default)
            {
                ViewLoading.SetProgress(currentValue);
            }

            return isCompleted;
        }
        private protected override void __OnExit()
        {
            IsNeedRemove = true;

            if (ViewLoading != default)
            {
                ViewLoading.Show(false);
                ViewLoading.EnableIO(false);
            }
        }
        private IEnumerator Loading()
        {
            AsyncOperation asyncOperation;

            float temp = 1.0f / (listUnloadContext.Count + listLoadContext.Count);
            float temp1 = default;

            foreach (var s in listUnloadContext)
            {
                if (s.IsLoaded())
                {
                    asyncOperation = SceneManager.UnloadSceneAsync(s.Name);

                    while (!asyncOperation.isDone)
                    {
                        targetValue = temp1 + asyncOperation.progress * temp;

                        yield return null;
                    }

                    temp1 += temp;
                }
            }

            foreach (var s in listLoadContext)
            {
                if (!s.IsLoaded())
                {
                    asyncOperation = SceneManager.LoadSceneAsync(s.NameResources, LoadSceneMode.Additive);

                    while (!asyncOperation.isDone)
                    {
                        targetValue = temp1 + asyncOperation.progress * temp;

                        yield return null;
                    }

                    temp1 += temp;
                }
            }

            targetValue = 1.0f;

            while (!Mathf.Approximately(currentValue, targetValue))
            {
                yield return null;
            }

            yield return new WaitForSeconds(WaitTime);

            isCompleted = true;
        }
    }
}
#endif