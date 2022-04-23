using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (_car._engineRPM >= _car._maxRPM) SetEngineLerp(_car._maxRPM - 1000);
        if (!_engineLerp)
        {
            _car._engineRPM =  Mathf.SmoothDamp(_car._engineRPM, _car._turnOnRPM + (Mathf.Abs(wheelRPM) * _car._finalDrive * _car._gears[_car._gearNum]), ref vel, _car._smoothTime * Time.deltaTime) ;
            _car._totalPower = (float)(clutch ? 0.0 :  _car._engineTorque.Evaluate(_car._engineRPM) * (_car._gears[_car._gearNum]) * _car._finalDrive * (vertical+0.000000001));
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
            //_car._engineRPM = Mathf.SmoothDamp(_car._engineRPM, _car._engineLerpValue, ref vel, _car.lerpSmoothTime * Time.deltaTime);
           _car._engineRPM = Mathf.Lerp(_car._engineRPM, _car._engineLerpValue,  _car.lerpSmoothTime * Time.deltaTime);
            _engineLerp = _car._engineRPM <= _car._engineLerpValue + 100 ? false : true;
        }
    }
    
    
}
