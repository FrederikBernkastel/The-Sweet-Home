using Cinemachine;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RAY_Core
{
    public interface IViewCamera
    {
        public Camera Camera { get; }
        public CinemachineVirtualCameraBase LastVirtualCamera { get; }

        public void ChangeVirtualCamera(TypeVirtualCamera typeVirtualCamera);
        public void AddVirtualCamera(TypeVirtualCamera typeVirtualCamera, CinemachineVirtualCameraBase virtualCamera);
    }
    public class ViewMainCamera : BaseView, IViewCamera
    {
        public override string Name { get; } = "ViewMainCamera";

        [BoxGroup("General")]
        [SerializeField][Required] private protected Camera _camera;

        private Dictionary<TypeVirtualCamera, CinemachineVirtualCameraBase> pairVirtualCameras { get; set; } = default;

        public Camera Camera => _camera;
        public CinemachineVirtualCameraBase LastVirtualCamera { get; private protected set; } = default;

        private protected override void __OnInit()
        {
            pairVirtualCameras = new();

            foreach (var s in Enum.GetValues(typeof(TypeVirtualCamera)))
            {
                pairVirtualCameras.Add((TypeVirtualCamera)s, default);
            }

            BaseMainStorage.MainStorage.PairView[TypeView.ViewMainCamera] ??= this;
        }
        private protected override void __OnDispose()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewMainCamera] = default;

            pairVirtualCameras.Clear();
            pairVirtualCameras = default;
        }

        public void ChangeVirtualCamera(TypeVirtualCamera typeVirtualCamera)
        {
            if (pairVirtualCameras[typeVirtualCamera] == default)
            {
                throw new Exception();
            }

            if (LastVirtualCamera != default)
            {
                LastVirtualCamera.enabled = false;
            }

            LastVirtualCamera = pairVirtualCameras[typeVirtualCamera];

            LastVirtualCamera.enabled = true;
        }
        public void AddVirtualCamera(TypeVirtualCamera typeVirtualCamera, CinemachineVirtualCameraBase virtualCamera)
        {
            if (pairVirtualCameras[typeVirtualCamera] != default)
            {
                pairVirtualCameras[typeVirtualCamera].enabled = false;
            }
            
            pairVirtualCameras[typeVirtualCamera] = virtualCamera;

            if (virtualCamera != default)
            {
                virtualCamera.enabled = false;
            }
        }
    }
}
