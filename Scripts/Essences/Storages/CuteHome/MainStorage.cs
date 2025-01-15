#if !ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAY_CuteHome
{
    public class MainStorage : BaseMainStorage
    {
        public override string Name { get; } = "MainStorage";

        private protected override void __OnInit()
        {

        }
        private protected override StateMachine CreateStateMachine()
        {
            StateMachine stateMachine = new("MainStateMachine",
                new KeyValuePair<string, BaseState>("GameState", new MainState()));

            //MainStateMachine = new StateMachine(
            //    new("LoadingToGameScene", new LoadingSceneState(Data.GameScene, Data.LoadScene, "GameScene", loadingState)),
            //    new("LoadingFromGameToGameScene", new LoadingSceneState(Data.GameScene, Data.MenuScene, Data.LoadScene, "GameScene", loadingState)),
            //    new("GameScene", new GameSceneState()));

            return stateMachine;
        }
    }
    [Serializable]
    public class ContextResourcesField : BaseCoreObject
    {
        [BoxGroup("ResourcesField")]
        [SerializeField][Scene] private protected string nameResources;
        [BoxGroup("ResourcesField")]
        [SerializeField] private protected TypeResources typeResources;

        public override string Name { get; } = "ContextResourcesField";

        private protected override void _OnInit()
        {
            ContextResources context = new(nameResources);

            context.OnInit();

            BaseMainStorage.MainStorage.PairContextResources[typeResources] = context;
        }
    }
}
#endif