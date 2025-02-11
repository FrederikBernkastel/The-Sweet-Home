using Cinemachine;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseViewVirtualCamera : BaseView, IIO
    {
        [BoxGroup("General")]
        [SerializeField][Required] private protected CinemachineVirtualCameraBase _virtualCamera;
        [BoxGroup("General")]
        [SerializeField] private protected TypeVirtualCamera typeVirtualCamera;

        public CinemachineVirtualCameraBase VirtualCamera => _virtualCamera;
        public TypeVirtualCamera TypeVirtualCamera => typeVirtualCamera;

        private Vector2 tempVector { get; set; } = default;

        private protected override void CreateGlobalInstance()
        {
            if (typeVirtualCamera != TypeVirtualCamera.None && typeVirtualCamera != TypeVirtualCamera.Ignore)
            {
                CameraSystem.Instance.AddVirtualCamera(typeVirtualCamera, this);
            }
        }
        private protected override void RemoveGlobalInstance()
        {
            if (typeVirtualCamera != TypeVirtualCamera.None && typeVirtualCamera != TypeVirtualCamera.Ignore)
            {
                CameraSystem.Instance.RemoveVirtualCamera(typeVirtualCamera);
            }
        }

        private protected override void OnShow()
        {
            _virtualCamera.enabled = true;
        }
        private protected override void OnHide()
        {
            _virtualCamera.enabled = false;
        }

        public void EnableIO()
        {
            if (_virtualCamera is CinemachineFreeLook camera)
            {
                camera.m_XAxis.Value = tempVector.x;
                camera.m_YAxis.Value = tempVector.y;
            }
        }
        public void DisableIO()
        {
            if (_virtualCamera is CinemachineFreeLook camera)
            {
                tempVector = new(camera.m_XAxis.Value, camera.m_YAxis.Value);
            }
        }

        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                if (_virtualCamera is CinemachineFreeLook camera)
                {
                    tempVector = new(camera.m_XAxis.Value, camera.m_YAxis.Value);
                }

                return initEvent?.Invoke() ?? true;
            });
        }
    }
}
