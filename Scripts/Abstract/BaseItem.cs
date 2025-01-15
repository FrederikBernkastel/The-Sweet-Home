#if ENABLE_ERRORS

using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseItem
    {
        private static int staticInstanceID;

        public int InstanceID { get; private protected set; } = staticInstanceID++;

        public bool IsPuted { get; private protected set; } = default;
        public BaseFriendlyCharacter CurrentCharacter { get; private protected set; } = default;
        public BaseViewItem ViewItem { get; private protected set; } = default;
        public BaseViewSlot CurrentSlot { get; private protected set; } = default;

        public abstract void SetPuted(bool isPuted);
        public abstract void SetCurrentCharacter(BaseFriendlyCharacter character);
        public abstract void SetSlot(BaseViewSlot slot);
        public abstract void SetViewItem(BaseViewItem view);

        public abstract void OnInit();

        public abstract BaseItemSO GetItemSO();
    }
}
#endif