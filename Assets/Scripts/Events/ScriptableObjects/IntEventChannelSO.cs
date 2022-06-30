using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects
{
    /// <summary>
    /// Event channel with Int parameter
    /// </summary>
    [CreateAssetMenu(menuName = "CarSimulator/Events/Int Event Channel")]
    public class IntEventChannelSO : BaseEventChannelSO
    {
        public UnityAction<int> OnEventRaised;

        public void RaiseEvent(int arg)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(arg);
        }
    }
}