using UnityEngine;

namespace VisualNovel

{
    [CreateAssetMenu(menuName = "CarSimulator/ScriptableObjects/ScriptInfo")]
    public class ScriptInfoSO : ScriptableObject
    {
        public ScriptSO CurrentlySelectedScript;//{ get; set; }
        public int CurrentDialogueScene =0;
    }
}