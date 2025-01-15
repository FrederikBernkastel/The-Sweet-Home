#if ENABLE_ERRORS

using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RAY_Core
{
    public abstract class BaseFactory<T>
    {
        public Transform Storage { get; }
        public int Capacity { get; }

        public BaseFactory(Transform storage, int capacity)
        {
            Storage = storage;
            Capacity = capacity;
        }

        public abstract void Init();
        public abstract T Get();
        public abstract void Release(T obj);
        public abstract void Dispose();
        public abstract string NameEssence { get; }
    }
}
#endif