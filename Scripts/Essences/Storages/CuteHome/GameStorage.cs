using Cinemachine;
using NaughtyAttributes;
using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_CuteHome
{
    public class GameStorage : BaseAdditionalStorage
    {
        public override string Name { get; } = "GameStorage";

        [BoxGroup("VirtualCameras")]
        [SerializeField][Required] private protected CinemachineVirtualCameraBase cameraMenu;

        private protected override void __OnInit()
        {
            var mainCamera = (IViewCamera)BaseMainStorage.MainStorage.PairView[TypeView.ViewMainCamera];

            mainCamera.AddVirtualCamera(TypeVirtualCamera.CameraMenu, cameraMenu);
        }
        private protected override void __OnDispose()
        {
            if (BaseMainStorage.MainStorage.PairView[TypeView.ViewMainCamera] != default)
            {
                var mainCamera = (IViewCamera)BaseMainStorage.MainStorage.PairView[TypeView.ViewMainCamera];

                mainCamera.AddVirtualCamera(TypeVirtualCamera.CameraMenu, default);
            }
        }
    }
}
