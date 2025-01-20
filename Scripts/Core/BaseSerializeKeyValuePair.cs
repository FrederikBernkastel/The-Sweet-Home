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
    public class SerializePairChannel<T, Y> : BaseSerializeKeyValuePair<T, Y> where T : Enum
    {
        [SerializeField] private protected T typeChannel;
        [SerializeField][Required] private protected Y clip;

        public override T Key => typeChannel;
        public override Y Value => clip;
    }
    public static class DictionaryExtensions
    {
        public static Dictionary<T, Y> Init<T, Y>(this Dictionary<T, Y> pairs) where T : Enum
        {
            pairs.Clear();

            foreach (var s in Enum.GetValues(typeof(T)))
            {
                pairs.Add((T)s, default);
            }

            return pairs;
        }
        public static void Add<T, Y>(this Dictionary<T, Y> pairs, IEnumerable<BaseSerializeKeyValuePair<T, Y>> valuePairs) where T : Enum
        {
            foreach (var s in valuePairs)
            {
                if (pairs.ContainsKey(s.Key))
                {
                    pairs[s.Key] = s.Value;

                    return;
                }
                else
                {
                    throw new Exception();
                }
            }
        }
        public static void Dispose<T, Y>(this Dictionary<T, Y> pairs)
        {
            pairs.Clear();
        }
        public static bool TryGetValueWithoutKey<T, Y>(this Dictionary<T, Y> pair, T key, out Y value) where Y: class
        {
            return pair.TryGetValue(key, out value) ? value != default : false;
        }
    }
}
