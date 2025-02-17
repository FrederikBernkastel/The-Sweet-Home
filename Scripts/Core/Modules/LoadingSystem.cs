#if !ENABLE_ERRORS

using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAY_Core
{
    public sealed class LoadingSystem : BaseCoreObject
    {
        public static LoadingSystem Instance { get; } = new();

        private Dictionary<TypeResources, ContextResources> pairContextResources { get; } = new();
        private NonAsyncLoadingState cacheNonAsyncState { get; set; } = default;
        private LoadingState cacheAsyncState { get; set; } = default;

        public IReadOnlyDictionary<TypeResources, ContextResources> PairContextResources => pairContextResources;

        private LoadingSystem() { }

        public override void Reset(Action resetEvent)
        {
            base.Reset(() => 
            {
                pairContextResources.Clear();

                pairContextResources.Init();

                cacheNonAsyncState = default;
                cacheAsyncState = default;
            });
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

        public void AddContextResources(ContextResources resources)
        {
            if (IsInited)
            {
                pairContextResources[resources.TypeResources] = resources;
            }
        }
        public void RemoveContextResources(TypeResources typeResources)
        {
            if (IsInited)
            {
                if (pairContextResources.TryGetValueWithoutKey(typeResources, out var val))
                {
                    pairContextResources[typeResources] = default;
                }
            }
        }
        public void StartLoadUnloadResources(Action<LoadingState> action)
        {
            if (IsInited)
            {
                if (cacheAsyncState == default)
                {
                    cacheAsyncState = new();

                    if (!cacheAsyncState.OnInit(default))
                    {
                        throw new Exception();
                    }
                }

                cacheAsyncState.Reset(default);

                action.Invoke(cacheAsyncState);

                cacheAsyncState.OnEnter(default);

                cacheAsyncState.EnableUpdateType(TypeUpdate.Update);

                UpdateSystem.Instance.AddUpdateObject("ContextLoading", cacheAsyncState);
            }
        }
        public void StartNonAsyncLoadUnloadResources(Action<NonAsyncLoadingState> action)
        {
            if (IsInited)
            {
                if (cacheNonAsyncState == default)
                {
                    cacheNonAsyncState = new();

                    if (!cacheNonAsyncState.OnInit(default))
                    {
                        throw new Exception();
                    }
                }

                cacheNonAsyncState.Reset(default);

                action.Invoke(cacheNonAsyncState);

                cacheNonAsyncState.OnEnter(default);

                cacheNonAsyncState.OnExit(default);
            }
        }
    }
}
#endif