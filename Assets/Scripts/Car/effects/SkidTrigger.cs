using InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class handling skid marking and smoke on race track
/// </summary>
public class SkidTrigger : MonoBehaviour
{
    [SerializeField] private GameplayInputReader inputReader;
    [SerializeField] private List<SkidMaker> skidMakers;

    private bool skidAplaience;

    private void Start()
    {
        for (int i = 0; i < skidMakers.Count; i++)
        {
            skidMakers[i].emiisionObject.GetComponent<ParticleSystem>().enableEmission = false;
            skidMakers[i].emiisionObject.GetComponent<TrailRenderer>().emitting = false;
        }
    }

    private void Update()
    {
        handleSkid();
    }

    /// <summary>
    /// Starts emiting traces when brake is hit
    /// </summary>
    public void handleSkid()
    {
        if (inputReader.BrakePressed || inputReader.HandBrakePressed) startSkid();
        else stopSkid();
    }

    /// <summary>
    /// Starts emitinge traces if the wheel is on ground
    /// </summary>
    public void startSkid()
    {
        WheelHit hit;
        for (int i = 0; i < skidMakers.Count; i++)
        {
            if(skidMakers[i].wheel.GetGroundHit(out hit))
            {
                skidMakers[i].emiisionObject.GetComponent<ParticleSystem>().enableEmission = true;
                skidMakers[i].emiisionObject.GetComponent<TrailRenderer>().emitting = true;
            }
            else
            {
                skidMakers[i].emiisionObject.GetComponent<ParticleSystem>().enableEmission = false;
                skidMakers[i].emiisionObject.GetComponent<TrailRenderer>().emitting = false;
            }
        }
        skidAplaience = true;
    }

    /// <summary>
    /// Stops emitinge traces if the wheel is on ground
    /// </summary>
    public void stopSkid()
    {
        if (!skidAplaience) return;
        for (int i = 0; i < skidMakers.Count; i++)
        {
                skidMakers[i].emiisionObject.GetComponent<ParticleSystem>().enableEmission = false;
                skidMakers[i].emiisionObject.GetComponent<TrailRenderer>().emitting = false;
        }
        skidAplaience = false;
    }
}
