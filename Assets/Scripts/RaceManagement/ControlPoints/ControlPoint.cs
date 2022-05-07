using Events.ScriptableObjects;
using UnityEngine;

namespace RaceManagement.ControlPoints
{
    public class ControlPoint : MonoBehaviour
    {
        public GameObject SpawnPoint => spawnPoint;
        
        [SerializeField] protected GameObject spawnPoint;
        [SerializeField] protected ControlPointEnterEventChannelSO  onControlPointEntered;
        protected virtual void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out RaceParticipant participant))
            {
                onControlPointEntered.RaiseEvent(participant, this);
            }
        }
    }
}
