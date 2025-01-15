#if ENABLE_ERRORS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace RAY_Core
{
    public class FactoryGameObject : BaseFactoryObject<GameObject>
    {
        public override string NameEssence { get; }

        public FactoryGameObject(GameObject prefab, string nameEssence, Transform storage, int capacity) : base(storage, capacity, new ObjectPool<GameObject>(
            () => GameObject.Instantiate(prefab, storage),
            u => u.SetActive(true),
            u => u.SetActive(false),
            u => GameObject.Destroy(u),
            true, capacity, capacity))
        {
            NameEssence = nameEssence;
        }
    }
}
#endif