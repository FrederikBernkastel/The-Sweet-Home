#if ENABLE_ERRORS

using NaughtyAttributes;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseItemSO : ScriptableObject
    {
        [BoxGroup("ItemInfo")]
        [SerializeField] public string NameObject;
        [SerializeField] public string VisibleName;
        [SerializeField] public TypeItem TypeItem;

        public abstract BaseItem CreateItem();
    }
}
#endif