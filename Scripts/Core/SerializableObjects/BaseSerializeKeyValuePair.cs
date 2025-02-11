using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    [Serializable]
    public abstract class BaseSerializeKeyValuePair<T, Y> where T : Enum
    {
        public abstract T Key { get; }
        public abstract Y Value { get; }
    }
    [Serializable]
    public sealed class SerializePairChannel<T, Y> : BaseSerializeKeyValuePair<T, Y> where T : Enum
    {
        [SerializeField] private T typeChannel;
        [SerializeField][Required] private Y clip;

        public override T Key => typeChannel;
        public override Y Value => clip;
    }
}
