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

        public string SceneName;


    }
}