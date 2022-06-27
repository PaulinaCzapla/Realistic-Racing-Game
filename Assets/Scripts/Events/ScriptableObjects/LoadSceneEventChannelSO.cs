using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects
{
    /// <summary>
    /// Event channel with GameSceneSO parameter
    /// </summary>
    [CreateAssetMenu(menuName = "CarSimulator/Events/Load Scene Event Channel")]
    public class LoadSceneEventChannelSO : BaseEventChannelSO
    {
        public UnityAction<GameSceneSO, bool> OnEventRaised;
        public UnityAction OnPhotonEventRaised;

        public void RaiseEvent(GameSceneSO arg0, bool arg1)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(arg0, arg1);
        }
        
        public void RaiseEvent()
        {
            if (OnPhotonEventRaised != null)
                OnPhotonEventRaised.Invoke();
        }
    }
}