using System;
using InputSystem;
using Photon.Pun;
using UnityEngine;

namespace Car.WheelsManagement
{
    public class CarMovementController : MonoBehaviour
    {
        [SerializeField] private GameObject cam;
        [SerializeField] private GameplayInputReader inputReader;
        [SerializeField] private WheelsController wheelsController = new WheelsController();
        [SerializeField] private Rigidbody rb;
        [SerializeField] private CarSO car;
        [SerializeField] private PhotonView photonView;

        private EngineController engine;
        private Vector2 _inputDirection;
        private float _direction;
        private float _dirDelta = 0.05f;
        private float _maxSpeed = 50; // m/s
        private void Awake()
        {
            inputReader.SetInput();
            engine = new EngineController(car);
        }

        private void OnEnable()
        {
            if (!photonView.IsMine)
            {
                cam.SetActive(false);
            }
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
            if (photonView ? photonView.IsMine : true)
            {
                if (inputReader.SteerPressed && _inputDirection.x != 0)
                {
                    if (Mathf.Abs(_direction) < Mathf.Abs(_inputDirection.x))
                        _direction += (Mathf.Sign(_inputDirection.x)) * _dirDelta * 2;
                    else
                        _direction = _inputDirection.x;
                }
                else if (_direction != 0)
                {
                    if (Mathf.Abs(_direction) > 0.15f)
                        _direction += -1 * (Mathf.Sign(_direction)) * _dirDelta;
                    else
                        _direction = 0;
                }
                //update wheels meshes rotation and position3

                wheelsController.UpdateWheels();
                HandleCarAcceleration();
                HandleBrake();
                HandleWheelsRotation();
                HandleGearSwap();
            }
        }

        private void Update()
        {

        }

        private void ApplyDownForce()
        {
            var downForce = car._downForce.Evaluate(rb.velocity.magnitude * 3.6f);
            rb.AddForce(-Vector3.up * downForce);
        }

        private void HandleGearSwap()
        {
            if (_inputDirection.y > 0 & car._gearNum == 0)
            {
                car._gearNum = 1;
            }
            switch (car._gearType)
            {
                case GearBoxType.AUTO:
                    AutoShift();
                    break;
                case GearBoxType.HALF:
                    DemandShift();
                    break;
                case GearBoxType.MAN:
                    if (inputReader.ClutchPressed)
                    {
                        DemandShift();
                    }
                    break;
            }
        }

        private void DemandShift()
        {
            if (car._gearNum != 1 && inputReader.ShiftDownGuard)
                car._gearNum--;
            inputReader.ShiftDownGuard = false;
            if (car._gearNum != (car._gears.Length - 1) && inputReader.ShiftUpGuard)
                car._gearNum++;
            inputReader.ShiftUpGuard = false;

        }

        private void AutoShift()
        {
            if (car._engineRPM > car._maxRPM + 1000 && car._gearNum < car._gears.Length - 1)
            {
                car._gearNum++;
            }
            else if (car._engineRPM <= car._minBrakeRPM + 1000 && car._gearNum > 1)
            {
                car._gearNum--;
            }
        }

        private void HandleCarAcceleration()
        {
            //Debug.Log("Text: " + car.drive);
            if (car && (car.drive == DriveType.FWD || car.drive == DriveType.AWD))
            {
                if (_inputDirection.y < 0)
                {
                    car._gearNum = 0;
                }
                engine.CalculateEnginePower(wheelsController.Wheel0RPM, rb.velocity.magnitude, inputReader.ClutchPressed, _inputDirection.y);
            }
            else
            {
                if (_inputDirection.y < 0)
                {
                    car._gearNum = 0;
                }
                engine.CalculateEnginePower(wheelsController.Wheel2RPM, rb.velocity.magnitude, inputReader.ClutchPressed, _inputDirection.y);
            }


            if (Mathf.Approximately(_inputDirection.y, 0) && car._engineRPM <= car._minBrakeRPM && (!inputReader.ClutchPressed))
            {
                //if there is no move forward input - apply the brake so the car can slowly lose speed 
                wheelsController.MoveWheels(0, 0, car.drive);
                wheelsController.ApplyBrake();
            }
            else
            {

                //if max speed not achieved - set motor torque
                wheelsController.MoveWheels(_inputDirection.y, car._totalPower, car.drive);
            }
        }

        private void HandleBrake()
        {
            if (inputReader.HandBrakePressed)
            {
                wheelsController.ApplyBrake();
            }
            else if (!Mathf.Approximately(_inputDirection.y, 0))
            {
                //if brake not pressed and move forward - set brake force to 0
                wheelsController.ApplyBrake(0);
            }
        }

        private void HandleWheelsRotation()
        {
            float _currMaxAngle = car._maxSteerAngle.Evaluate(rb.velocity.magnitude * 3.6f);
            wheelsController.RotateWheels(_direction, _currMaxAngle);
        }

        private void OnSteerPressed(Vector2 arg0)
        {
            _inputDirection = arg0;
            // direction.x and direction.y are floats between -1 and 1. For keyboard there is always -1, 0 or 1 value.
        }

        private void OnSteerCanceledPressed(Vector2 arg0)
        {
            _inputDirection = Vector2.zero;
        }


    }
}