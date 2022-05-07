using RaceManagement.ControlPoints;
using UnityEngine;

namespace RaceManagement.ResetDetection
{
    public class WaterDetection : MonoBehaviour
    {
        private ControlPoint _pointcontrol;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out RaceParticipant participant))
            {
                _pointcontrol = participant.ControlPointsActivated[participant.ControlPointsActivated.Count - 1];
                other.gameObject.transform.rotation = _pointcontrol.SpawnPoint.transform.rotation;
                other.gameObject.transform.position = _pointcontrol.SpawnPoint.transform.position;
            }
        }
    }
}
