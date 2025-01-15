#if ENABLE_ERRORS

using RAY_Core;

namespace RAY_Cossacks
{
    public class MainStorage : BaseMainStorage<MainStorage>, IMainStorageApp
    {
        private protected override void _OnInit()
        {
            base._OnInit();

            BaseState loadingstate = loadingState;

            StateMachine = new StateMachine(
                new("LoadingToGameScene", new LoadingSceneState(Data.GameScene, Data.LoadScene, "GameScene", loadingState)),
                new("LoadingFromGameToGameScene", new LoadingSceneState(Data.GameScene, Data.GameScene, Data.LoadScene, "GameScene", loadingState)),
                new("GameScene", new GameSceneState()));

            StateMachine.OnInit();
        }
        private protected override void _OnDispose()
        {
            
        }
    }
}
#endif