using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VisualNovel.Dialogues
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Dialogue")]
    [Serializable]
    public class DialogueSO : ScriptableObject
    {
        [Header("Characters (first is always the speaker)")]
        public List<DialogueSceneElement> sceneElements;
        [Header("Dialogue of the first character")]
        public string dialogueSegment;
        [Header("Character name")]
        public string name;

        public bool shouldFadeIn;
        public bool shouldOverridePreviousDialogue = true;
    }
}