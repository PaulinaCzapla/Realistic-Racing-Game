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
    public float _totalPower = 0.0f;
    public AnimationCurve _engineTorque;
    public float _maxRPM = 12000;

    [Header("Gearbox variables")]
    public GearBoxType _gearType;
    public int _gearNum;
    public float[] _gears = { };
    public float _finalDrive = 0.0f;

    [Header("Engine checking variables")]
    public float _engineRPM = 1000.0f;
    public float _vertical = 0.0f;
    public float _engineLerpValue;
    public float _minBrakeRPM = 2000.0F;

    [Header("Wheels variables")]



    [Header("Wheels checking variables")]
    //public float _wheelRPM = 0.0f;
    //public float _velocity = 0.0f;
    

    [Header("Motion, machanics and time related constance")]
    public float _turnOnRPM = 1000;
    public float _smoothTime = 0.2f;
    public float lerpSmoothTime = 5;
}
