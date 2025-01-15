#if !ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RAY_Core
{
    public interface IViewLoading
    {
        public void SetProgress(float progress);
    }
    public interface IViewCanvas
    {
        public Canvas Canvas { get; }
    }
    public class ViewLoading : BaseView, IViewLoading, IViewCanvas
    {
        public override string Name { get; } = "ViewLoading";

        [BoxGroup("General")]
        [SerializeField][Required] private protected RectTransform progressBar;
        [SerializeField][Required] private protected TMP_Text text;
        [SerializeField][Required] private protected Animator animator;
        [SerializeField][Required] private protected Canvas canvas;

        [BoxGroup("NameTriggers")]
        [SerializeField][AnimatorParam("animator")] private protected string paramDefault;
        [SerializeField][AnimatorParam("animator")] private protected string paramLoading;

        private float progress;

        public Canvas Canvas => canvas;

        public void SetProgress(float progress)
        {
            progressBar.sizeDelta = new(this.progress * progress, progressBar.sizeDelta.y);
            text.text = string.Format("{0:0}%", progress * 100);
        }

        private protected override void __Hide()
        {
            animator.SetTrigger(paramDefault);

            canvas.gameObject.SetActive(false);
        }
        private protected override void __OnInit()
        {
            progress = progressBar.sizeDelta.x;

            BaseMainStorage.MainStorage.PairView[TypeView.ViewLoadingDefault] ??= this;

            if (!CameraHelper.BindingCameraWithCanvas(
                (IViewCamera)BaseMainStorage.MainStorage.PairView[TypeView.ViewUICamera],
                (IViewCanvas)BaseMainStorage.MainStorage.PairView[TypeView.ViewLoadingDefault]))
            {
                throw new Exception();
            }
        }
        private protected override void __OnDispose()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewLoadingDefault] = default;
        }
        private protected override void __Show()
        {
            canvas.gameObject.SetActive(true);

            animator.SetTrigger(paramLoading);
        }
    }
}
#endif