using UnityEngine;
using UnityEngine.Events;
using VisualNovel;

namespace Events.ScriptableObjects
{
    /// <summary>
    /// Event channel with ScriptSO parameter
    /// </summary>
    [CreateAssetMenu(menuName = "CarSimulator/Events/Script Event Channel")]
    public class ScriptEventChannelSO : BaseEventChannelSO
    {
        public UnityAction<ScriptSO, int> OnEventRaised;

        public void RaiseEvent(ScriptSO arg, int arg1)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(arg, arg1);
        }
    }
}