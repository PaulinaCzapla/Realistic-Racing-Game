using Events.ScriptableObjects;
using UnityEngine;

namespace RaceManagement.ControlPoints
{
    public class FinishPoint : ControlPoint
    {
        [SerializeField] private RaceParticipantEventChannelSO onFinishPointEntered;
        protected override void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out RaceParticipant participant))
            {
                onControlPointEntered.RaiseEvent(participant, this);
                onFinishPointEntered.RaiseEvent(participant);
            }
        }
    }
}
