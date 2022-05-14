using RaceManagement.ControlPoints;
using UnityEngine;

namespace RaceManagement.ResetDetection
{
    public class FlippedOverDetection : MonoBehaviour
    {
        public bool flipped = false;
        private float _resetTime = 0f;
        //private ControlPoint _pointcontrol;
        //private RaceParticipant _raceParticipant;
        private BackToCheckpoint _backToCheckpoint;
        //[SerializeField] private CarSO car;

        private void Start()
        {
            _backToCheckpoint = GetComponent<BackToCheckpoint>();
        }

        void Update()
        {
            if (transform.position.y < 5 && transform.eulerAngles.z > 80 && transform.eulerAngles.z < 300)
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
            if (_resetTime > 2f)
            {
                _backToCheckpoint.ResetPosition();
            }
            _resetTime += Time.deltaTime;
        }
    }
}
