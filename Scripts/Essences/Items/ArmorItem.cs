#if ENABLE_ERRORS

using RAY_Core;
using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace RAY_TestTask_1
{
    public class ArmorItem : BaseItem
    {
        public ArmorSO ArmorSO { get; private protected set; }

        public ArmorItem(ArmorSO armorSO)
        {
            ArmorSO = armorSO;
        }

        public override BaseItemSO GetItemSO() => ArmorSO;
        public override void SetPuted(bool isPuted)
        {
            IsPuted = isPuted;
        }
        public override void SetViewItem(BaseViewItem view)
        {
            ViewItem = view;
        }
        public override void SetSlot(BaseViewSlot slot)
        {
            CurrentSlot = slot;
        }
        public override void SetCurrentCharacter(BaseFriendlyCharacter character)
        {
            CurrentCharacter = character;
        }
        //public override void SetPuted(TypeBool flag, bool isRefresh)
        //{
        //    if (flag == TypeBool.None || IsPuted == flag)
        //    {
        //        return;
        //    }

        //    IsPuted = flag;

        //    if (CurrentCharacter != default)
        //    {
        //        if (IsPuted == TypeBool.True)
        //        {
        //            CurrentCharacter.PutOffArmor(GetBaseItemSO().TypeItem);

        //            CurrentCharacter.PutOnArmor(this);
        //        }
        //        else
        //        {
        //            CurrentCharacter.PutOffArmor(GetBaseItemSO().TypeItem);
        //        }
        //    }
        //}
        //public override bool SetCurrentCharacter(Character character, bool isRefresh)
        //{
        //    if (CurrentCharacter == character)
        //    {
        //        return false;
        //    }

        //    SetPuted(TypeBool.False, true);

        //    if (CurrentCharacter != default)
        //    {
        //        CurrentCharacter.SubItemFromInventory(this);
        //    }

        //    CurrentCharacter = character;

        //    if (CurrentCharacter != default)
        //    {
        //        CurrentCharacter.AddItemToInventory(this);
        //    }

        //    return true;
        //}
        //public override bool SetSlot(BaseViewSlot slot, bool isRefresh)
        //{
        //    if (CurrentSlot == slot)
        //    {
        //        return false;
        //    }

        //    //if (slot.CurrentItem != default)
        //    //{
        //    //    if (CurrentSlot != default)
        //    //    {
        //    //        if (slot.IsAvailable(this) && CurrentSlot.IsAvailable(slot.CurrentItem))
        //    //        {

        //    //        }
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    if (slot.IsAvailable(this))
        //    //    {
        //    //        slot.SetItem(this);
        //    //    }
        //    //}

        //    CurrentSlot = slot;

        //    if (isRefresh)
        //    {
        //        slot.SetItem(this);
        //    }

        //    if (slot == default)
        //    {
        //        if (ViewItem != default)
        //        {
        //            ViewItem.Hide();
        //            ViewItem.DisableIO();
        //        }
        //    }

        //    return true;
        //}
        //public override bool SetViewItem(ViewItem view, bool isRefresh)
        //{
        //    if (ViewItem == view)
        //    {
        //        return false;
        //    }
        
        //    ViewItem = view;

        //    if (ViewItem != default && view == default)
        //    {
        //        ViewItem.Hide();
        //        ViewItem.DisableIO();
        //    }

        //    return true;
        //}
        public override void OnInit()
        {
        
        }
    }

    //public enum TypeBool : byte
    //{
    //    None,
    //    True,
    //    False,
    //}
}
#endif