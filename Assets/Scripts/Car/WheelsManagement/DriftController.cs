using InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftController : MonoBehaviour
{
    [SerializeField] private List<WheelCollider> wheel;
    [SerializeField] private GameplayInputReader inputReader;
    [SerializeField] private Rigidbody rb;

    private float _minStiffness = 1f;
    private float _maxStiffness = 2f;

    // Update is called once per frame
    void FixedUpdate()
    {
        
        ControlDrift(rb.velocity.magnitude * 3.6f);
    }

    private void ControlDrift(float vel)
    {
            WheelHit hit;
            for (int i = 0; i < wheel.Count; i++)
            {
                if (wheel[i].GetGroundHit(out hit))
                {
                    WheelFrictionCurve forwardFriction = wheel[i].forwardFriction;
                    forwardFriction.stiffness = (inputReader.HandBrakePressed) ? Mathf.SmoothDamp(forwardFriction.stiffness, _minStiffness, ref vel, Time.deltaTime * 2) : _maxStiffness;
                    wheel[i].forwardFriction = forwardFriction;

                    WheelFrictionCurve sidewaysFriction = wheel[i].sidewaysFriction;
                    sidewaysFriction.stiffness = (inputReader.HandBrakePressed) ? Mathf.SmoothDamp(sidewaysFriction.stiffness, _minStiffness, ref vel, Time.deltaTime * 2) : _maxStiffness;
                    wheel[i].sidewaysFriction = sidewaysFriction;
                }
            }
    }

    }

