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
    public sealed class ViewLoading : BaseViewUI, IViewCanvas
    {
        [BoxGroup("General")]
        [SerializeField][Required] private RectTransform progressBar;
        [BoxGroup("General")]
        [SerializeField][Required] private TMP_Text text;
        [BoxGroup("General")]
        [SerializeField][Required] private Animator animator;
        [BoxGroup("General")]
        [SerializeField][Required] private Canvas canvas;

        [BoxGroup("NameTriggers")]
        [SerializeField][AnimatorParam("animator")] private string paramDefault;
        [BoxGroup("NameTriggers")]
        [SerializeField][AnimatorParam("animator")] private string paramLoading;

        private float progress { get; set; } = 0;

        public Canvas Canvas => canvas;

        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                progress = progressBar.sizeDelta.x;

                CameraSystem.Instance.BindingCameraWithCanvas(this);

                return true;
            });
        }

        public void SetProgress(float progress)
        {
            progressBar.sizeDelta = new(this.progress * progress, progressBar.sizeDelta.y);
            text.text = string.Format("{0:0}%", progress * 100);
        }

        private protected override void OnHide()
        {
            animator.SetTrigger(paramDefault);

            canvas.gameObject.SetActive(false);
        }
        private protected override void OnShow()
        {
            canvas.gameObject.SetActive(true);

            animator.SetTrigger(paramLoading);
        }
    }
}
#endif