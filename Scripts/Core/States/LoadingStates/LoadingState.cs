#if !ENABLE_ERRORS

using RAY_CuteHome;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAY_Core
{
    public sealed class NonAsyncLoadingState : BaseState
    {
        private List<ContextResources> listUnloadContext { get; } = new();
        private List<ContextResources> listLoadContext { get; } = new();

        public override void EnableUpdateType()
        {
            
        }

        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                Reset(default);

                return true;
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() => 
            {
                Reset(default);

                return true;
            });
        }
        public override void OnEnter(Action enterEvent)
        {
            base.OnEnter(() => 
            {
                NonAsyncLoading();
            });
        }
        public override void Reset(Action resetEvent)
        {
            base.Reset(() => 
            {
                listUnloadContext.Clear();
                listLoadContext.Clear();
            });
        }
        private void NonAsyncLoading()
        {
            AsyncOperation asyncOperation;

            foreach (var s in listUnloadContext)
            {
                if (s.IsLoaded())
                {
                    asyncOperation = SceneManager.UnloadSceneAsync(s.NameResources);

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
        public void AddUnloadContext(ContextResources baseContextResources)
        {
            if (IsInited)
            {
                listUnloadContext.Add(baseContextResources);
            }
        }
        public void AddLoadContext(ContextResources baseContextResources)
        {
            if (IsInited)
            {
                listLoadContext.Add(baseContextResources);
            }
        }
    }
    public sealed class LoadingState : BaseState
    {
        public float WaitTime { get; set; } = default;
        public float SpeedLoadingBar { get; set; } = default;
        private float currentValue { get; set; } = default;
        private float targetValue { get; set; } = default;
        public ViewLoading ViewLoading { get; set; } = default;

        private List<ContextResources> listUnloadContext { get; } = new();
        private List<ContextResources> listLoadContext { get; } = new();

        private bool isCompleted { get; set; } = false;

        public override void EnableUpdateType()
        {
            EnableUpdateType(TypeUpdate.Update);
        }

        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() =>
            {
                Reset(default);

                return true;
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() =>
            {
                Reset(default);

                return true;
            });
        }
        public override void OnEnter(Action enterEvent)
        {
            base.OnEnter(() =>
            {
                ApplicationEntry.MainEntry.StartCoroutine(Loading());

                if (ViewLoading != default)
                {
                    ViewLoading.Show(true);
                    ViewLoading.EnableIO(true);
                }
            });
        }
        public override void Reset(Action resetEvent)
        {
            base.Reset(() =>
            {
                listUnloadContext.Clear();
                listLoadContext.Clear();

                isCompleted = false;
                ViewLoading = default;
                WaitTime = default;
                SpeedLoadingBar = default;
                currentValue = default;
                targetValue = default;
            });
        }
        public override void OnUpdate(Action updateEvent)
        {
            base.OnUpdate(() => 
            {
                currentValue = Mathf.MoveTowards(currentValue, targetValue, SpeedLoadingBar * Time.deltaTime);

                if (ViewLoading != default)
                {
                    ViewLoading.SetProgress(currentValue);
                }
                if (isCompleted)
                {
                    OnExit(default);
                }
            });
        }
        public override void OnExit(Action exitEvent)
        {
            base.OnExit(() => 
            {
                UpdateSystem.Instance.AddUpdateObjectToDispose(this);

                if (ViewLoading != default)
                {
                    ViewLoading.Show(false);
                    ViewLoading.EnableIO(false);
                }
            });
        }
        public void AddUnloadContext(ContextResources baseContextResources)
        {
            if (IsInited)
            {
                listUnloadContext.Add(baseContextResources);
            }
        }
        public void AddLoadContext(ContextResources baseContextResources)
        {
            if (IsInited)
            {
                listLoadContext.Add(baseContextResources);
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
                    asyncOperation = SceneManager.UnloadSceneAsync(s.NameResources);

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