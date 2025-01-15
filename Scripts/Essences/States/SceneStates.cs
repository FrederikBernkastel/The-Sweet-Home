#if ENABLE_ERRORS

using RAY_Core;
using RAY_Cossacks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAY_Core
{
    public class GameSceneState : BaseState
    {
        public override string NameState => "GameScene";
    }
    public class MenuSceneState : BaseState
    {
        public override string NameState => "MenuScene";
    }
    public class LoadingSceneState : BaseState
    {
        public override string NameState => "LoadingScene";

        public static string OldScene { get; private set; }
        public static string NewScene { get; private set; }
        public static string TargetState { get; private set; }
        public static string LoadScene { get; private set; }
        public static BaseState LoadingState { get; private set; }

        private string oldScene;
        private string newScene;
        private string targetState;
        private string loadScene;
        private BaseState loadingState;
        
        public LoadingSceneState(string oldScene, string newScene, string loadScene, string targetState, BaseState loadingState)
        {
            this.oldScene = oldScene;
            this.newScene = newScene;
            this.targetState = targetState;
            this.loadScene = loadScene;
            this.loadingState = loadingState;
        }
        public LoadingSceneState(string newScene, string loadScene, string targetState, BaseState loadingState)
        {
            this.newScene = newScene;
            this.targetState = targetState;
            this.loadScene = loadScene;
            this.loadingState = loadingState;
        }

        private protected override void Enter()
        {
            OldScene = oldScene;
            NewScene = newScene;
            TargetState = targetState;
            LoadScene = loadScene;
            LoadingState = loadingState;

            SceneManager.LoadScene(LoadScene, LoadSceneMode.Additive);
        }
        private protected override void Exit()
        {
            OldScene = string.Empty;
            NewScene = string.Empty;
            TargetState = string.Empty;
            LoadScene = string.Empty;
            LoadingState = default;
        }
    }
}
#endif