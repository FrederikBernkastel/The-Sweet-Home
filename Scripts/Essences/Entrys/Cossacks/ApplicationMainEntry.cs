#if ENABLE_ERRORS

using RAY_Core;

namespace RAY_Cossacks
{
    public class ApplicationMainEntry : BaseApplicationEntry<ApplicationMainEntry>, IMainEntryApp
    {
        private protected override void OnInit()
        {
            //BaseStorage.MainStorage.Instance.MainStateMachine.SetState("LoadingToGameScene");
        }
        private protected override void OnDispose()
        {
            
        }
    }
}
#endif