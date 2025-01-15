#if ENABLE_ERRORS

using NaughtyAttributes;
using System;
using UnityEngine;

namespace RAY_Core
{
    [CreateAssetMenu(fileName = "CharacterSO", menuName = "ListCharactersSO/CharacterSO")]
    public class CharacterSO : ScriptableObject
    {
        [BoxGroup("CharacterInfo")]
        [SerializeField] public string NameObject;
        [SerializeField] public string VisibleName;
        [SerializeField] public int LevelCharacter;
        [SerializeField] public BaseItemSO[] ListItemsSO;
        [SerializeField] public FieldPutOnItem[] ListItemsPutOnSO;

        [Serializable]
        public class FieldPutOnItem
        {
            [SerializeField] public TypeItem TypeItem;
            [SerializeField][Required] public BaseItemSO ItemSO;
        }
    }
}
#endif