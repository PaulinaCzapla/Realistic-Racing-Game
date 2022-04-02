using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Car _car;

    private void CalculateEnginePower()
    {

        /* TODO: Wheel RPM */

        _car._engineRPM = Mathf.SmoothDamp(_car._engineRPM, _car._TurnOnRPM + (Mathf.Abs(_car._wheelRPM) * _car._finalDrive * _car._gears[_car._gearNum]), ref _car._velocity, _car._smoothTime * Time.deltaTime);
        _car._totalPower = _car._engineTorque.Evaluate(_car._engineRPM) * (_car._gears[_car._gearNum]) * _car._finalDrive * _car._vertical;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
