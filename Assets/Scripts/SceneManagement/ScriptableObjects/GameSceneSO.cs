using System;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace SceneManagement.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameScene", menuName = "Scene Data/GameSceneSO")]
    public class GameSceneSO : ScriptableObject
    {
#if UNITY_EDITOR
        public SceneAsset sceneAsset;
#endif
        public string SceneName;

#if UNITY_EDITOR
        private void OnEnable()
        {
            SceneName = sceneAsset.name;
        }
#endif
    }
}