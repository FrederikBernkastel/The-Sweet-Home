#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using RAY_Cossacks;
using RAY_Lesson7;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace RAY_Lesson7
{
    public class GameStorage : BaseStorage<GameStorage>
    {
        [Header("Prefabs")]
        [SerializeField][Required] public ViewHouse prefabViewHouse;

        [Header("Houses")]
        [SerializeField] private HouseData[] ListHouses;

        [Header("Buttons")]
        [SerializeField] private ButtonData[] ListButtons;

        private Dictionary<string, HouseData> DicHouses { get; } = new();
        private Dictionary<string, ButtonData> DicButtons { get; } = new();
        private List<BaseView> ListViews { get; } = new();
        private Dictionary<Type, BaseModelEssence> DicModelsEssence { get; } = new();

        public void AddModelEssence<T>(T essence) where T : BaseModelEssence
        {
            DicModelsEssence.Add(essence.GetType(), essence);
        }
        public T GetModelEssence<T>() where T : BaseModelEssence
        {
            return (DicModelsEssence.GetValueOrDefault(typeof(T)) ?? throw new Exception()) as T;
        }
        public T GetView<T>() where T : BaseView
        {
            return ListViews.FirstOrDefault(u => u is T) as T ?? throw new Exception();
        }
        public HouseData GetHouseData(string key)
        {
            return DicHouses.GetValueOrDefault(key) ?? throw new Exception();
        }
        public ButtonData GetButtonDate(string key)
        {
            return DicButtons.GetValueOrDefault(key) ?? throw new Exception();
        }
        public IEnumerable<BaseView> GetListViews()
        {
            return ListViews;
        }
        private protected override void _OnInit()
        {
            foreach (var s in ListHouses)
            {
                DicHouses.Add(s.Name, s);
            }
            foreach (var s in ListButtons)
            {
                DicButtons.Add(s.Name, s);
            }
            foreach (var s in GameObject.FindObjectsOfType<BaseView>())
            {
                ListViews.Add(s);
            }

            StateMachine = new(
                new("ContextMessageBox", new MessageBoxState()
                {
                    Message = "HELLO",
                    ExitAction = () => GameStorage.Instance.StateMachine.SetState("ContextGame"),
                    ViewMessageBox = RAY_Lesson7.MainStorage.Instance.PresenterMessageBox,
                }),
                new("ContextEndGame", new MessageBoxState()
                {
                    Message = "Вы проиграли!!!",
                    ExitAction = () => RAY_Lesson7.MainStorage.Instance.StateMachine.SetState("LoadingFromGameToGameScene"),
                    ViewMessageBox = RAY_Lesson7.MainStorage.Instance.PresenterMessageBox,
                }),
                new("ContextGame", new ContextGameState()));

            StateMachine.OnInit();
        }
        private protected override void _OnDispose()
        {

        }
    }
}
#endif