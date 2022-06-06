using Events.ScriptableObjects;
using UnityEngine;

namespace SoundManagement
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip splashClip;
        [SerializeField] private AudioClip carColisionClip;
        [SerializeField] private AudioClip trackColisionClip;
        [SerializeField] private AudioClip menuMusicClip;

        private AudioSource _splashSource;
        private AudioSource _carColisionSource;
        private AudioSource _trackColisionSource;
        private AudioSource _menuMusicSource;

        [Header("EventChannels")]
        [SerializeField] private SoundEventChannelSO onSoundPlay;
        [SerializeField] private SoundEventChannelSO onMenuMusicStart;
        [SerializeField] private SoundEventChannelSO onMenuMusicStop;
        [SerializeField] private SoundSettingsEventChannelSO onSoundSettingsChange;

        private void OnStart()
        {
            OnMenuMusicStart(SoundName.MenuMusic);
        }
        private void OnEnable()
        {
            SetUpAudioSources();
            onMenuMusicStop.OnEventRaised += OnMenuMusicStop;
            onSoundPlay.OnEventRaised += OnSoundPlay;
            onMenuMusicStart.OnEventRaised += OnMenuMusicStart;
            onSoundSettingsChange.OnEventRaised += OnSoundSettingsChange;
        }
        private void OnDisable()
        {
            onSoundPlay.OnEventRaised -= OnSoundPlay;
            onMenuMusicStop.OnEventRaised -= OnMenuMusicStop;
            onMenuMusicStart.OnEventRaised -= OnMenuMusicStart;
            onSoundSettingsChange.OnEventRaised -= OnSoundSettingsChange;
            foreach (var source in GetComponents<AudioSource>())
            {
                Destroy(source);
            }
        }

        private void OnSoundPlay(SoundName soundName)
        {
            switch (soundName)
            {
                case SoundName.WaterSplash:
                    _splashSource.Play();
                    return;
                case SoundName.CarColision:
                    _carColisionSource.Play();
                    return;
                case SoundName.TrackColision:
                    _trackColisionSource.Play();
                    return;
                case SoundName.MenuMusic:
                    _menuMusicSource.Play();
                    return;
                default:
                    return;
            }
        }

        private void OnMenuMusicStart(SoundName soundName)
        {
            _menuMusicSource.loop = true;
            _menuMusicSource.Play();
        }

        private void OnMenuMusicStop(SoundName soundName)
        {
            _menuMusicSource.loop = false;
            _menuMusicSource.Stop();
        }

        private void OnSoundSettingsChange(float musicVolume, float soundVolume)
        {
            _menuMusicSource.volume = musicVolume;
            _carColisionSource.volume = soundVolume;
            _trackColisionSource.volume = soundVolume;
            _splashSource.volume = soundVolume;
        }
        private void SetUpAudioSources()
        {
            _splashSource = SetUpAudioSource(splashClip);
            _carColisionSource = SetUpAudioSource(carColisionClip);
            _trackColisionSource = SetUpAudioSource(trackColisionClip);
            _menuMusicSource = SetUpAudioSource(menuMusicClip);
        }

        private AudioSource SetUpAudioSource(AudioClip clip)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = 1;
            return source;
        }
    }
}