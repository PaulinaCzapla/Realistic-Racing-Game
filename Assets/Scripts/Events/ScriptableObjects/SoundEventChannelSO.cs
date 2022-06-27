using SoundManagement;
using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects
{
    /// <summary>
    /// Event channel with SoundName parameter
    /// </summary>
    [CreateAssetMenu(menuName = "CarSimulator/Events/Sound Event Channel")]
    public class SoundEventChannelSO : BaseEventChannelSO
    {
        public UnityAction<SoundName> OnEventRaised;

        public void RaiseEvent(SoundName arg)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(arg);
        }
    }
}