using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseViewCamera : BaseView
    {
        [BoxGroup("General")]
        [SerializeField][Required] private protected Camera _camera;
        [BoxGroup("General")]
        [SerializeField] private protected TypeCamera typeCamera;

        public Camera Camera => _camera;
        public TypeCamera TypeCamera => typeCamera;

        private protected override void CreateGlobalInstance()
        {
            if (typeCamera != TypeCamera.Ignore)
            {
                CameraSystem.Instance.AddCamera(typeCamera, this);
            }
        }
        private protected override void RemoveGlobalInstance()
        {
            if (typeCamera != TypeCamera.Ignore)
            {
                CameraSystem.Instance.RemoveCamera(typeCamera);
            }
        }

        private protected override void OnShow()
        {
            _camera.enabled = true;
        }
        private protected override void OnHide()
        {
            _camera.enabled = false;
        }
    }
}
