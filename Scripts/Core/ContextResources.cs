using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAY_Core
{
    public sealed class ContextResources : BaseCoreObject
    {
        public string NameResources { get; } = default;
        public TypeResources TypeResources { get; } = default;
        private Scene scene { get; set; } = default;

        public ContextResources(string name, TypeResources typeResources)
        {
            NameResources = name;
            TypeResources = typeResources;
        }

        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                scene = SceneManager.GetSceneByName(NameResources);

                return true;
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() =>
            {
                scene = default;

                return true;
            });
        }

        public bool IsLoaded() => IsInited && scene.isLoaded;
    }
}
