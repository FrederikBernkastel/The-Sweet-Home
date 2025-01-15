using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    [CreateAssetMenu(fileName = "MainData", menuName = "ScriptableObjects/MainData", order = 1)]
    public class MainData : ScriptableObject
    {
        [Header("Scenes")]
        [SerializeField][Scene] public string LoadScene;
        [SerializeField][Scene] public string MenuScene;
        [SerializeField][Scene] public string GameScene;

        [Header("Prefabs")]
        [SerializeField][Required] public ViewLoading PrefabViewLoading;
        //[SerializeField][Required] public ViewMessageBox PrefabViewMessageBox;

        [Header("RefStorageView")]
        [SerializeField][Tag] public string RefStorageView;
    }
}
