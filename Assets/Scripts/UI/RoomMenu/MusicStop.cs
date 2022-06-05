using Events.ScriptableObjects;
using SoundManagement;
using UnityEngine;

public class MusicStop : MonoBehaviour
{
    [SerializeField] private SoundEventChannelSO stopMenuMusic;
    private void OnDisable()
    {
        stopMenuMusic.RaiseEvent(SoundName.MenuMusic);
    }
}
