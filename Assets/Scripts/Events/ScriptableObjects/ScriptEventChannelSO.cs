using UnityEngine;
using UnityEngine.Events;
using VisualNovel;

namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "CarSimulator/Events/Script Event Channel")]
    public class ScriptEventChannelSO : BaseEventChannelSO
    {
        public UnityAction<ScriptSO> OnEventRaised;

        public void RaiseEvent(ScriptSO arg)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(arg);
        }
    }
}