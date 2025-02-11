#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Lesson7;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RAY_Core
{
    public class ViewCommands : BaseView
    {
        [Header("Ref")]
        [SerializeField][Required] private Transform refButton;
        [SerializeField][Required] private TMP_Text refTitle;

        [Header("Prefabs")]
        [SerializeField][Required] private Button prefabButton;

        private List<Button> listButtons { get; } = new();

        public override string Name => "ViewCommands";

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
        public void SetTitle(string title)
        {
            refTitle.text = title;
        }
    }
}
#endif