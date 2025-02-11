using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_CuteHome
{
    public sealed class ViewAdditionalUICanvas : BaseViewUI, IViewCanvas
    {
        [BoxGroup("General")]
        [SerializeField][Required] private Canvas canvas;

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
        }
        private protected override void OnShow()
        {
            canvas.gameObject.SetActive(true);
        }
    }
}
