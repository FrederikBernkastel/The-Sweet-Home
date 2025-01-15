#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System.Linq;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseViewItem : BaseView
    {
        public BaseItem CurrentItem { get; private protected set; } = default;

        public abstract void SetCurrentItem(BaseItem item);
        public abstract BaseItemSO GetItemSO();
        public abstract Transform GetTransform();
    }
    public class ViewItem : BaseViewItem
    {
        public override string Name { get; } = "ViewItem";

        [BoxGroup("ItemInfo")]
        [SerializeField][Required] private protected Renderer render;

        [BoxGroup("SO")]
        [SerializeField][Required] private protected BaseItemSO itemSO;

        [BoxGroup("DEBUG")]
        [SerializeField][ReadOnly] private protected int instanceID;

        public override BaseItemSO GetItemSO()
        {
            return itemSO;
        }
        private protected override void _DisableIO()
        {
        
        }
        private protected override void _EnableIO()
        {
        
        }
        private protected override void _Hide()
        {
            render.gameObject.SetActive(false);
        }
        private protected override void _OnAwake()
        {
        
        }
        private protected override void _OnDispose()
        {
        
        }
        private protected override void _Show()
        {
            render.gameObject.SetActive(true);
        }

        public override Transform GetTransform() => render.transform;
        public override void SetCurrentItem(BaseItem item)
        {
            CurrentItem = item;

            instanceID = CurrentItem?.InstanceID ?? 0;
        }
    }
}
#endif