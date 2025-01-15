#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using RAY_CuteHome;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

namespace RAY_TestTask_1
{
    public abstract class BaseViewSharedInventory : BaseView
    {
        public BaseFriendlyCharacter CurrentCharacter { get; private protected set; } = default;

        public abstract void SetCurrentCharacter(BaseFriendlyCharacter character);
    }
    public class ViewSharedInventory : BaseViewSharedInventory
    {
        public override string Name { get; } = "ViewSharedInventory";

        [BoxGroup("Views")]
        [SerializeField][Required] private protected BaseViewInventory viewInventory;
        [SerializeField][Required] private protected BaseViewName viewName;
        [SerializeField][Required] private protected BaseViewLevel viewLevel;
        [SerializeField][Required] private protected BaseViewStats viewStats;
        [SerializeField][Required] private protected BaseViewCharacterInventory viewCharacterInventory;

        [BoxGroup("SlotsArmor")]
        [SerializeField][Required] private protected BaseViewSlot viewSlotHelmet;
        [SerializeField][Required] private protected BaseViewSlot viewSlotArmor;
        [SerializeField][Required] private protected BaseViewSlot viewSlotLegs;
        [SerializeField][Required] private protected BaseViewSlot viewSlotGloves;
        [SerializeField][Required] private protected BaseViewSlot viewSlotShoes;
        [SerializeField][Required] private protected BaseViewSlot viewSlotBelt;

        [BoxGroup("General")]
        [SerializeField][Required] private protected Canvas canvas;

        public override void SetCurrentCharacter(BaseFriendlyCharacter character)
        {
            CurrentCharacter = character;
        }

