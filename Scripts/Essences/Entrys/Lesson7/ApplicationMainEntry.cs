#if ENABLE_ERRORS

using RAY_Core;

namespace RAY_Lesson7
{
    public class ApplicationMainEntry : BaseApplicationEntry<ApplicationMainEntry>, IMainEntryApp
    {
        private protected override void OnInit()
        {
            //MainStorage.Instance.MainStateMachine.SetState("LoadingToMenuScene");
        }
        private protected override void OnDispose()
        {
            
        }
    }
}
#endif