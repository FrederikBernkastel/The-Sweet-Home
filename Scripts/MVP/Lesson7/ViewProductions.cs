#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RAY_Lesson7
{
    public class ViewProductions : BaseView
    {
        [Header("Ref")]
        [SerializeField][Required] private Transform _ref;
        [SerializeField][Required] private Transform refButton;
        [SerializeField][Required] private TMP_Text refTitle;

        [Header("Prefabs")]
        [SerializeField][Required] private ViewHouse prefabViewHouse;
        [SerializeField][Required] private Button prefabButton;

        private List<ViewHouse> listHouses { get; } = new();
        private List<Button> listButtons { get; } = new();

        public override string Name => "ViewHouses";

        private static int globalID;

        public void AddHouse(HouseData house)
        {
            var _house = GameObject.Instantiate(prefabViewHouse, _ref);

            //_house.SetInfo(house, ++globalID);

            listHouses.Add(_house);
        }
        public void RemoveHouse(int id)
        {
            var house = listHouses.FirstOrDefault(u => u.ID == id);

            if (house)
            {
                listHouses.Remove(house);

                GameObject.Destroy(house);
            }
        }
        public void AddButton(ButtonData data, UnityAction action)
        {
            var button = GameObject.Instantiate(prefabButton, refButton);

            button.GetComponent<Image>().sprite = data.Icon;
            button.name = data.Name;
            button.onClick.AddListener(action);

            listButtons.Add(button);
        }
        public void RemoveButton(string key)
        {
            var button = listButtons.FirstOrDefault(u => u.name == key);

            if (button)
            {
                listButtons.Remove(button);

                GameObject.Destroy(button);
            }
        }
        public void RemoveAll()
        {
            foreach (var s in listButtons)
            {
                listButtons.Remove(s);

                GameObject.Destroy(s);
            }
        }
        private void Update()
        {
            if (BaseMouseUIView.SelectFieldView)
            {
                if (Input.GetMouseButtonUp(1))
                {
                    BaseMouseUIView.SelectFieldView.SetStateIdle(true);

                    BaseMouseUIView.SelectFieldView = default;
                }
            }
        }
        public override void Show(bool flag)
        {
            base.Show(flag);

            if (flag)
            {
                AddButton(GameStorage.Instance.GetButtonDate("ѕлюсћельница"), () => AddHouse(GameStorage.Instance.GetHouseData("ћельница")));
                AddButton(GameStorage.Instance.GetButtonDate("ѕлюсЎахта"), () => AddHouse(GameStorage.Instance.GetHouseData("Ўахта")));
                AddButton(GameStorage.Instance.GetButtonDate("ѕлюс узн€"), () => AddHouse(GameStorage.Instance.GetHouseData(" узн€")));
                AddButton(GameStorage.Instance.GetButtonDate("ѕлюсјкадеми€"), () => AddHouse(GameStorage.Instance.GetHouseData("јкадеми€")));
            }
            else
            {
                RemoveButton("ѕлюсћельница");
                RemoveButton("ѕлюсЎахта");
                RemoveButton("ѕлюс узн€");
                RemoveButton("ѕлюсјкадеми€");
            }
        }
        public void SetTitle(string title)
        {
            refTitle.text = title;
        }
    }
}
#endif