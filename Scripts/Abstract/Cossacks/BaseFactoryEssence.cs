#if ENABLE_ERRORS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Cossack
{
    public abstract class BaseFactoryEssence<T> : BaseFactory<T>
    {
        public enum TypeEssence
        {
            Unit,
            House,
            Forest,
        }

        [SerializeField] public bool isSelecting;
        [SerializeField] public int hp;
        [SerializeField] public int maxHp;
        [SerializeField] public int armor;
        [SerializeField] public Vector3 scale;

        public abstract TypeEssence typeEssence { get; }
        public BaseCommandRef[] listCommmand { get; private protected set; }
    }
}
#endif