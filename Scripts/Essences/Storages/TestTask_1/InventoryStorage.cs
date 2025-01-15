#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using UnityEngine;

namespace RAY_TestTask_1
{
    public class InventoryStorage : BaseStorage<InventoryStorage>
    {
        public override string Name { get; } = "InventoryStorage";

        [BoxGroup("General")]
        [SerializeField][Required] private protected Canvas canvasInventory;

        public Canvas CanvasInventory => canvasInventory;

        private protected override void _OnAwake()
        {
            if (CameraStorage.Instance != null)
            {
                var camera = CameraStorage.Instance.UICamera;

                canvasInventory.renderMode = RenderMode.ScreenSpaceCamera;
                canvasInventory.worldCamera = camera;
            }
        }
        private protected override StateMachine CreateStateMachine()
        {
            StateMachine stateMachine = new("InventoryStateMachine");

            return stateMachine;
        }
    }
}
#endif