using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RAY_CuteHome
{
    public interface IViewProloge
    {

    }
    public class ViewProloge : BaseView, IViewProloge, IViewCanvas
    {
        public override string Name { get; } = "ViewProloge";

        [BoxGroup("General")]
        [SerializeField][Required] private protected Animator animator;
        [SerializeField][Required] private protected Canvas canvas;

        [BoxGroup("NameTriggers")]
        [SerializeField][AnimatorParam("animator")] private protected string paramDefault;
        [SerializeField][AnimatorParam("animator")] private protected string paramProloge;

        public Canvas Canvas => canvas;

        private protected override void __Hide()
        {
            canvas.gameObject.SetActive(false);
        }
        private protected override void __OnInit()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewPrologeDefault] ??= this;

            if (!CameraHelper.BindingCameraWithCanvas(
                (IViewCamera)BaseMainStorage.MainStorage.PairView[TypeView.ViewUICamera],
                (IViewCanvas)BaseMainStorage.MainStorage.PairView[TypeView.ViewPrologeDefault]))
            {
                throw new Exception();
            }
        }
        private protected override void __OnDispose()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewPrologeDefault] = default;
        }
        private protected override void __Show()
        {
            canvas.gameObject.SetActive(true);

            animator.GetBehaviour<StateMachineAnimation>().SetCommandExit(new TempCommand()
            {
                ViewProloge = this,
            });

            animator.SetTrigger(paramProloge);
        }

        private class TempCommand : IBaseCommandMachineAnimator
        {
            public ViewProloge ViewProloge { get; set; }
            
            public void Execute(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            {
                ViewProloge.Show(false);
                ViewProloge.EnableIO(false);

                if (BaseMainStorage.MainStorage.PairView[TypeView.ViewLoadingDefault] == default)
                {
                    throw new Exception();
                }

                LoadingSystem.StartLoadUnloadResources(u =>
                {
                    u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.GameResources]);
                    u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.GameUIResources]);
                    u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.AdditionalUIResources]);
                    u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.StoryUIResources]);
                    u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.MainActorResources]);

                    u.WaitTime = 2f;
                    u.SpeedLoadingBar = 0.5f;
                    u.ViewLoading = (ViewLoading)BaseMainStorage.MainStorage.PairView[TypeView.ViewLoadingDefault];
                });
            }
        }
    }
}
