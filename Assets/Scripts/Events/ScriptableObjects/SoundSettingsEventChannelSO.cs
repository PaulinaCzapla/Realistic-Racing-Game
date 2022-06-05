using SoundManagement;
using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "CarSimulator/Events/Sound Settings Event Channel")]
    public class SoundSettingsEventChannelSO : BaseEventChannelSO
    {
        public UnityAction<float, float> OnEventRaised;

        public void RaiseEvent(float musicVolume, float soundVolume)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(musicVolume, soundVolume);
        }
    }
}