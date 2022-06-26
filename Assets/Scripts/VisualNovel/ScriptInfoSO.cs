using UnityEngine;

namespace VisualNovel

{
    /// <summary>
    /// A scriptable object that contains information about current tutorial script
    /// </summary>
    [CreateAssetMenu(menuName = "CarSimulator/ScriptableObjects/ScriptInfo")]
    public class ScriptInfoSO : ScriptableObject
    {
        public ScriptSO CurrentlySelectedScript;//{ get; set; }
        public int CurrentDialogueScene =0;
    }
}