#if ENABLE_ERRORS

using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace RAY_Core
{
    public class ViewResources : BaseView
    {
        [Header("ListResources")]
        [SerializeField] private FieldDefaultValueResource[] listValues;

        public override string Name => "ViewResources";

        private protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < listValues.Length; i++)
            {
                SetValue(listValues[i], listValues[i].defaultValue);
            }
        }
        public void SetValue(Resource resource, int val)
        {
            var res = listValues.FirstOrDefault(u => u.name == resource);

            if (res != null)
            {
                SetValue(res, val);
            }
        }
        public void AddValue(Resource resource, int val)
        {
            var res = listValues.FirstOrDefault(u => u.name == resource);

            if (res != null)
            {
                AddValue(res, val);
            }
        }
        public void SubValue(Resource resource, int val)
        {
            var res = listValues.FirstOrDefault(u => u.name == resource);

            if (res != null)
            {
                SubValue(res, val);
            }
        }
        private void SetValue(FieldDefaultValueResource resource, int val)
        {
            val = val < 0 ? 0 : val > 99999 ? 99999 : val;

            resource.CurrentValue = val;
        }
        private void AddValue(FieldDefaultValueResource resource, int val)
        {
            SetValue(resource, resource.CurrentValue + val);
        }
        private void SubValue(FieldDefaultValueResource resource, int val)
        {
            SetValue(resource, resource.CurrentValue - val);
        }

        public enum Resource
        {
            Wheat,
            Coal,
            Gold,
        }

        [Serializable]
        public class FieldDefaultValueResource
        {
            [SerializeField] public Resource name;
            [SerializeField][Min(0)] public int defaultValue;
            [SerializeField][Required] public TMP_Text resourceText;

            public int CurrentValue { get; set; }
        }
    }
}
#endif