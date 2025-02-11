#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using UnityEngine;

namespace RAY_TestTask_1
{
    public class CameraStorage : BaseAdditionalStorage
    {
        public override string Name { get; } = "CameraStorage";

        [BoxGroup("Cameras")]
        [SerializeField][Required] public Camera UICamera;
        [SerializeField][Required] public Camera MainCamera;

        private protected override void __OnInit()
        {
            if (InventoryStorage.Instance != null)
            {
                var canvas = InventoryStorage.Instance.CanvasInventory;

                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = UICamera;
            }
        }
    }
}
#endif