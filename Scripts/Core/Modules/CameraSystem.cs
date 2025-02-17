using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RAY_Core
{
    public sealed class CameraSystem : BaseCoreObject
    {
        public static CameraSystem Instance { get; } = new();

        private Dictionary<TypeCamera, BaseViewCamera> pairCameras { get; } = new();
        private Dictionary<TypeVirtualCamera, BaseViewVirtualCamera> pairVirtualCameras { get; } = new();

        public BaseViewVirtualCamera CurrentVirtualCamera { get; private set; } = default;

        public IReadOnlyDictionary<TypeVirtualCamera, BaseViewVirtualCamera> PairVirtualCameras => pairVirtualCameras;
        public IReadOnlyDictionary<TypeCamera, BaseViewCamera> PairCameras => pairCameras;

        private CameraSystem() { }

        public override void Reset(Action resetEvent)
        {
            base.Reset(() => 
            {
                pairCameras.Clear();
                pairVirtualCameras.Clear();

                pairCameras.Init();
                pairVirtualCameras.Init();

                CurrentVirtualCamera = default;
            });
        }
        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                Reset(default);

                return true;
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() =>
            {
                Reset(default);

                return true;
            });
        }
        public void BindingCameraWithCanvas(IViewCanvas viewCanvas)
        {
            if (IsInited)
            {
                if (viewCanvas != default)
                {
                    viewCanvas.Canvas.renderMode = RenderMode.ScreenSpaceCamera;
                    viewCanvas.Canvas.worldCamera = (pairCameras.GetValueOrDefault(TypeCamera.UICamera) as BaseViewCamera)?.Camera;
                }
            }
        }
        public void AddCamera(TypeCamera typeView, BaseViewCamera viewCamera)
        {
            if (IsInited)
            {
                pairCameras[typeView] ??= viewCamera;
            }
        }
        public void RemoveCamera(TypeCamera typeView)
        {
            if (IsInited)
            {
                pairCameras[typeView] = default;
            }
        }
        public void ChangeVirtualCamera(BaseViewVirtualCamera virtualCamera)
        {
            if (IsInited)
            {
                if (CurrentVirtualCamera != virtualCamera)
                {
                    CurrentVirtualCamera?.Show(false);
                    //CurrentVirtualCamera?.EnableIO(false);

                    CurrentVirtualCamera = virtualCamera;

                    CurrentVirtualCamera?.Show(true);
                    //CurrentVirtualCamera?.EnableIO(true);
                }
            }
        }
        public void ChangeVirtualCamera(TypeVirtualCamera typeVirtualCamera)
        {
            if (IsInited)
            {
                if (typeVirtualCamera == TypeVirtualCamera.None || pairVirtualCameras[typeVirtualCamera] != default)
                {
                    if (CurrentVirtualCamera != pairVirtualCameras[typeVirtualCamera])
                    {
                        CurrentVirtualCamera?.Show(false);
                        //CurrentVirtualCamera?.EnableIO(false);

                        CurrentVirtualCamera = pairVirtualCameras[typeVirtualCamera];

                        CurrentVirtualCamera?.Show(true);
                        //CurrentVirtualCamera?.EnableIO(true);
                    }
                }
            }
        }
        public void AddVirtualCamera(TypeVirtualCamera typeVirtualCamera, BaseViewVirtualCamera virtualCamera)
        {
            if (IsInited)
            {
                if (typeVirtualCamera != TypeVirtualCamera.None)
                {
                    pairVirtualCameras[typeVirtualCamera] ??= virtualCamera;
                }
            }
        }
        public void RemoveVirtualCamera(TypeVirtualCamera typeVirtualCamera)
        {
            if (IsInited)
            {
                if (pairVirtualCameras.TryGetValueWithoutKey(typeVirtualCamera, out var _))
                {
                    ChangeVirtualCamera(pairVirtualCameras.Keys.FirstOrDefault(u =>
                        pairVirtualCameras[u] != default &&
                        CurrentVirtualCamera != pairVirtualCameras[u] &&
                        pairVirtualCameras[u].TypeVirtualCamera != typeVirtualCamera));

                    pairVirtualCameras[typeVirtualCamera] = default;
                }
            }
        }
    }
}
