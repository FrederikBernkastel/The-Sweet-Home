#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RAY_TestTask_1
{
    public class ViewSlotPut : ViewSlot
    {
        [BoxGroup("General")]
        [SerializeField] private protected TypeItem typeItem;

        public override bool IsAvailable(BaseItem item)
        {
            return item.GetItemSO().TypeItem == typeItem && item is ArmorItem;
        }
        public override void SetCurrentItem(BaseItem item)
        {
            if (CurrentItem != default)
            {
                CurrentItem.CurrentCharacter.PutOffArmor(CurrentItem.GetItemSO().TypeItem);
            }
        
            base.SetCurrentItem(item);

            if (CurrentItem != default)
            {
                CurrentItem.CurrentCharacter.PutOnArmor(CurrentItem as ArmorItem);
            }
        }
        //public override void OnBeginDrag(PointerEventData eventData)
        //{
        //    SetDragItem(default);
        //}
        public override void OnEndDrag(PointerEventData eventData)
        {
            if (DragItem != default)
            {
                (DragItem.CurrentSlot as BaseViewSlotDAD).CancelDragAndDrop();
            }
        }
    }
}
#endif