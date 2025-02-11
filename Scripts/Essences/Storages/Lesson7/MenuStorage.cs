#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Lesson7
{
    public class MenuStorage : BaseStorage<MenuStorage>
    {
        [Header("RefStorageView")]
        [SerializeField][Tag] public string RefStorageView;

        [Header("Prefabs")]
        [SerializeField][Required] public GameObject PrefabViewMenu;

        private protected override void _OnInit()
        {
            MainStateMachine = new(
                new KeyValuePair<string, BaseState>("ContextMenu", new MenuState() 
                {
                    GameAction = () => 
                    {
                        MainStorage.Instance.MainStateMachine.SetState("LoadingFromMenuToGameScene");
                    },
                }));

            MainStateMachine.OnInit();
        }
        private protected override void _OnDispose()
        {

        }
    }
}
#endif