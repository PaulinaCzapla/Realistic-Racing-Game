using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "CarSimulator/Events/GameObject Event Channel")]
    public class GameObjectEventChannelSO : BaseEventChannelSO
    {
        public UnityAction<GameObject> OnEventRaised;

        public void RaiseEvent(GameObject arg)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(arg);
        }
    }
}