using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RAY_Core
{
    [CreateAssetMenu(fileName = "ButtonData", menuName = "ScriptableObjects/ButtonData", order = 1)]
    public class ButtonData : ScriptableObject
    {
        [Header("General")]
        [SerializeField][Required] public Sprite Icon;
        [SerializeField] public string Name;
    }
}
