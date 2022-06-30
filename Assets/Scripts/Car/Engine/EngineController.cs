using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class controlling engine rpm and motor torque
/// </summary>
public class EngineController 
{
    private CarSO _car;
    

    private bool _engineLerp = false;
    private float vel = 0.0f;

    public EngineController(CarSO car)
    {
        this._car = car;
    }

    /// <summary>
    /// Method for calculating current engine total power
    /// </summary>
    /// <param name="wheelRPM">
    /// Current wheels RPM taken from wheel colliders
    /// </param>
    /// <param name="velocity">
    /// Velocity of the car referenced to adjust stiffness in particular frame in Km/h
    /// </param>
    /// <param name="clutch">
    /// Determiner if the clutch is pressed 
    /// </param>
    /// <param name="vertical">
    /// Input of the gas pedal from 0-1
    /// </param>
    public void CalculateEnginePower(float wheelRPM, float velocity, bool clutch, float vertical)
    {
        
        LerpEngine(velocity*3.6f);
        if (_car.engineRpm >= _car.MAXRpm) SetEngineLerp(_car.MAXRpm - 1000);
        if (!_engineLerp)
        {
            _car.engineRpm =  Mathf.SmoothDamp(_car.engineRpm, _car.turnOnRpm + (Mathf.Abs(wheelRPM) * _car.finalDrive * _car.gears[_car.gearNum]), ref velocity, _car.smoothTime * Time.deltaTime) ;
            _car.totalPower = (float)(clutch ? 0.0 :  _car._engineTorque.Evaluate(_car.engineRpm) * (_car.gears[_car.gearNum]) * _car.finalDrive * (Mathf.Abs(vertical) + 0.000000001));
        }
        
    }

    /// <summary>
    /// Set the lerp loop
    /// </summary>
    /// <param name="num">
    /// lerp value to be settled
    /// </param>
    private void SetEngineLerp(float num)
    {
        _engineLerp = true;
        _car.engineLerpValue = num;
    }

    /// <summary>
    /// Engine lerping function, to prevent player from overreaching RPM value
    /// </summary>
    /// <param name="velocity">
    /// Velocity of the car referenced to adjust stiffness in particular frame in Km/h
    /// </param>
    private void LerpEngine(float velocity)
    {
        if (_engineLerp)
        {
           
           _car.engineRpm = Mathf.Lerp(_car.engineRpm, _car.engineLerpValue,  _car.lerpSmoothTime * Time.deltaTime);
            _engineLerp = _car.engineRpm <= _car.engineLerpValue + 100 ? false : true;
        }
    }
    
    
}
