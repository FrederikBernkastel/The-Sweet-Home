#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace RAY_TestTask_1
{
    public class ViewInventory : BaseViewInventory
    {
        public override string Name { get; } = "ViewInventory";

        private List<BaseViewSlot> listSlots { get; set; }
        private List<BaseViewItem> listItems { get; set; }


        //[BoxGroup("Armor")]
        //[SerializeField][Required] private protected StorageArmor stHelmet;
        //[SerializeField][Required] private protected StorageArmor stArmor;
        //[SerializeField][Required] private protected StorageArmor stLegs;
        //[SerializeField][Required] private protected StorageArmor stGloves;
        //[SerializeField][Required] private protected StorageArmor stShoes;
        //[SerializeField][Required] private protected StorageArmor stBelt;
        //[SerializeField] private protected StorageArmor[] listHelmet;
        //[SerializeField] private protected StorageArmor[] listArmor;
        //[SerializeField] private protected StorageArmor[] listLegs;
        //[SerializeField] private protected StorageArmor[] listGloves;
        //[SerializeField] private protected StorageArmor[] listShoes;
        //[SerializeField] private protected StorageArmor[] listBelt;

        [BoxGroup("ViewCharacter")]
        //[SerializeField][Required] private protected ViewCharacterInventory viewCharacter;
        [SerializeField][Required] private protected Transform refSlots;
        [SerializeField][Required] private protected Transform refItems;

        [BoxGroup("General")]
        [SerializeField][Required] private protected GridLayoutGroup grid;

        //[BoxGroup("ViewSlots")]
        //[SerializeField][Required] private protected ViewSlotUI viewSlotHelmet;
        //[SerializeField][Required] private protected ViewSlotUI viewSlotArmor;
        //[SerializeField][Required] private protected ViewSlotUI viewSlotLegs;
        //[SerializeField][Required] private protected ViewSlotUI viewSlotGloves;
        //[SerializeField][Required] private protected ViewSlotUI viewSlotShoes;
        //[SerializeField][Required] private protected ViewSlotUI viewSlotBelt;

        //private protected StorageArmor currentHelmet;
        //private protected StorageArmor currentArmor;
        //private protected StorageArmor currentLegs;
        //private protected StorageArmor currentGloves;
        //private protected StorageArmor currentShoes;
        //private protected StorageArmor currentBelt;

        private protected Dictionary<TypeStatArmor, Action<int>> pairStat = new();

        private Vector3 tempPosition;

        public override void SetCurrentCharacter(BaseFriendlyCharacter character)
        {
            //if (CurrentCharacter == character)
            //{
            //    return false;
            //}

            CurrentCharacter = character;

            //if (isRefresh)
            //{
            //    foreach (var s in character.ListItems)
            //    {
            //        s.SetViewItem(default, false);
            //        s.SetSlot(default, false);
            //    }
            
            //    foreach (var s in character.ListItems)
            //    {
            //        var viewItem = GetFreeViewItem(s.GetBaseItemSO());

            //        if (viewItem != default)
            //        {
            //            s.SetViewItem(viewItem);
            //            viewItem.SetCurrentItem(s);

            //            var slot = GetFreeSlot();

            //            if (slot != default)
            //            {
            //                //Debug.Log(slot.GetComponent<RectTransform>().anchoredPosition);
            //                s.SetSlot(slot);
            //                slot.SetItem(s);
            //            }

            //            viewItem.Hide();
            //            viewItem.DisableIO();
            //        }
            //    }
            //}

            //return true;
        }
        public override BaseViewItem GetFreeViewItem(BaseItemSO baseItemSO)
        {
            return listItems?.FirstOrDefault(
                u => u.CurrentItem == default && 
                u.GetItemSO().GetInstanceID() == baseItemSO.GetInstanceID()) ?? default;
        }
        private void SetListSlots()
        {
            if (listSlots != default)
            {
                return;
            }

            listSlots = new();

            foreach (Transform s in refSlots)
            {
                if (s.TryGetComponent<BaseViewSlot>(out var slot))
                {
                    listSlots.Add(slot);
                }
            }
        }
        private void SetListItems()
        {
            if (listItems != default)
            {
                return;
            }

            listItems = new();

            foreach (Transform s in refItems)
            {
                if (s.TryGetComponent<ViewItem>(out var item))
                {
                    listItems.Add(item);
                }
            }
        }
        private protected override void _OnAwake()
        {
            //grid.CalculateLayoutInputHorizontal();
            //grid.CalculateLayoutInputVertical();
            //grid.SetLayoutHorizontal();
            //grid.SetLayoutVertical();

            SetListSlots();
            SetListItems();

            foreach (var s in listSlots)
            {
                s.OnAwake();

                s.SetCurrentItem(default);
            }
            foreach (var s in listItems)
            {
                s.OnAwake();

                s.SetCurrentItem(default);

                s.Hide();
                s.DisableIO();
            }

            //var charater = MainStorage.Instance.MainCharacter as Character;

            //foreach (var s in charater.ListItems)
            //{
            //    var slot = GetFreeSlot();

            //    if (slot != default)
            //    {
            //        s.SetSlot(slot);
            //    }
            //}

            //pairStat.Add(StatsArmor.Attack, SetAttack);
            //pairStat.Add(StatsArmor.Defense, SetDefense);
            //pairStat.Add(StatsArmor.Speed, SetSpeed);
            //pairStat.Add(StatsArmor.Life, SetLife);

            //foreach (var s in listHelmet)
            //{
            //    s.Renderer.gameObject.SetActive(false);
            //}
            //foreach (var s in listArmor)
            //{
            //    s.Renderer.gameObject.SetActive(false);
            //}
            //foreach (var s in listLegs)
            //{
            //    s.Renderer.gameObject.SetActive(false);
            //}
            //foreach (var s in listGloves)
            //{
            //    s.Renderer.gameObject.SetActive(false);
            //}
            //foreach (var s in listShoes)
            //{
            //    s.Renderer.gameObject.SetActive(false);
            //}
            //foreach (var s in listBelt)
            //{
            //    s.Renderer.gameObject.SetActive(false);
            //}

            //viewCharacter.OffArmor();
            //viewCharacter.OffBelt();
            //viewCharacter.OffHelmet();
            //viewCharacter.OffLegs();
            //viewCharacter.OffShoes();
            //viewCharacter.OffGloves();

            //viewSlotHelmet.ActionDrop = u =>
            //{
            //    if (u.pointerDrag.TryGetComponent<StorageArmor>(out var comp))
            //    {
            //        SetHelmet(comp);
            //    }
            //};
            //viewSlotArmor.ActionDrop = u =>
            //{
            //    if (u.pointerDrag.TryGetComponent<StorageArmor>(out var comp))
            //    {
            //        SetArmor(comp);
            //    }
            //};
            //viewSlotArmor.ActionDrag = u =>
            //{
            //    currentArmor.transform.position += new Vector3(u.delta.x, u.delta.y, currentArmor.transform.position.z);
            //};
            //viewSlotArmor.ActionBeginDrag = u =>
            //{
            //    tempPosition = currentArmor.transform.position;
            //}; 
            //viewSlotArmor.ActionEndDrag = u =>
            //{
            //    tempPosition = currentArmor.transform.position;
            //};
        }
        private protected override void _OnDispose()
        {
            foreach (var s in listSlots)
            {
                s.OnDispose();
            }
            foreach (var s in listItems)
            {
                s.OnDispose();
            }
        }
        private protected override void _EnableIO()
        {
            //foreach (var s in listSlots)
            //{
            //    s.EnableIO();
            //}
            //foreach (var s in listItems)
            //{
            //    s.EnableIO();
            //}
        }
        private protected override void _DisableIO()
        {
            //foreach (var s in listSlots)
            //{
            //    s.DisableIO();
            //}
            //foreach (var s in listItems)
            //{
            //    s.DisableIO();
            //}
        }
        private protected override void _Show()
        {
            //foreach (var s in listSlots)
            //{
            //    s.Show();
            //}
            //foreach (var s in listItems)
            //{
            //    s.Show();
            //}
        }
        private protected override void _Hide()
        {
            //foreach (var s in listSlots)
            //{
            //    s.Hide();
            //}
            //foreach (var s in listItems)
            //{
            //    s.Hide();
            //}
        }

        public override void SortAll()
        {
            throw new NotImplementedException();
        }

        public override void SortEquipment()
        {
            throw new NotImplementedException();
        }

        public override void SortPotion()
        {
            throw new NotImplementedException();
        }

        public override void SortQuest()
        {
            throw new NotImplementedException();
        }

        public override void Sort()
        {
            throw new NotImplementedException();
        }

        public override BaseViewSlot GetFreeSlot()
        {
            return listSlots.FirstOrDefault(u => u.CurrentItem == default);
        }
    }
}
#endif