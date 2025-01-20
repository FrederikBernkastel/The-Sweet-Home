using Cinemachine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseViewVirtualCamera : BaseView
    {
        [BoxGroup("General")]
        [SerializeField][Required] private protected CinemachineVirtualCameraBase _virtualCamera;
        [SerializeField] private protected bool isHotInput;
        [SerializeField] private protected KeyCode key;
        [SerializeField] private protected TypeVirtualCamera typeVirtualCamera;

        public CinemachineVirtualCameraBase VirtualCamera => _virtualCamera;
        public KeyCode Key
        {
            get => key;
            set => key = value;
        }
        public bool IsHotInput
        {
            get => isHotInput;
            set => isHotInput = value;
        }
        public TypeVirtualCamera TypeVirtualCamera => typeVirtualCamera;

        private protected override void CreateGlobalInstance()
        {
            if (typeVirtualCamera != TypeVirtualCamera.None && typeVirtualCamera != TypeVirtualCamera.Ignore)
            {
                BaseCameraSystem.Instance.AddVirtualCamera(typeVirtualCamera, this);
            }
        }
        private protected override void RemoveGlobalInstance()
        {
            if (typeVirtualCamera != TypeVirtualCamera.None && typeVirtualCamera != TypeVirtualCamera.Ignore)
            {
                BaseCameraSystem.Instance.RemoveVirtualCamera(typeVirtualCamera);
            }
        }
        private protected override void __EnableIO()
        {
            if (isHotInput)
            {
                UserInput.ListEvents += InputUpdate;
            }
        }
        private protected override void __DisableIO()
        {
            if (isHotInput)
            {
                UserInput.ListEvents -= InputUpdate;
            }
        }
        private void InputUpdate()
        {
            if (Input.GetKeyDown(key))
            {
                BaseCameraSystem.Instance.ChangeVirtualCamera(this);
            }
        }
        private protected override void __Show()
        {
            _virtualCamera.enabled = true;
        }
        private protected override void __Hide()
        {
            _virtualCamera.enabled = false;
        }
    }
}
