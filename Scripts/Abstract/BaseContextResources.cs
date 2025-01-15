using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAY_Core
{
    public interface IBaseContextResources : ICoreObject
    {
        public string NameResources { get; }
        
        public bool IsLoaded();

        public static bool IsExistContext(string name)
        {
            var scene = SceneManager.GetSceneByName(name);

            return scene.IsValid();
        }
    }
    public class ContextResources : BaseCoreObject, IBaseContextResources
    {
        public override string Name { get; } = "ContextResources";

        public string NameResources => _name;
        private string _name { get; set; } = default;
        private Scene scene { get; set; } = default;

        public ContextResources(string name)
        {
            _name = name;
        }

        private protected override void _OnInit()
        {
            scene = SceneManager.GetSceneByName(_name);
        }
        public bool IsLoaded()
        {
            return scene.isLoaded;
        }
    }
}
