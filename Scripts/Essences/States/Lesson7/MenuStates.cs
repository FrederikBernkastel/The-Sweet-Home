#if ENABLE_ERRORS

using RAY_Core;
using RAY_Cossacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RAY_Lesson7
{
    public class MenuState : BaseState
    {
        public override string NameState => "Menu";

        public UnityAction ExitAction { get; set; }
        public UnityAction GameAction { get; set; }
        private ViewMenu ViewMenu;

        private protected override void Init()
        {
            ViewMenu = GameObject.Instantiate(MenuStorage.Instance.PrefabViewMenu, GameObject.FindGameObjectWithTag(MenuStorage.Instance.RefStorageView).transform)
                .GetComponentInChildren<ViewMenu>();

            ViewMenu.Show(false);
        }
        private protected override void Enter()
        {
            ViewMenu.Show(true);

            ViewMenu.SetClickButtonGame(GameAction);
            ViewMenu.SetClickButtonExit(ExitAction);
        }
        private protected override void Exit()
        {
            ViewMenu.Show(false);
        }
    }
}
#endif