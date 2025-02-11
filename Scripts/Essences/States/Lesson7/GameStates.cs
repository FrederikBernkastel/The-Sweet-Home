#if ENABLE_ERRORS

using MapMagic.Core;
using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RAY_Lesson7
{
    public class ContextGameState : BaseOrderState
    {
        public override string NameState => "Game";

        private protected override KeyValuePair<StateMachine, string>[] GetListStateMachine()
        {
            //StateMachine cameraStateMachine = new(
            //    new KeyValuePair<string, BaseState>("ContextLife", new CameraLifeState()));

            //KeyValuePair<StateMachine, string>[] list = new KeyValuePair<StateMachine, string>[]
            //{
            //    new(cameraStateMachine, "ContextLife"),
            //};

            return new KeyValuePair<StateMachine, string>[0];
        }
        private protected override void Init()
        {
            GameStorage.Instance.GetView<ViewGame>().ActivatedUI(false);

            base.Init();
        }
        private protected override void Enter()
        {
            foreach (var s in GameStorage.Instance.GetListViews())
            {
                s.Show(true);
            }

            GameStorage.Instance.GetView<ViewGame>().ActivatedUI(true);

            base.Enter();
        }
        private protected override void Exit()
        {
            foreach (var s in GameStorage.Instance.GetListViews())
            {
                s.Show(false);
            }

            GameStorage.Instance.GetView<ViewGame>().ActivatedUI(false);

            base.Exit();
        }
    }
    public class ViewHousesState : BaseState
    {
        public override string NameState => "ViewHousesState";

        private ViewHouses view;
        private ModelEssenceHouses model;

        private protected override void Enter()
        {
            model.Enable();
        }
        private protected override void Exit()
        {
            model.Disable();
        }
        private protected override void Init()
        {
            view = GameStorage.Instance.GetView<ViewHouses>();

            GameStorage.Instance.AddModelEssence(model = new() { View = view });

            model.Init();
        }
        private protected override void Dispose()
        {
            model.Dispose();
        }
    }
    public abstract class BaseModelEssence
    {

    }
    public class ModelEssenceHouses : BaseModelEssence
    {
        public ViewHouses View { get; set; }
        private List<HouseField> listHouses { get; } = new();
        private Transform folder;

        private static int globalID;

        public void Enable()
        {
            View.AddButton(GameStorage.Instance.GetButtonDate("ѕлюсћельница"), () => View.AddHouse(GameStorage.Instance.GetHouseData("ћельница")));
            View.AddButton(GameStorage.Instance.GetButtonDate("ѕлюсЎахта"), () => View.AddHouse(GameStorage.Instance.GetHouseData("Ўахта")));
            View.AddButton(GameStorage.Instance.GetButtonDate("ѕлюс узн€"), () => View.AddHouse(GameStorage.Instance.GetHouseData(" узн€")));
            View.AddButton(GameStorage.Instance.GetButtonDate("ѕлюсјкадеми€"), () => View.AddHouse(GameStorage.Instance.GetHouseData("јкадеми€")));
        }
        public void Disable()
        {
            View.RemoveAll();
        }
        public void AddHouse(HouseData house)
        {
            var obj = GameObject.Instantiate(GameStorage.Instance.prefabViewHouse, folder);
            
            HouseField field = new()
            {
                ID = ++globalID,
                Data = house,
                View = obj,
            };

            listHouses.Add(field);

            obj.SetInfo(field);
        }
        public void RemoveHouse(int id)
        {
            var house = listHouses.FirstOrDefault(u => u.ID == id);

            if (house != null)
            {
                listHouses.Remove(house);

                GameObject.Destroy(house.View.gameObject);
            }
        }
        public void Init()
        {
            folder = View.CreateNewFolder("FolderHouses");
        }
        public void Dispose()
        {
            View.RemoveFolder(folder);
        }
    }
    public class HouseField
    {
        public int ID { get; set; }
        public HouseData Data { get; set; }
        public ViewHouse View { get; set; }
        public List<FieldState> ListStates { get; } = new();
    }
}
#endif