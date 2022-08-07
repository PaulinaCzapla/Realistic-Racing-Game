using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects
{
    /// <summary>
    /// Event channel without parameter
    /// </summary>
    [CreateAssetMenu(menuName = "CarSimulator/Events/Void Event Channel")]
    public class VoidEventChannelSO : BaseEventChannelSO
    {
        public UnityAction OnEventRaised;

        public void RaiseEvent()
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke();
        }
    }
}