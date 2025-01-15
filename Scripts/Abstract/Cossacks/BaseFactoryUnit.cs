#if ENABLE_ERRORS

using RAY_Cossack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Cossacks
{
    public abstract class BaseFactoryUnit<T> : BaseFactoryEssence<T>
    {
        public enum TypeUnit
        {
            Peasant,
            Guard,
            Enemy,
            Shieldman,
        }

        [SerializeField] public string nameIsRun;
        [SerializeField] public string nameIsDeath;
        [SerializeField] public float distanceMovementStop;
        [SerializeField] public float speed;

        public abstract TypeUnit typeUnit { get; }
    }
}
#endif