        private protected override void _DisableIO()
        {
            viewInventory.DisableIO();
            viewName.DisableIO();
            viewLevel.DisableIO();
            viewStats.DisableIO();
            viewCharacterInventory.DisableIO();

            viewSlotHelmet.DisableIO();
            viewSlotArmor.DisableIO();
            viewSlotLegs.DisableIO();
            viewSlotGloves.DisableIO();
            viewSlotShoes.DisableIO();
            viewSlotBelt.DisableIO();
        }
        private protected override void _EnableIO()
        {
            viewInventory.EnableIO();
            viewName.EnableIO();
            viewLevel.EnableIO();
            viewStats.EnableIO();
            viewCharacterInventory.EnableIO();

            viewSlotHelmet.EnableIO();
            viewSlotArmor.EnableIO();
            viewSlotLegs.EnableIO();
            viewSlotGloves.EnableIO();
            viewSlotShoes.EnableIO();
            viewSlotBelt.EnableIO();
        }
        private protected override void _OnAwake()
        {
            viewInventory.OnAwake();
            viewName.OnAwake();
            viewLevel.OnAwake();
            viewStats.OnAwake();
            viewCharacterInventory.OnAwake();

            viewSlotHelmet.OnAwake();
            viewSlotArmor.OnAwake();
            viewSlotLegs.OnAwake();
            viewSlotGloves.OnAwake();
            viewSlotShoes.OnAwake();
            viewSlotBelt.OnAwake();

            viewSlotHelmet.SetCurrentItem(default);
            viewSlotArmor.SetCurrentItem(default);
            viewSlotLegs.SetCurrentItem(default);
            viewSlotGloves.SetCurrentItem(default);
            viewSlotShoes.SetCurrentItem(default);
            viewSlotBelt.SetCurrentItem(default);

            foreach (var s in canvas.GetComponentsInChildren<LayoutGroup>())
            {
                s.CalculateLayoutInputHorizontal();
                s.CalculateLayoutInputVertical();
                s.SetLayoutHorizontal();
                s.SetLayoutVertical();
            }

            foreach (var s in MainStorage.Instance.MainCharacter.ListItems)
            {
                var viewItem = viewInventory.GetFreeViewItem(s.GetItemSO());

                if (viewItem != default)
                {
                    s.SetViewItem(viewItem);
                    viewItem.SetCurrentItem(s);

                    if (s.IsPuted)
                    {
                        if (s.GetItemSO().TypeItem == TypeItem.Armor)
                        {
                            s.SetSlot(viewSlotArmor);
                            viewSlotArmor.SetCurrentItem(s);

                            viewSlotArmor.AttachItem();
                        }
                        else if (s.GetItemSO().TypeItem == TypeItem.Belt)
                        {
                            s.SetSlot(viewSlotBelt);
                            viewSlotBelt.SetCurrentItem(s);

                            viewSlotBelt.AttachItem();
                        }
                        else if (s.GetItemSO().TypeItem == TypeItem.Helmet)
                        {
                            s.SetSlot(viewSlotHelmet);
                            viewSlotHelmet.SetCurrentItem(s);

                            viewSlotHelmet.AttachItem();
                        }
                        else if (s.GetItemSO().TypeItem == TypeItem.Legs)
                        {
                            s.SetSlot(viewSlotLegs);
                            viewSlotLegs.SetCurrentItem(s);

                            viewSlotLegs.AttachItem();
                        }
                        else if (s.GetItemSO().TypeItem == TypeItem.Gloves)
                        {
                            s.SetSlot(viewSlotGloves);
                            viewSlotGloves.SetCurrentItem(s);

                            viewSlotGloves.AttachItem();
                        }
                        else if (s.GetItemSO().TypeItem == TypeItem.Shoes)
                        {
                            s.SetSlot(viewSlotShoes);
                            viewSlotShoes.SetCurrentItem(s);

                            viewSlotShoes.AttachItem();
                        }
                        else
                        {
                            s.SetPuted(false);
                        }
                    }

                    if (!s.IsPuted)
                    {
                        var slot = viewInventory.GetFreeSlot();

                        if (slot != default)
                        {
                            s.SetSlot(slot);
                            slot.SetCurrentItem(s);

                            slot.AttachItem();
                        }
                    }
                }
            }

            viewCharacterInventory.Hide();
            viewCharacterInventory.DisableIO();
        }
        private protected override void _OnDispose()
        {
            viewInventory.OnDispose();
            viewName.OnDispose();
            viewLevel.OnDispose();
            viewStats.OnDispose();
            viewCharacterInventory.OnDispose();

            viewSlotHelmet.OnDispose();
            viewSlotArmor.OnDispose();
            viewSlotLegs.OnDispose();
            viewSlotGloves.OnDispose();
            viewSlotShoes.OnDispose();
            viewSlotBelt.OnDispose();
        }
        private protected override void _Show()
        {
            canvas.gameObject.SetActive(true);

            MainStorage.Instance.MainCharacter.SetViewName(viewName);
            MainStorage.Instance.MainCharacter.SetViewLevel(viewLevel);
            MainStorage.Instance.MainCharacter.SetViewStats(viewStats);
            MainStorage.Instance.MainCharacter.SetViewInventory(viewInventory);
            MainStorage.Instance.MainCharacter.SetViewCharacterInventory(viewCharacterInventory);

            viewName.SetCurrentCharacter(MainStorage.Instance.MainCharacter);
            viewLevel.SetCurrentCharacter(MainStorage.Instance.MainCharacter);
            viewStats.SetCurrentCharacter(MainStorage.Instance.MainCharacter);
            viewInventory.SetCurrentCharacter(MainStorage.Instance.MainCharacter);
            viewCharacterInventory.SetCurrentCharacter(MainStorage.Instance.MainCharacter);

            foreach (var s in MainStorage.Instance.MainCharacter.ListItems)
            {
                if (s.ViewItem != default)
                {
                    s.ViewItem.Show();
                    s.ViewItem.EnableIO();
                }
            }

            viewCharacterInventory.Show();
            viewCharacterInventory.EnableIO();

            viewName.SetName(MainStorage.Instance.MainCharacter.Name);
            viewLevel.SetLevel(MainStorage.Instance.MainCharacter.Level);

            viewStats.SetAttack(MainStorage.Instance.MainCharacter.PairStats.TryGetValue(TypeStatArmor.Attack, out var attack) ? attack : default);
            viewStats.SetDefense(MainStorage.Instance.MainCharacter.PairStats.TryGetValue(TypeStatArmor.Defense, out var defense) ? defense : default);
            viewStats.SetLife(MainStorage.Instance.MainCharacter.PairStats.TryGetValue(TypeStatArmor.Life, out var life) ? life : default);
            viewStats.SetSpeed(MainStorage.Instance.MainCharacter.PairStats.TryGetValue(TypeStatArmor.Speed, out var speed) ? speed : default);

            if (MainStorage.Instance.MainCharacter.PairPutOnItems.TryGetValue(TypeItem.Helmet, out var helmet))
            {
                if (helmet == default)
                {
                    viewCharacterInventory.OffHelmet();
                }
                else
                {
                    viewCharacterInventory.OnHelmet();
                }
            }
            else
            {
                viewCharacterInventory.OffHelmet();
            }
            if (MainStorage.Instance.MainCharacter.PairPutOnItems.TryGetValue(TypeItem.Armor, out var armor))
            {
                if (armor == default)
                {
                    viewCharacterInventory.OffArmor();
                }
                else
                {
                    viewCharacterInventory.OnArmor();
                }
            }
            else
            {
                viewCharacterInventory.OffArmor();
            }
            if (MainStorage.Instance.MainCharacter.PairPutOnItems.TryGetValue(TypeItem.Legs, out var legs))
            {
                if (legs == default)
                {
                    viewCharacterInventory.OffLegs();
                }
                else
                {
                    viewCharacterInventory.OnLegs();
                }
            }
            else
            {
                viewCharacterInventory.OffLegs();
            }
            if (MainStorage.Instance.MainCharacter.PairPutOnItems.TryGetValue(TypeItem.Shoes, out var shoes))
            {
                if (shoes == default)
                {
                    viewCharacterInventory.OffShoes();
                }
                else
                {
                    viewCharacterInventory.OnShoes();
                }
            }
            else
            {
                viewCharacterInventory.OffShoes();
            }
            if (MainStorage.Instance.MainCharacter.PairPutOnItems.TryGetValue(TypeItem.Gloves, out var gloves))
            {
                if (gloves == default)
                {
                    viewCharacterInventory.OffGloves();
                }
                else
                {
                    viewCharacterInventory.OnGloves();
                }
            }
            else
            {
                viewCharacterInventory.OffGloves();
            }
            if (MainStorage.Instance.MainCharacter.PairPutOnItems.TryGetValue(TypeItem.Belt, out var belt))
            {
                if (belt == default)
                {
                    viewCharacterInventory.OffBelt();
                }
                else
                {
                    viewCharacterInventory.OnBelt();
                }
            }
            else
            {
                viewCharacterInventory.OffBelt();
            }
        }
        private protected override void _Hide()
        {
            canvas.gameObject.SetActive(false);

            MainStorage.Instance.MainCharacter.SetViewName(default);
            MainStorage.Instance.MainCharacter.SetViewLevel(default);
            MainStorage.Instance.MainCharacter.SetViewStats(default);
            MainStorage.Instance.MainCharacter.SetViewInventory(default);
            MainStorage.Instance.MainCharacter.SetViewCharacterInventory(default);

            viewName.SetCurrentCharacter(default);
            viewLevel.SetCurrentCharacter(default);
            viewStats.SetCurrentCharacter(default);
            viewInventory.SetCurrentCharacter(default);
            viewCharacterInventory.SetCurrentCharacter(default);

            foreach (var s in MainStorage.Instance.MainCharacter.ListItems)
            {
                if (s.ViewItem != default)
                {
                    s.ViewItem.Hide();
                    s.ViewItem.DisableIO();
                }
            }

            viewCharacterInventory.Hide();
            viewCharacterInventory.DisableIO();
        }
    }
}
#endif