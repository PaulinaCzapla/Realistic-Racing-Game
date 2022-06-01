using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car", menuName = "CarSimulator/ScriptableObjects/Car")]
public class CarSO : ScriptableObject
{
    [Header("Suspension variables")]
    public AnimationCurve _downForce;


    [Header("Suspension checking variables")]

    [Header("Engine variables")]
    public DriveType drive;
    public float totalPower = 1.7e-06f;
    public AnimationCurve _engineTorque;
    public float MAXRpm = 10000;

    [Header("Gearbox variables")]
    public GearBoxType gearType = GearBoxType.HALF;
    public int gearNum = 1;
    public  float[] gears = { };
    public float finalDrive = 3.4f;

    [Header("Engine checking variables")]
    public float engineRpm = 1000.0f;
    public float vertical = 1f;
    public float engineLerpValue = 9000;
    public float minBrakeRpm = 2000.0F;

    [Header("Wheels variables")]
    public AnimationCurve _maxSteerAngle;

    [Header("Wheels checking variables")]
    //public float _wheelRPM = 0.0f;
    //public float _velocity = 0.0f;
    
    [Header("Motion, machanics and time related constance")]
    public float turnOnRpm = 1000;
    public float smoothTime = 0.2f;
    public float lerpSmoothTime = 5;
    public float carSpeed = 0;
}
