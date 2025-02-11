#if ENABLE_ERRORS

using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace RAY_Core
{
    public abstract class BaseFactoryObject<T> : BaseFactory<T> where T : class
    {
        private protected ObjectPool<T> listPool { get; }
        private protected List<T> listActive { get; }
        public IEnumerable<T> ListActive => listActive;

        public BaseFactoryObject(Transform storage, int capacity, ObjectPool<T> objectPool) : base(storage, capacity)
        {
            listPool = objectPool;
            listActive = new(capacity);
        }

        public override void Init()
        {
            for (int i = 0; i < Capacity; i++)
            {
                listActive.Add(listPool.Get());
            }
            listActive.Clear();
        }
        public override T Get()
        {
            var obj = listPool.Get();
            listActive.Add(obj);
            return obj;
        }
        public override void Release(T obj)
        {
            listActive.Remove(obj);
            listPool.Release(obj);

        }
        public override void Dispose()
        {
            listActive.Clear();
            listPool.Dispose();
        }
    }
}
#endif