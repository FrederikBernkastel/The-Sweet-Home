#if ENABLE_ERRORS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseViewInventory : BaseView
    {
        public BaseFriendlyCharacter CurrentCharacter { get; private protected set; } = default;

        public abstract void SortAll();
        public abstract void SortEquipment();
        public abstract void SortPotion();
        public abstract void SortQuest();
        public abstract void Sort();
        public abstract BaseViewSlot GetFreeSlot();
        public abstract BaseViewItem GetFreeViewItem(BaseItemSO baseItemSO);
        public abstract void SetCurrentCharacter(BaseFriendlyCharacter character);
    }
}
#endif