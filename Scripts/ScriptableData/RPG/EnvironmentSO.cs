#if ENABLE_ERRORS

using NaughtyAttributes;
using UnityEngine;

namespace RAY_Core
{
    [CreateAssetMenu(fileName = "EnvironmentSO", menuName = "ListEnvironmentsSO/EnvironmentSO")]
    public class EnvironmentSO : ScriptableObject
    {
        [BoxGroup("EnvironmentInfo")]
        [SerializeField] public BaseItemSO[] ListItemsSO;
    }
}
#endif