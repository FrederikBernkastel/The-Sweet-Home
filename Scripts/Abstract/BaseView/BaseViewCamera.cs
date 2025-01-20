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
        [SerializeField] private protected TypeCamera typeCamera;

        public Camera Camera => _camera;

        private protected override void CreateGlobalInstance()
        {
            BaseCameraSystem.Instance.AddCamera(typeCamera, this);
        }
        private protected override void RemoveGlobalInstance()
        {
            BaseCameraSystem.Instance.RemoveCamera(typeCamera);
        }
        private protected override void __Show()
        {
            _camera.enabled = true;
        }
        private protected override void __Hide()
        {
            _camera.enabled = false;
        }
    }
}
