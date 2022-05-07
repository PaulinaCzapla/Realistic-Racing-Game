using RaceManagement.ControlPoints;
using UnityEngine;

namespace RaceManagement.ResetDetection
{
    public class FlippedOverDetection : MonoBehaviour
    {
        public bool flipped = false;
        private float _resetTime = 0f;
        private ControlPoint _pointcontrol;
        private RaceParticipant _raceParticipant;
        private void Start()
        {
            _raceParticipant = GetComponent<RaceParticipant>();
        }

        void Update()
        {
            if (transform.position.y < 5 && transform.eulerAngles.z > 90 && transform.eulerAngles.z < 300)
            { 
                Timer();
            }
            else
            {
                _resetTime = 0f;
            }
        }

        private void Timer()
        {
            _pointcontrol = _raceParticipant.ControlPointsActivated[_raceParticipant.ControlPointsActivated.Count - 1];
            if (_resetTime > 2f)
            {
                transform.rotation = _pointcontrol.SpawnPoint.transform.rotation;
                transform.position = _pointcontrol.SpawnPoint.transform.position;
            }
            _resetTime += Time.deltaTime;
        }
    }
}
