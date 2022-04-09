using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameScene", menuName = "Scene Data/GameSceneSO")]
    public class GameSceneSO : ScriptableObject
    {
        public SceneAsset sceneAsset;
        public string SceneName => sceneAsset.name;
    }
}