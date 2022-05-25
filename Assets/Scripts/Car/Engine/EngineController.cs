using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EngineController 
{
    private CarSO _car;

    private bool _engineLerp = false;
    private float vel = 0.0f;

    public EngineController(CarSO car)
    {
        this._car = car;
    }

    public void CalculateEnginePower(float wheelRPM, float velocity, bool clutch, float vertical)
    {
        
        LerpEngine(velocity);
        if (_car.engineRpm >= CarSO.MAXRpm) SetEngineLerp(CarSO.MAXRpm - 1000);
        if (!_engineLerp)
        {
            _car.engineRpm =  Mathf.SmoothDamp(_car.engineRpm, _car.turnOnRpm + (Mathf.Abs(wheelRPM) * _car.finalDrive * _car.gears[_car.gearNum]), ref vel, _car.smoothTime * Time.deltaTime) ;
            _car.totalPower = (float)(clutch ? 0.0 :  _car._engineTorque.Evaluate(_car.engineRpm) * (_car.gears[_car.gearNum]) * _car.finalDrive * (Mathf.Abs(vertical)+0.000000001));
        }
        
    }
    
    private void SetEngineLerp(float num)
    {
        _engineLerp = true;
        _car.engineLerpValue = num;
    }

    private void LerpEngine(float velocity)
    {
        if (_engineLerp)
        {
            //_car._engineRPM = Mathf.SmoothDamp(_car._engineRPM, _car._engineLerpValue, ref vel, _car.lerpSmoothTime * Time.deltaTime);
           _car.engineRpm = Mathf.Lerp(_car.engineRpm, _car.engineLerpValue,  _car.lerpSmoothTime * Time.deltaTime);
            _engineLerp = _car.engineRpm <= _car.engineLerpValue + 100 ? false : true;
        }
    }
    
    
}
