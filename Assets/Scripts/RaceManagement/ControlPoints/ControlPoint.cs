using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;

namespace RaceManagement.ControlPoints
{
    /// <summary>
    /// A class that represents control point/checkpoint, that can be activated by player.
    /// /// </summary>
    public class ControlPoint : MonoBehaviour
    {
        public GameObject SpawnPoint => spawnPoint;

        [SerializeField] public List<GameObject> spawnPoints = new List<GameObject>();
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
