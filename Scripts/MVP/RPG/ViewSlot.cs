#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RAY_TestTask_1
{
    public class ViewSlot : BaseViewSlotDAD
    {
        public override string Name { get; } = "ViewSlot";

        [BoxGroup("Anchor")]
        [SerializeField] private protected Vector3 scale;

        [BoxGroup("DEBUG")]
        [SerializeField][ReadOnly] private protected int instanceID;

        public override bool IsAvailable(BaseItem item)
        {
            return item != default;
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            if (DragItem != default)
            {
                var results = new List<RaycastResult>();
                GetComponentInParent<GraphicRaycaster>().Raycast(eventData, results);

                bool flag = false;
            
                foreach (var hit in results)
                {
                    if (hit.gameObject.TryGetComponent<BaseViewSlot>(out var slot))
                    {
                        if (slot.CurrentItem != default)
                        {
                            if (DragItem.CurrentSlot.IsAvailable(slot.CurrentItem) && slot.IsAvailable(DragItem))
                            {
                                BaseViewSlot slotDrag = DragItem.CurrentSlot;
                                BaseViewSlot slotDrop = slot;
                                BaseItem itemDrag = DragItem;
                                BaseItem itemDrop = slot.CurrentItem;

                                slotDrag.SetCurrentItem(itemDrop);
                                itemDrop.SetSlot(slotDrag);

                                slotDrag.AttachItem();

                                slotDrop.SetCurrentItem(itemDrag);
                                itemDrag.SetSlot(slotDrop);

                                slotDrop.AttachItem();

                                flag = true;
                            }
                        }
                        else
                        {
                            if (slot.IsAvailable(DragItem))
                            {
                                BaseViewSlot slotDrag = DragItem.CurrentSlot;
                                BaseViewSlot slotDrop = slot;
                                BaseItem itemDrag = DragItem;

                                slotDrag.SetCurrentItem(default);
                                slotDrop.SetCurrentItem(itemDrag);
                                itemDrag.SetSlot(slotDrop);

                                slotDrop.AttachItem();

                                flag = true;
                            }
                        }

                        break;
                    }
                }

                if (!flag)
                {
                    (DragItem.CurrentSlot as BaseViewSlotDAD).CancelDragAndDrop();
                }
            }

            SetDragItem(default);
        }
        public override void OnBeginDrag(PointerEventData eventData)
        {
            SetDragItem(CurrentItem);
        }
        public override void OnDrag(PointerEventData eventData)
        {
            if (DragItem != default)
            {
                if (DragItem.ViewItem != default)
                {
                    if (CameraStorage.Instance != default)
                    {
                        //Debug.Log("HELLOOO");
                        var pos = CameraStorage.Instance.UICamera.ScreenToWorldPoint(eventData.position);
                        DragItem.ViewItem.GetTransform().position = new Vector3(pos.x, pos.y, DragItem.ViewItem.GetTransform().position.z);
                    }
                }
            }
        }
        public override void CancelDragAndDrop()
        {
            AttachItem();
        }
        public override void AttachItem()
        {
            if (CurrentItem != default)
            {
                if (CurrentItem.ViewItem != default)
                {
                    CurrentItem.ViewItem.GetTransform().localPosition = new Vector3(
                        transform.position.x,
                        transform.position.y,
                        CurrentItem.ViewItem.GetTransform().localPosition.z);
                    CurrentItem.ViewItem.GetTransform().localScale = scale;
                }
            }
        }
        public override void SetCurrentItem(BaseItem item)
        {
            CurrentItem = item;

            instanceID = CurrentItem?.InstanceID ?? 0;
        }
        public override void SetDragItem(BaseItem item)
        {
            DragItem = item;
        }

        private protected override void _DisableIO()
        {

        }
        private protected override void _EnableIO()
        {

        }
        private protected override void _Hide()
        {
        
        }
        private protected override void _OnAwake()
        {
        
        }
        private protected override void _OnDispose()
        {

        }
        private protected override void _Show()
        {
        
        }
    }
}
#endif