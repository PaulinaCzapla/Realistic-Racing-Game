using System;
using System.Collections.Generic;
using UnityEngine;
using VisualNovel.Dialogues;

namespace VisualNovel
{
    [CreateAssetMenu(menuName = "CarSimulator/ScriptableObjects/Script")]
    public class ScriptSO : ScriptableObject
    {
        public List<DialogueScene> dialogueScenes;
    }

    [Serializable]
    public class DialogueScene
    {
        public List<DialogueSO> dialogues;
    }
}