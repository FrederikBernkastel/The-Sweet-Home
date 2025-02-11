#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_TestTask_1;
using System;
using UnityEngine;

namespace RAY_Core
{
    [CreateAssetMenu(fileName = "ArmorSO", menuName = "ListItemsSO/ArmorSO")]
    public class ArmorSO : BaseItemSO
    {
        [BoxGroup("Stats")]
        [SerializeField] public Stat[] listStats;

        public ArmorItem CreateArmorItem()
        {
            return new ArmorItem(this);
        }
        public override BaseItem CreateItem()
        {
            return CreateArmorItem();
        }

        [Serializable]
        public class Stat
        {
            [SerializeField] public TypeStatArmor statEnum;
            [SerializeField] public int statNumber;
        }
    }
}
#endif