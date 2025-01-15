using Cinemachine;
using NaughtyAttributes;
using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_CuteHome
{
    public class MainActorStorage : BaseAdditionalStorage
    {
        public override string Name { get; } = "MainActorStorage";

        [BoxGroup("VirtualCameras")]
        [SerializeField][Required] private protected CinemachineVirtualCameraBase cameraMainCharacter;

        private protected override void __OnInit()
        {
            var mainCamera = (IViewCamera)BaseMainStorage.MainStorage.PairView[TypeView.ViewMainCamera];

            mainCamera.AddVirtualCamera(TypeVirtualCamera.CameraMainCharacter, cameraMainCharacter);
        }
        private protected override void __OnDispose()
        {
            if (BaseMainStorage.MainStorage.PairView[TypeView.ViewMainCamera] != default)
            {
                var mainCamera = (IViewCamera)BaseMainStorage.MainStorage.PairView[TypeView.ViewMainCamera];

                mainCamera.AddVirtualCamera(TypeVirtualCamera.CameraMainCharacter, default);
            }
        }
    }
}
