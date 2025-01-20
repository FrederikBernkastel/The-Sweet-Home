using NaughtyAttributes;
using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_CuteHome
{
    public class ViewAdditionalUICanvas : BaseView, IViewCanvas
    {
        [BoxGroup("General")]
        [SerializeField][Required] private protected Canvas canvas;

        public Canvas Canvas => canvas;

        private protected override void __Hide()
        {
            canvas.gameObject.SetActive(false);
        }
        private protected override void __OnInit()
        {
            BaseCameraSystem.Instance.BindingCameraWithCanvas(this);
        }
        private protected override void __Show()
        {
            canvas.gameObject.SetActive(true);
        }
    }
}
