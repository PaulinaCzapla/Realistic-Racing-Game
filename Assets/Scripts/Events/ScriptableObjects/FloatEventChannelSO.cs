using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects
{
    /// <summary>
    /// Event channel with float parameter
    /// </summary>
    [CreateAssetMenu(menuName = "CarSimulator/Events/Float Event Channel")]
    public class FloatEventChannelSO : BaseEventChannelSO
    {
        public UnityAction<float> OnEventRaised;

        public void RaiseEvent(float arg)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(arg);
        }
    }
}