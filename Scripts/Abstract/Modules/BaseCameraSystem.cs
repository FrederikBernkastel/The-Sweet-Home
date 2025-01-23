using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseCameraSystem
    {
        public static BaseCameraSystem Instance { get; } = new CameraSystem().OnInit();

        public Dictionary<TypeCamera, BaseViewCamera> PairCameras { get; } = new();
        public Dictionary<TypeVirtualCamera, BaseViewVirtualCamera> PairVirtualCameras { get; } = new();
        public BaseViewVirtualCamera CurrentVirtualCamera { get; private protected set; } = default;

        public abstract void AddCamera(TypeCamera typeView, BaseViewCamera viewCamera);
        public abstract void RemoveCamera(TypeCamera typeView);
        public abstract void ChangeVirtualCamera(TypeVirtualCamera typeVirtualCamera);
        public abstract void ChangeVirtualCamera(BaseViewVirtualCamera virtualCamera);
        public abstract void AddVirtualCamera(TypeVirtualCamera typeVirtualCamera, BaseViewVirtualCamera virtualCamera);
        public abstract void RemoveVirtualCamera(TypeVirtualCamera typeVirtualCamera);
        private protected virtual BaseCameraSystem OnInit() => this;
        public abstract void BindingCameraWithCanvas(IViewCanvas viewCanvas);

        private class CameraSystem : BaseCameraSystem
        {
            private protected override BaseCameraSystem OnInit()
            {
                PairCameras.Clear();
                PairVirtualCameras.Clear();

                foreach (var s in Enum.GetValues(typeof(TypeCamera)))
                {
                    PairCameras.Add((TypeCamera)s, default);
                }
                foreach (var s in Enum.GetValues(typeof(TypeVirtualCamera)))
                {
                    PairVirtualCameras.Add((TypeVirtualCamera)s, default);
                }

                return this;
            }
            public override void BindingCameraWithCanvas(IViewCanvas viewCanvas)
            {
                if (viewCanvas != default)
                {
                    viewCanvas.Canvas.renderMode = RenderMode.ScreenSpaceCamera;
                    viewCanvas.Canvas.worldCamera = (PairCameras.GetValueOrDefault(TypeCamera.UICamera) as BaseViewCamera)?.Camera;
                }
            }
            public override void AddCamera(TypeCamera typeView, BaseViewCamera viewCamera)
            {
                PairCameras[typeView] ??= viewCamera;
            }
            public override void RemoveCamera(TypeCamera typeView)
            {
                PairCameras[typeView] = default;
            }
            public override void ChangeVirtualCamera(BaseViewVirtualCamera virtualCamera)
            {
                if (CurrentVirtualCamera != virtualCamera)
                {
                    CurrentVirtualCamera?.Show(false);
                    CurrentVirtualCamera?.EnableIO(false);

                    CurrentVirtualCamera = virtualCamera;

                    CurrentVirtualCamera?.Show(true);
                    CurrentVirtualCamera?.EnableIO(true);
                }
            }
            public override void ChangeVirtualCamera(TypeVirtualCamera typeVirtualCamera)
            {
                if (typeVirtualCamera == TypeVirtualCamera.None || PairVirtualCameras[typeVirtualCamera] != default)
                {
                    if (CurrentVirtualCamera != PairVirtualCameras[typeVirtualCamera])
                    {
                        CurrentVirtualCamera?.Show(false);
                        CurrentVirtualCamera?.EnableIO(false);

                        CurrentVirtualCamera = PairVirtualCameras[typeVirtualCamera];

                        CurrentVirtualCamera?.Show(true);
                        CurrentVirtualCamera?.EnableIO(true);
                    }
                }
            }
            public override void AddVirtualCamera(TypeVirtualCamera typeVirtualCamera, BaseViewVirtualCamera virtualCamera)
            {
                if (typeVirtualCamera != TypeVirtualCamera.None)
                {
                    PairVirtualCameras[typeVirtualCamera] ??= virtualCamera;
                }
            }
            public override void RemoveVirtualCamera(TypeVirtualCamera typeVirtualCamera)
            {
                if (PairVirtualCameras.TryGetValueWithoutKey(typeVirtualCamera, out var _))
                {
                    ChangeVirtualCamera(PairVirtualCameras.Keys.FirstOrDefault(u =>
                        PairVirtualCameras[u] != default &&
                        CurrentVirtualCamera != PairVirtualCameras[u] &&
                        PairVirtualCameras[u].TypeVirtualCamera != typeVirtualCamera));

                    PairVirtualCameras[typeVirtualCamera] = default;
                }
            }
        }
    }
}
