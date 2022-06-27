using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects
{
    /// <summary>
    /// Event channel with Vector2 parameter
    /// </summary>
    [CreateAssetMenu(menuName = "CarSimulator/Events/Vector2 Event Channel")]
    public class Vector2EventChannelSO : BaseEventChannelSO
    {
        public UnityAction<Vector2> OnEventRaised;

        public void RaiseEvent(Vector2 arg)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(arg);
        }
    }
}