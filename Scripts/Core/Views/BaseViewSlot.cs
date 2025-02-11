#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

namespace RAY_Core
{
    public abstract class BaseViewSlot : BaseView
    {
        //[BoxGroup("Anchor")]
        //[SerializeField] private protected Vector3 scale;

        //[BoxGroup("General")]
        //[SerializeField] private protected TypeItem typeItem;

        public BaseItem CurrentItem { get; private protected set; } = default;

        public abstract void SetCurrentItem(BaseItem item);
        public abstract void AttachItem();
        //public abstract void SetItem3D(BaseItem item, Vector2 position, Vector3 scale);
        public abstract bool IsAvailable(BaseItem item);
    }
}
#endif