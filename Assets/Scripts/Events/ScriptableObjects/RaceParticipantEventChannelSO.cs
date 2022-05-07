using RaceManagement;
using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "CarSimulator/Events/RaceParticipant Event Channel")]
    public class RaceParticipantEventChannelSO : BaseEventChannelSO
    {
        public UnityAction<RaceParticipant> OnEventRaised;

        public void RaiseEvent(RaceParticipant arg)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(arg);
        }
    }
}