#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using UnityEngine;

namespace RAY_TestTask_1
{
    public class ViewArmorPut : BaseView
    {
        public override string Name { get; } = "ViewArmorPut";

        [BoxGroup("ViewSlots")]
        [SerializeField][Required] private protected BaseViewSlot viewSlotHelmet;
        [SerializeField][Required] private protected BaseViewSlot viewSlotArmor;
        [SerializeField][Required] private protected BaseViewSlot viewSlotLegs;
        [SerializeField][Required] private protected BaseViewSlot viewSlotGloves;
        [SerializeField][Required] private protected BaseViewSlot viewSlotShoes;
        [SerializeField][Required] private protected BaseViewSlot viewSlotBelt;

        public bool GetSlot(TypeItem typeItem, out BaseViewSlot slot)
        {
            if (typeItem == TypeItem.Helmet)
            {
                slot = viewSlotHelmet;
                return true;
            }
            else if (typeItem == TypeItem.Armor)
            {
                slot = viewSlotArmor;
                return true;
            }
            else if (typeItem == TypeItem.Legs)
            {
                slot = viewSlotLegs;
                return true;
            }
            else if (typeItem == TypeItem.Gloves)
            {
                slot = viewSlotGloves;
                return true;
            }
            else if (typeItem == TypeItem.Shoes)
            {
                slot = viewSlotShoes;
                return true;
            }
            else if (typeItem == TypeItem.Belt)
            {
                slot = viewSlotBelt;
                return true;
            }
            else
            {
                slot = default;
                return false;
            }
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