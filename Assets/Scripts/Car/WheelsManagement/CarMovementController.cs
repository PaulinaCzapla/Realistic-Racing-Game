using System;
using InputSystem;
using UnityEngine;

namespace Car.WheelsManagement
{
    public class CarMovementController : MonoBehaviour
    {
        [SerializeField] private GameplayInputReader inputReader;
        [SerializeField] private WheelsController wheelsController = new WheelsController();
        [SerializeField] private Rigidbody rb;
        [SerializeField] private CarSO car;

        private EngineController engine;
        private Vector2 _direction;
        private float _maxSpeed = 50; // m/s
        private void Awake()
        {
            inputReader.SetInput();
            engine = new EngineController(car);
        }

        private void OnEnable()
        {
            inputReader.SteerEvent += OnSteerPressed;
            inputReader.SteerCanceledEvent += OnSteerCanceledPressed;
        }

        private void OnDisable()
        {
            inputReader.SteerEvent -= OnSteerPressed;
            inputReader.SteerCanceledEvent -= OnSteerCanceledPressed;
        }

        private void FixedUpdate()
        {
            //update wheels meshes rotation and position
            wheelsController.UpdateWheels();
            HandleCarAcceleration();
            HandleBrake();
            HandleWheelsRotation();
        }

        private void HandleCarAcceleration()
        {
            //Debug.Log("Text: " + car.drive);
            if (car && (car.drive == DriveType.FWD || car.drive == DriveType.AWD))
            {
                engine.CalculateEnginePower(wheelsController.Wheel0RPM, rb.velocity.magnitude);
            }
            else
            {
                engine.CalculateEnginePower(wheelsController.Wheel2RPM, rb.velocity.magnitude);
            }
            

            if (Mathf.Approximately(_direction.y, 0))
            {
                //if there is no move forward input - apply the brake so the car can slowly lose speed 
                wheelsController.MoveWheels(0,0,car.drive);
                wheelsController.ApplyBrake();
            }
            else
            {

                //if max speed not achieved - set motor torque
                wheelsController.MoveWheels(_direction.y,car._totalPower,car.drive);
            }
        }

        private void HandleBrake()
        {
            if (inputReader.HandBrakePressed)
            {
                wheelsController.ApplyBrake();
            }
            else if(!Mathf.Approximately(_direction.y,0))
            {
                //if brake not pressed and move forward - set brake force to 0
                wheelsController.ApplyBrake(0);
            }
        }

        private void HandleWheelsRotation()
        {
            if (inputReader.SteerPressed)
            {
                wheelsController.RotateWheels(_direction.x);
            }
        }
        
        private void OnSteerPressed(Vector2 arg0)
        {
            _direction = arg0;
            // direction.x and direction.y are floats between -1 and 1. For keyboard there is always -1, 0 or 1 value.
        }

        private void OnSteerCanceledPressed(Vector2 arg0)
        {
            _direction = Vector2.zero;
        }
    }
}