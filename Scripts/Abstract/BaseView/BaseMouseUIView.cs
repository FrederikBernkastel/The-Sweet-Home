#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RAY_Core
{
    public abstract class BaseMouseUIView : BaseView, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Colors")]
        [SerializeField] private protected Color ColorEnter;
        [SerializeField] private protected Color ColorIdle;
        [SerializeField] private protected Color ColorSelect;

        [Header("General")]
        [SerializeField][Required] private protected Image Background;

        public static BaseMouseUIView SelectFieldView { get; set; }

        public virtual void SetStateIdle(bool force)
        {
            if (force)
            {
                Background.color = ColorIdle;
            }
            else if (this != SelectFieldView)
            {
                Background.color = ColorIdle;
            }
        }
        public virtual void SetStateEnter(bool force)
        {
            if (force)
            {
                Background.color = ColorIdle;
            }
            else if (this != SelectFieldView)
            {
                Background.color = ColorEnter;
            }
        }
        public virtual void SetStateSelect()
        {
            Background.color = ColorSelect;

            SelectFieldView = this;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetStateEnter(false);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            SetStateIdle(false);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            SetStateSelect();
        }
    }
}
#endif