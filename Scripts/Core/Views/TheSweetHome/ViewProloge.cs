using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RAY_CuteHome
{
    public sealed class ViewProloge : BaseViewUI, IViewCanvas
    {
        [BoxGroup("General")]
        [SerializeField][Required] private Animator animator;
        [BoxGroup("General")]
        [SerializeField][Required] private Canvas canvas;

        [BoxGroup("NameTriggers")]
        [SerializeField][AnimatorParam("animator")] private string paramDefault;
        [BoxGroup("NameTriggers")]
        [SerializeField][AnimatorParam("animator")] private string paramProloge;

        public Canvas Canvas => canvas;

        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                CameraSystem.Instance.BindingCameraWithCanvas(this);

                return true;
            });
        }

        private protected override void OnHide()
        {
            canvas.gameObject.SetActive(false);

            animator.enabled = false;
        }
        private protected override void OnShow()
        {
            canvas.gameObject.SetActive(true);

            animator.enabled = true;

            animator.GetBehaviour<StateMachineAnimation>().SetCommandExit(new TempCommand()
            {
                ViewProloge = this,
            });

            animator.SetTrigger(paramProloge);
        }

        private protected override void OnEnableIO()
        {
            IOSystem.Instance.UIEvent += InputUpdate;
        }
        private protected override void OnDisableIO()
        {
            IOSystem.Instance.UIEvent -= InputUpdate;
        }

        private void InputUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Hide();
                DisableIO();
            }
        }

        private class TempCommand : IBaseCommandMachineAnimator
        {
            public ViewProloge ViewProloge { get; set; }

            public void Execute(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            {
                ViewProloge.Hide();
                ViewProloge.DisableIO();

                LoadingSystem.Instance.StartLoadUnloadResources(u =>
                {
                    u.AddLoadContext(LoadingSystem.Instance.PairContextResources[TypeResources.GameResources]);
                    u.AddLoadContext(LoadingSystem.Instance.PairContextResources[TypeResources.GameUIResources]);
                    u.AddLoadContext(LoadingSystem.Instance.PairContextResources[TypeResources.AdditionalUIResources]);
                    u.AddLoadContext(LoadingSystem.Instance.PairContextResources[TypeResources.StoryUIResources]);
                    u.AddLoadContext(LoadingSystem.Instance.PairContextResources[TypeResources.MainActorResources]);

                    u.WaitTime = 2f;
                    u.SpeedLoadingBar = 0.5f;
                    u.ViewLoading = GraphicsSystem.Instance.PairViewsUI[TypeViewUI.ViewLoadingDefault] as ViewLoading;
                });
            }
        }
    }
}
