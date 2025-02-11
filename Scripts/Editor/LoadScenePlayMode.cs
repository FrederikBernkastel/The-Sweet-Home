#if UNITY_EDITOR

using NaughtyAttributes;
using RAY_Core;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace RAY_Core
{
    [CreateAssetMenu(fileName = "SceneAutoLoader", menuName = "ScriptableObjects/SceneAutoLoader")]
    public sealed class SceneAutoLoader : ScriptableObject
    {
        [SerializeField][Required] private SceneAsset sceneToLoad;

        private void OnValidate()
        {
            EditorSceneManager.playModeStartScene = sceneToLoad;
        }
    }

#endif
}