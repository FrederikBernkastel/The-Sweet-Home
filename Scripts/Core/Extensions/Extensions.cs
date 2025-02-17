using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
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
        //public static void Dispose<T, Y>(this Dictionary<T, Y> pairs) where Y : RAY_Core.IDispose
        //{
        //    foreach (var s in pairs)
        //    {
        //        s.Value.Dispose();
        //    }
        //}
        public static bool TryGetValueWithoutKey<T, Y>(this IReadOnlyDictionary<T, Y> pair, T key, out Y value) where Y : class
        {
            return pair.TryGetValue(key, out value) ? value != default : false;
        }
        public static bool TryGetValueWithoutKey<T, Y, N>(this IReadOnlyDictionary<T, Y> pair, T key, out N value) where Y : class where N : class
        {
            if (pair.TryGetValue(key, out var _value))
            {
                if (_value != default)
                {
                    value = _value as N;

                    if (value == default)
                    {
                        return false;
                    }

                    return true;
                }

                value = default;

                return false;
            }

            value = default;

            return false;
        }
        public static Y TryGetValueWithoutKey<T, Y>(this IReadOnlyDictionary<T, Y> pair, T key) where Y : class
        {
            return pair.TryGetValue(key, out var value) ? value : default;
        }
    }
}
