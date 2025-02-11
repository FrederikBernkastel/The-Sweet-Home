#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RAY_Lesson7
{
    public class ViewHouse : BaseNotifyView
    {
        public override string Name => "ViewHouse " + "#" + ID;

        public int ID { get; private set; }
        public HouseData Data { get; private set; }
        public List<FieldState> ListStates { get; private set; }

        public void SetInfo(HouseField data)
        {
            var image = GameObject.Instantiate(data.Data.PrefabIcon, screen.transform);
            var name = GameObject.Instantiate(data.Data.PrefabText, screen.transform);

            image.sprite = data.Data.Icon;
            name.text = data.Data.Name + " #" + data.ID;

            foreach (var s in data.Data.ListResources)
            {
                var icon = GameObject.Instantiate(data.Data.PrefabIcon, screen.transform);
                var text = GameObject.Instantiate(data.Data.PrefabText, screen.transform);

                icon.sprite = s.Icon;
                text.text = s.Price.ToString();
            }
        }
        private protected override void Awake()
        {
            base.Awake();
            
            SetStateIdle(true);
        }
        private protected override void OnDestroy()
        {
            base.OnDestroy();
            
            if (this == BaseMouseUIView.SelectFieldView)
            {
                BaseMouseUIView.SelectFieldView = default;
            }
        }
        private void CreateHouse()
        {
            if (BaseMouseUIView.SelectFieldView is ViewHouse)
            {
                GameStorage.Instance.GetView<ViewHouses>().AddHouse((BaseMouseUIView.SelectFieldView as ViewHouse).Data);

                BaseMouseUIView.SelectFieldView.SetStateIdle(true);

                BaseMouseUIView.SelectFieldView = default;
            }
        }
        public override void SetStateIdle(bool force)
        {
            base.SetStateIdle(force);

            StopNotify(GameStorage.Instance.GetView<ViewNotify>());
        }
        public override void SetStateEnter(bool force)
        {
            base.SetStateEnter(force);

            StartNotify(GameStorage.Instance.GetView<ViewNotify>());
        }
        public override void SetStateSelect()
        {
            base.SetStateSelect();

            GameStorage.Instance.GetView<ViewHouses>().AddButton(GameStorage.Instance.GetButtonDate("ПостроитьЗдание"), CreateHouse);
        }
    }
}
#endif