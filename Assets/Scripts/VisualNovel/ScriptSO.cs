using System;
using System.Collections.Generic;
using UnityEngine;
using VisualNovel.Dialogues;
using System.Linq;

namespace VisualNovel
{
    /// <summary>
    /// A class that represents tutorial script=
    /// </summary>
    [CreateAssetMenu(menuName = "CarSimulator/ScriptableObjects/Script")]
    public class ScriptSO : ScriptableObject
    {
        public List<DialogueScene> dialogueScenes;

        public int AllDialogues => dialogueScenes.Sum(x => x.dialogues.Count);
    }

    [Serializable]
    public class DialogueScene
    {
        public List<DialogueSO> dialogues;
    }
}