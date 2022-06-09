using UnityEngine;
using UnityEngine.Events;
using VisualNovel;

namespace Events.ScriptableObjects
{
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