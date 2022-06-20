using System.Collections;
using System.Collections.Generic;
using InputSystem;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    [SerializeField] private GameplayInputReader inputReader;
    [SerializeField] private CarSO car;
    [SerializeField] private AudioClip lowAccelClip;
    [SerializeField] private AudioClip lowDecelClip;
    [SerializeField] private AudioClip highAccelClip;
    [SerializeField] private AudioClip highDecelClip;
    [SerializeField] private float pitchMultiplier = 1.0f;
    [SerializeField] private float highPitchMultiplier = 0.25f;
    [SerializeField] private float lowPitchMin = 1.0f;
    [SerializeField] private float lowPitchMax = 1.5f;
    [SerializeField] private float dopplerLevel = 1.0f;
    [SerializeField] private float maxRolloffDistance = 500;
    private float _accFade = 0;
    private AudioSource _lowAccelSource;
    private AudioSource _lowDecelSource;
    private AudioSource _highAccelSource;
    private AudioSource _highDecelSource;

    private void OnEnable()
    {
        Debug.Log("Hello from enable");
        _highAccelSource = SetUpEngineAudioSource(highAccelClip);
    }

    private void OnDisable()
    {
        Debug.Log("Hello from disable");
        foreach (var source in GetComponents<AudioSource>())
        {
            Destroy(source);
        }
    }

    private void Update()
    {
        float pitch = GetNewPitch();
        _highAccelSource.pitch = Mathf.Min(pitch * pitchMultiplier * highPitchMultiplier, 1.3f);
    }

    private float GetNewPitch()
    {
        Debug.Log(car.totalPower);
        float pitch = ULerp(lowPitchMin, lowPitchMax, car.engineRpm / car.MAXRpm);
        return pitch;
    }

    private AudioSource SetUpEngineAudioSource(AudioClip clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = 0;
        source.spatialBlend = 1;
        source.loop = true;

        source.time = 0f;
        source.Play();
        source.minDistance = 5;
        source.maxDistance = maxRolloffDistance;
        source.dopplerLevel = 0;
        source.volume = 1;
        return source;
    }


    // unclamped versions of Lerp and Inverse Lerp, to allow value to exceed the from-to range
    private static float ULerp(float from, float to, float value)
    {
        return (1.0f - value) * from + value * to;
    }
}
