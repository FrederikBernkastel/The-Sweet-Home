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
    public class SceneAutoLoader : ScriptableObject
    {
        [SerializeField][Scene] public string sceneToLoad;

        [InitializeOnLoadMethod]
        private static void OnLoad()
        {
            SceneAutoLoader settings = Resources.Load<SceneAutoLoader>("SceneAutoLoader");

            if (settings != null)
            {
                EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            }
            else
            {
                Debug.LogWarning("SceneAutoLoader settings not found. Create a ScriptableObject with the specified name.");
            }
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                SceneAutoLoader settings = Resources.Load<SceneAutoLoader>("SceneAutoLoader");

                if (settings != null && settings.sceneToLoad != null)
                {
                    BaseApplicationEntry.IsStartApplication = true;
                    EditorSceneManager.LoadScene(settings.sceneToLoad);
                }
                else
                {
                    Debug.LogWarning("SceneAutoLoader settings not found. Create a ScriptableObject with the specified name.");
                }
            }
        }
    }

#endif
}