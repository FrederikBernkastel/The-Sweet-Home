#if ENABLE_ERRORS

using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseCharacter
    {
        private static int staticInstanceID;

        public int InstanceID { get; private protected set; } = staticInstanceID++;

        public abstract void OnInit();
    }
    public abstract class BaseFriendlyCharacter : BaseCharacter
    {
        public string Name { get; private protected set; } = default;
        public int Level { get; private protected set; } = default;
        public List<BaseItem> ListItems { get; private protected set; } = default;
        public Dictionary<TypeStatArmor, int> PairStats { get; private protected set; } = default;
        public Dictionary<TypeItem, BaseItem> PairPutOnItems { get; private protected set; } = default;

        public BaseViewName ViewName { get; private protected set; } = default;
        public BaseViewLevel ViewLevel { get; private protected set; } = default;
        public BaseViewStats ViewStats { get; private protected set; } = default;
        //public ViewArmorPut ViewArmor { get; private set; }
        public BaseViewInventory ViewInventory { get; private protected set; } = default;
        public BaseViewCharacterInventory ViewCharacterInventory { get; private protected set; } = default;

        public abstract CharacterSO GetCharacterSO();

        public abstract void SetViewName(BaseViewName view);
        public abstract void SetViewLevel(BaseViewLevel view);
        public abstract void SetViewStats(BaseViewStats view);
        //public abstract void SetViewArmor(ViewArmorPut view);
        public abstract void SetViewInventory(BaseViewInventory view);
        public abstract void SetViewCharacterInventory(BaseViewCharacterInventory character);

        public abstract void SetName(string name);
        public abstract void SetLevel(int level);
        public abstract void SetStat(TypeStatArmor statType, int statValue);
        public abstract void AddItemToInventory(BaseItem baseItem);
        public abstract void SubItemFromInventory(BaseItem baseItem);
        public abstract void PutOnArmor(BaseItem armorItem);
        public abstract void PutOffArmor(TypeItem typeItem);
    }
}
#endif