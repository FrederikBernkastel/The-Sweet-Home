#if ENABLE_ERRORS

using RAY_Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace RAY_TestTask_1
{
    public class Character : BaseFriendlyCharacter
    {
        public CharacterSO CharacterSO { get; private protected set; }

        public Character(CharacterSO characterSO)
        {
            CharacterSO = characterSO;
        }

        public override void SetViewName(BaseViewName view)
        {
            ViewName = view;
        }
        public override void SetViewLevel(BaseViewLevel view)
        {
            ViewLevel = view;
        }
        public override void SetViewStats(BaseViewStats view)
        {
            ViewStats = view;
        }
        public override void SetViewInventory(BaseViewInventory view)
        {
            ViewInventory = view;
        }
        public override void SetViewCharacterInventory(BaseViewCharacterInventory character)
        {
            ViewCharacterInventory = character;
        }
        public override CharacterSO GetCharacterSO() => CharacterSO;
        public override void SetName(string name)
        {
            Name = name;

            if (ViewName != null)
            {
                ViewName.SetName(Name);
            }
        }
        public override void SetLevel(int level)
        {
            Level = level;

            if (ViewLevel != null)
            {
                ViewLevel.SetLevel(Level);
            }
        }
        public override void SetStat(TypeStatArmor statType, int statValue)
        {
            if (PairStats.ContainsKey(statType))
            {
                PairStats[statType] = statValue;
            }

            if (ViewStats != null)
            {
                ViewStats.SetAttack(PairStats.TryGetValue(TypeStatArmor.Attack, out var attack) ? attack : default);
                ViewStats.SetDefense(PairStats.TryGetValue(TypeStatArmor.Defense, out var defense) ? defense : default);
                ViewStats.SetLife(PairStats.TryGetValue(TypeStatArmor.Life, out var life) ? life : default);
                ViewStats.SetSpeed(PairStats.TryGetValue(TypeStatArmor.Speed, out var speed) ? speed : default);
            }
        }
        public override void PutOffArmor(TypeItem typeItem)
        {
            if (PairPutOnItems.ContainsKey(typeItem))
            {
                if (PairPutOnItems[typeItem] != default)
                {
                    //foreach (var s in PairPutOnItems[typeItem].ArmorSO.listStats)
                    //{
                    //    if (PairStats.ContainsKey(s.statEnum))
                    //    {
                    //        SetStat(s.statEnum, PairStats[s.statEnum] - s.statNumber);
                    //    }
                    //}

                    PairPutOnItems[typeItem].SetPuted(false);
                    PairPutOnItems[typeItem] = default;

                    if (ViewCharacterInventory != null)
                    {
                        if (typeItem == TypeItem.Belt)
                        {
                            ViewCharacterInventory.OffBelt();
                        }
                        else if (typeItem == TypeItem.Armor)
                        {
                            ViewCharacterInventory.OffArmor();
                        }
                        else if (typeItem == TypeItem.Legs)
                        {
                            ViewCharacterInventory.OffLegs();
                        }
                        else if (typeItem == TypeItem.Helmet)
                        {
                            ViewCharacterInventory.OffHelmet();
                        }
                        else if (typeItem == TypeItem.Gloves)
                        {
                            ViewCharacterInventory.OffGloves();
                        }
                        else if (typeItem == TypeItem.Shoes)
                        {
                            ViewCharacterInventory.OffShoes();
                        }
                    }
                }
            }
        }
        public override void PutOnArmor(BaseItem armorItem)
        {
            if (armorItem != default)
            {
                //if (PairPutOnItems.ContainsKey(armorItem.ArmorSO.TypeItem))
                //{
                //    if (PairPutOnItems[armorItem.ArmorSO.TypeItem] != armorItem)
                //    {
                //        foreach (var s in armorItem.ArmorSO.listStats)
                //        {
                //            if (PairStats.ContainsKey(s.statEnum))
                //            {
                //                SetStat(s.statEnum, PairStats[s.statEnum] + s.statNumber);
                //            }
                //        }

                //        PairPutOnItems[armorItem.ArmorSO.TypeItem] = armorItem;
                //        PairPutOnItems[armorItem.ArmorSO.TypeItem].SetPuted(true);

                //        if (ViewCharacterInventory != null)
                //        {
                //            if (armorItem.ArmorSO.TypeItem == TypeItem.Belt)
                //            {
                //                ViewCharacterInventory.OnBelt();
                //            }
                //            else if (armorItem.ArmorSO.TypeItem == TypeItem.Armor)
                //            {
                //                ViewCharacterInventory.OnArmor();
                //            }
                //            else if (armorItem.ArmorSO.TypeItem == TypeItem.Legs)
                //            {
                //                ViewCharacterInventory.OnLegs();
                //            }
                //            else if (armorItem.ArmorSO.TypeItem == TypeItem.Helmet)
                //            {
                //                ViewCharacterInventory.OnHelmet();
                //            }
                //            else if (armorItem.ArmorSO.TypeItem == TypeItem.Gloves)
                //            {
                //                ViewCharacterInventory.OnGloves();
                //            }
                //            else if (armorItem.ArmorSO.TypeItem == TypeItem.Shoes)
                //            {
                //                ViewCharacterInventory.OnShoes();
                //            }
                //        }
                //    }
                //}
            }
        }
        public override void AddItemToInventory(BaseItem baseItem)
        {
            ListItems.Add(baseItem);

            if (ViewInventory != null)
            {
                //
                //
                //
            }
        }
        public override void SubItemFromInventory(BaseItem baseItem)
        {
            ListItems.Remove(baseItem);

            if (ViewInventory != null)
            {
                //
                //
                //
            }
        }
        public override void OnInit()
        {
            PairPutOnItems = new()
            {
                [TypeItem.Helmet] = default,
                [TypeItem.Armor] = default,
                [TypeItem.Legs] = default,
                [TypeItem.Gloves] = default,
                [TypeItem.Shoes] = default,
                [TypeItem.Belt] = default,
            };

            PairStats = new()
            {
                [TypeStatArmor.Attack] = default,
                [TypeStatArmor.Defense] = default,
                [TypeStatArmor.Speed] = default,
                [TypeStatArmor.Life] = default,
            };

            ListItems = new();
        }
    }
}
#endif