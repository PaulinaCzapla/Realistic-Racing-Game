using RaceManagement;
using RaceManagement.ControlPoints;
using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects
{
    /// <summary>
    /// Event channel with ControlPoint parameter
    /// </summary>
    [CreateAssetMenu(menuName = "CarSimulator/Events/ControlPointEnter Event Channel")]
    public class ControlPointEnterEventChannelSO : BaseEventChannelSO
    {
        public UnityAction<RaceParticipant, ControlPoint> OnEventRaised;

        public void RaiseEvent(RaceParticipant arg, ControlPoint controlPoint)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(arg, controlPoint);
        }
    }
}