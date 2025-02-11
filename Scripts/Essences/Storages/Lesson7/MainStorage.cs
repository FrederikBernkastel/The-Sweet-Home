#if ENABLE_ERRORS

using RAY_Core;

namespace RAY_Lesson7
{
    public class MainStorage : BaseMainStorage<MainStorage>
    {
        private protected override void _OnInit()
        {
            base._OnInit();

            BaseState loadingstate = loadingState;

            MainStateMachine = new StateMachine(
                new("LoadingToMenuScene", new LoadingSceneState(Data.MenuScene, Data.LoadScene, "MenuScene", loadingState)),
                new("LoadingFromMenuToGameScene", new LoadingSceneState(Data.MenuScene, Data.GameScene, Data.LoadScene, "GameScene", loadingState)),
                new("LoadingFromGameToMenuScene", new LoadingSceneState(Data.GameScene, Data.MenuScene, Data.LoadScene, "MenuScene", loadingState)),
                new("GameScene", new GameSceneState()),
                new("MenuScene", new MenuSceneState()));

            BaseStorage.MainStateMachine = MainStateMachine;

            MainStateMachine.OnInit();
        }
        private protected override void _OnDispose()
        {
            BaseStorage.MainStateMachine = default;
        }
    }
}
#endif