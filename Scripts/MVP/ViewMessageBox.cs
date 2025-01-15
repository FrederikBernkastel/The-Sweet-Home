#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RAY_Core
{
    public class ViewMessageBox : BaseView
    {
        [Header("MessageBox")]
        [SerializeField][NaughtyAttributes.Tag] private string tagText = "MessageText";

        [SerializeField][Range(0, 1)] private float width = 0.5f;
        [SerializeField][Range(0, 1)] private float height = 0.5f;

        [SerializeField][Required] private Canvas canvas;

        public override string Name => "ViewMessageBox";

        public void SetMessage(string str)
        {
            screen.GetComponentsInChildren<TMP_Text>().FirstOrDefault(u => u.CompareTag(tagText))?.SetText(str.Replace("\\n", "\n"));
        }
        public void SetClickButton(UnityAction unityAction)
        {
            var temp = screen.GetComponentInChildren<Button>();
            temp.onClick.RemoveAllListeners();
            temp.onClick.AddListener(unityAction);
        }
        public void SetDefaultSize()
        {
            if (canvas.TryGetComponent<CanvasScaler>(out var scaler))
            {
                if (scaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
                {
                    var size = scaler.referenceResolution;

                    screen.GetComponent<RectTransform>().sizeDelta = new(size.x * width, size.y * height);
                }
            }
        }
    }
}
#endif