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
        // _lowAccelSource = SetUpEngineAudioSource(lowAccelClip);
        // _highDecelSource = SetUpEngineAudioSource(highDecelClip);
        // _lowDecelSource = SetUpEngineAudioSource(lowDecelClip);
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
        // float newLowPitch = pitch * pitchMultiplier;
        // float newHighPitch = newLowPitch * highPitchMultiplier;

        // _lowAccelSource.pitch = newLowPitch;
        // _lowDecelSource.pitch = newLowPitch;
        // _highAccelSource.pitch = newHighPitch;
        // _highDecelSource.pitch = newHighPitch;

        // float normalizedRPM = Mathf.Min(car.totalPower / car.MAXRpm, 1f);
        // float acceleration = inputReader.GasPressed ? 1 : 0;
        // _accFade = Mathf.Lerp(_accFade, Mathf.Abs(acceleration), 20 * Time.deltaTime);

        // float decFade = 1 - _accFade;

        // float highFade = _accFade;//Mathf.InverseLerp(0.2f, 0.8f, normalizedRPM);
        // float lowFade = 1 - highFade;

        // highFade = Mathf.InverseLerp(0.2f, 0.8f, normalizedRPM);
        // lowFade = 1 - ((1 - lowFade) * (1 - lowFade));
        // decFade = 1 - (1 - decFade) * (1 - decFade);

        // _lowAccelSource.volume = Mathf.Lerp(_lowAccelSource.volume, lowFade * _accFade, 30 * Time.deltaTime);
        // _lowDecelSource.volume = Mathf.Lerp(_lowDecelSource.volume, lowFade * decFade, 30 * Time.deltaTime);
        // _highAccelSource.volume = Mathf.Lerp(_highAccelSource.volume, highFade * _accFade, 30 * Time.deltaTime);
        // _highDecelSource.volume = Mathf.Lerp(_highDecelSource.volume, highFade * decFade, 30 * Time.deltaTime);
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
