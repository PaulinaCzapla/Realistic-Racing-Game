using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineController 
{
    private CarSO _car;

    private bool _engineLerp = false;

    public EngineController(CarSO car)
    {
        this._car = car;
    }

    public void CalculateEnginePower(float wheelRPM, float velocity)
    {


        LerpEngine(velocity);
        if (_car._engineRPM >= _car._maxRPM) SetEngineLerp(_car._maxRPM - 1000);
        if (!_engineLerp)
        {
            _car._engineRPM = Mathf.SmoothDamp(_car._engineRPM, _car._turnOnRPM + (Mathf.Abs(wheelRPM) * _car._finalDrive * _car._gears[_car._gearNum]), ref velocity, _car._smoothTime * Time.deltaTime);
            _car._totalPower = _car._engineTorque.Evaluate(_car._engineRPM) * (_car._gears[_car._gearNum]) * _car._finalDrive * _car._vertical;
        }
        
    }
    
    private void SetEngineLerp(float num)
    {
        _engineLerp = true;
        _car._engineLerpValue = num;
    }

    private void LerpEngine(float velocity)
    {
        if (_engineLerp)
        {
            //_car._engineRPM = Mathf.SmoothDamp(_car._engineRPM, _car._engineLerpValue, ref velocity, _car.lerpSmoothTime * Time.deltaTime);
            _car._engineRPM = Mathf.Lerp(_car._engineRPM, _car._engineLerpValue,  _car.lerpSmoothTime * Time.deltaTime);
            _engineLerp = _car._engineRPM <= _car._engineLerpValue + 100 ? false : true;
        }
    }
    
    
}
