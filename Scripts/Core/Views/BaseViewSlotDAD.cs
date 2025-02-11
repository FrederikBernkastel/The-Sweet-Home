#if ENABLE_ERRORS

using UnityEngine;
using UnityEngine.EventSystems;

namespace RAY_Core
{
    public abstract class BaseViewSlotDAD : BaseViewSlot, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public static BaseItem DragItem { get; private protected set; } = default;

        public abstract void OnBeginDrag(PointerEventData eventData);
        public abstract void OnDrag(PointerEventData eventData);
        public abstract void OnEndDrag(PointerEventData eventData);
        public abstract void CancelDragAndDrop();
        public abstract void SetDragItem(BaseItem item);
    }
}
#endif