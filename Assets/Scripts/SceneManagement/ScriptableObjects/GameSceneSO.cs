using System;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace SceneManagement.ScriptableObjects
{
    /// <summary>
    /// Represents game scene
    /// /// </summary>
    /// 
    [CreateAssetMenu(fileName = "GameScene", menuName = "Scene Data/GameSceneSO")]
    public class GameSceneSO : ScriptableObject
    {
#if UNITY_EDITOR
        public SceneAsset sceneAsset;
#endif
        public string SceneName { get; private set; }

#if UNITY_EDITOR
        private void OnEnable()
        {
            SceneName = sceneAsset.name;
        }
#endif
    }
}