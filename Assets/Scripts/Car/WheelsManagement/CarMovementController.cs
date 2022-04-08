using System;
using InputSystem;
using UnityEngine;

namespace Car.WheelsManagement
{
    public class CarMovementController : MonoBehaviour
    {
        [SerializeField] private GameplayInputReader inputReader;
        [SerializeField] private WheelsController wheelsController = new WheelsController();

        private Vector2 _direction;

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
            wheelsController.UpdateWheels();
            if (inputReader.SteerPressed)
            {
                wheelsController.RotateWheels(_direction.x);
                wheelsController.MoveWheels(_direction.y);
            }

            if(inputReader.HandBrakePressed)
                wheelsController.ApplyBrake();
            else
            {
                 wheelsController.ApplyBrake(0);
            }
        }

        private void OnSteerPressed(Vector2 arg0)
        {
            _direction = arg0;
        }

        private void OnSteerCanceledPressed(Vector2 arg0)
        {
            _direction = Vector2.zero;
        }
    }
}