using UnityEngine;

namespace Events.ScriptableObjects
{
    /// <summary>
    /// This is a base for class for scriptable object event channels
    /// /// </summary>
    public class BaseEventChannelSO : ScriptableObject
    {
        [TextArea] public string description;
    }
}