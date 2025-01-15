using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RAY_Core
{
    [CreateAssetMenu(fileName = "HouseData", menuName = "ScriptableObjects/HouseData", order = 1)]
    public class HouseData : ScriptableObject
    {
        [Header("Prefabs")]
        [SerializeField][Required] public Image PrefabIcon;
        [SerializeField][Required] public TMP_Text PrefabText;

        [Header("General")]
        [SerializeField][Required] public Sprite Icon;
        [SerializeField] public string Name;
        [SerializeField] public string Description;

        [SerializeField] public FieldResource[] ListResources;
        [SerializeField] public FieldState[] ListStats;
    }
    [Serializable]
    public class FieldResource
    {
        [SerializeField][Required] public Sprite Icon;
        [SerializeField] public string Name;
        [SerializeField][Min(0)] public int Price;
    }
    [Serializable]
    public class FieldState
    {
        [SerializeField] public string Key;
        [SerializeField] public int Value;
    }
}
