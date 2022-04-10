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

        private Vector2 _direction;
        private float _maxSpeed = 50; // m/s
        private void Awake()
        {
            inputReader.SetInput();
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
            if (Mathf.Approximately(_direction.y, 0))
            {
                //if there is no move forward input - apply the brake so the car can slowly lose speed 
                wheelsController.MoveWheels(0);
                wheelsController.ApplyBrake();
            }
            else if (rb.velocity.magnitude <= _maxSpeed)
            {
                //if max speed not achieved - set motor torque
                wheelsController.MoveWheels(_direction.y);
            }
            else
            {
                wheelsController.MoveWheels(0);
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