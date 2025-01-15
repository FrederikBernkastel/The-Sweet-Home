#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RAY_TestTask_1
{
    public class MainStorage : BaseStorage<MainStorage>, IMainStorageApp
    {
        public override string Name { get; } = "MainStorage";

        [BoxGroup("MainCharacter")]
        [SerializeField][Required] private protected CharacterSO characterSO;

        [BoxGroup("MainEnvironment")]
        [SerializeField][Required] private protected EnvironmentSO environmentSO;

        public BaseFriendlyCharacter MainCharacter { get; private protected set; } = default;

        public Dictionary<string, BaseItemSO> PairItems { get; private protected set; } = default;

        private protected override void _OnAwake()
        {
            SetPairItems();
            SetMainCharacter();
        }
        private protected override StateMachine CreateStateMachine()
        {
            StateMachine stateMachine = new("MainStateMachine",
                new KeyValuePair<string, BaseState>("GameState", new MainState()));

            return stateMachine;
        }
        private void SetPairItems()
        {
            if (PairItems != default)
            {
                return;
            }

            PairItems = new();

            foreach (var s in environmentSO.ListItemsSO)
            {
                PairItems.Add(s.NameObject, s);
            }
        }
        private void SetMainCharacter()
        {
            if (MainCharacter != default)
            {
                return;
            }

            MainCharacter = new Character(characterSO);

            MainCharacter.OnInit();

            MainCharacter.SetName(characterSO.VisibleName);
            MainCharacter.SetLevel(characterSO.LevelCharacter);

            foreach (var s in characterSO.ListItemsSO)
            {
                var item = s.CreateItem();

                item.OnInit();

                item.SetPuted(true);
                item.SetSlot(default);
                item.SetViewItem(default);
                item.SetCurrentCharacter(MainCharacter);

                MainCharacter.AddItemToInventory(item);
            }


            //List<BaseItem> baseItems = new List<BaseItem>();

            //foreach (var s in characterSO.ListItemsSO)
            //{
            //    if (SystemSO.PairItems.TryGetValue(s.NameObject, out var val))
            //    {
            //        baseItems.Add(val.CreateItem());
            //    }
            //}

            //foreach (var s in characterSO.ListItemsPutOnSO)
            //{
            //    if (characterSO.ListItemsSO.Any(u => u.GetInstanceID() == s.ItemSO.GetInstanceID()))
            //    {
            //        baseItems.First(u => u.GetBaseItemSO().GetInstanceID() == s.ItemSO.GetInstanceID()).IsPuted = true;
            //    }
            //}

            //MainCharacter = new Character(new()
            //{
            //    CharacterSO = characterSO,
            //    Name = characterSO.VisibleName,
            //    Level = characterSO.LevelCharacter,
            //    ListItems = baseItems,
            //});

            //MainCharacter.OnInit();
        }
    }
    public class SystemSO
    {
        public Dictionary<string, BaseItemSO> PairItems { get; private protected set; }

        public bool SetPairItems(BaseItemSO[] baseItemSOs)
        {
            if (PairItems != default)
            {
                return false;
            }

            PairItems = new();

            foreach (var s in baseItemSOs)
            {
                PairItems.Add(s.NameObject, s);
            }

            return true;
        }
    }
}
#endif