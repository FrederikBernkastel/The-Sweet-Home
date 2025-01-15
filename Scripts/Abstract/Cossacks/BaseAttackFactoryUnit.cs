#if ENABLE_ERRORS

using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Cossacks
{
    public abstract class BaseAttackFactoryUnit<T> : BaseFactoryUnit<T>
    {
        [SerializeField] public int damage;
        [SerializeField] public string nameIsAttack;
        [SerializeField] public int radius;
        [SerializeField] public float distanceStop;
        [SerializeField] public bool isAutoAttack;
        [SerializeField][Tag] public string[] tagsEnemys;
        [SerializeField] public LayerMask[] layerMasksAttack;
    }
}
#endif