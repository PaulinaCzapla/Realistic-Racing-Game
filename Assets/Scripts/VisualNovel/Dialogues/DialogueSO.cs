using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VisualNovel.Dialogues
{
    [CreateAssetMenu(menuName = "CarSimulator/ScriptableObjects/Dialogue")]
    [Serializable]
    public class DialogueSO : ScriptableObject
    {
        [Header("Characters (first is always the speaker)")]
        public List<DialogueSceneElement> sceneElements;
        [Header("Dialogue of the first character")]
        public string dialogueSegment;
        [Header("Character name")]
        public string name;

        [Range(0,5)]
        public int numOfFadeInCharacters = 1;
        public bool shouldFadeIn;
        public bool shouldOverridePreviousDialogue = true;
    }
}