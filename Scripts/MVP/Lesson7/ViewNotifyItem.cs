#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using RAY_Lesson7;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RAY_Lesson7
{
    public class ViewNotifyItem : BaseView
    {
        [Header("Ref")]
        [SerializeField][Required] private Transform refStats;

        [Header("General")]
        [SerializeField][Required] private Image icon;
        [SerializeField][Required] private TMP_Text title;
        [SerializeField][Required] private TMP_Text description;

        [Header("Prefabs")]
        [SerializeField][Required] private TMP_Text prefabStats;

        public override string Name => "ViewNotifyItem";

        public void SetDescriptionHouse(ViewHouse house)
        {
            icon.sprite = house.Data.Icon;
            title.text = house.Data.Name;
            description.text = house.Data.Description;

            foreach (Transform s in refStats)
            {
                GameObject.Destroy(s);
            }
            
            foreach (var s in house.Data.ListStats)
            {
                var stats = GameObject.Instantiate(prefabStats, refStats);

                stats.text = s.Key + ": " + s.Value;
            }
        }
    }
}
#endif