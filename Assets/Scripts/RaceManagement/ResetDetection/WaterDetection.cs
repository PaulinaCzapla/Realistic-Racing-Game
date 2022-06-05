using Events.ScriptableObjects;
using SoundManagement;
using UnityEngine;

namespace RaceManagement.ResetDetection
{
    public class WaterDetection : MonoBehaviour
    {
        //private ControlPoint _pointcontrol;
        //[SerializeField] private CarSO car;
        [SerializeField] private SoundEventChannelSO playSound;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BackToCheckpoint backToCheckpoint))
            {
                backToCheckpoint.ResetPosition();
                playSound.RaiseEvent(SoundName.WaterSplash);
            }
        }
    }
}
