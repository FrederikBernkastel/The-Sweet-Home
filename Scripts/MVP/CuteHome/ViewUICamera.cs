using Cinemachine;
using NaughtyAttributes;
using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public class ViewUICamera : BaseView, IViewCamera
    {
        public override string Name { get; } = "ViewUICamera";

        [BoxGroup("General")]
        [SerializeField][Required] private protected Camera _camera;

        public Camera Camera => _camera;

        private protected override void __OnInit()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewUICamera] ??= this;
        }
        private protected override void __OnDispose()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewUICamera] = default;
        }






        public CinemachineVirtualCameraBase LastVirtualCamera { get; }

        public void ChangeVirtualCamera(TypeVirtualCamera typeVirtualCamera) { }
        public void AddVirtualCamera(TypeVirtualCamera typeVirtualCamera, CinemachineVirtualCameraBase virtualCamera) { }
    }
}
