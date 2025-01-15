using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_CuteHome
{
    public interface IViewMenu
    {

    }
    public class ViewMenu : BaseView, IViewCanvas, IViewMenu
    {
        public override string Name { get; } = "ViewMenu";

        [BoxGroup("General")]
        [SerializeField][Required] private protected Canvas canvas;

        public Canvas Canvas => canvas;

        private protected override void __Hide()
        {
            canvas.gameObject.SetActive(false);
        }
        private protected override void __OnInit()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewMenuDefault] ??= this;

            if (!CameraHelper.BindingCameraWithCanvas(
                (IViewCamera)BaseMainStorage.MainStorage.PairView[TypeView.ViewUICamera],
                (IViewCanvas)BaseMainStorage.MainStorage.PairView[TypeView.ViewMenuDefault]))
            {
                throw new Exception();
            }
        }
        private protected override void __OnDispose()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewMenuDefault] = default;
        }
        private protected override void __Show()
        {
            canvas.gameObject.SetActive(true);
        }
    }
}
