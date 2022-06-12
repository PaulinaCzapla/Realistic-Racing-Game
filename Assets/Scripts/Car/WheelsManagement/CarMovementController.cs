using System;
using InputSystem;
using Network;
using Photon.Pun;
using RaceManagement;
using UnityEngine;
using UnityEngine.InputSystem;

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
        //[SerializeField] private RaceParticipant raceParticipant;
        [SerializeField] private DisconnectPlayer disconnectPlayer;
        //[SerializeField] private PlayerInput input;

        private EngineController _engine;
        private Vector2 _inputDirection;
        private float _direction;
        private float _dirDelta = 0.05f;
        private float _maxSpeed = 50; // m/s

        private void Awake()
        {
           // inputReader.SetInput();
            _engine = new EngineController(car);
        }

        private void OnEnable()
        {
            if (!photonView.IsMine)
            {
                cam.SetActive(false);
            }

            inputReader.SteerEvent += OnSteerPressed;
            inputReader.SteerCanceledEvent += OnSteerCanceledPressed;
            inputReader.GasEvent += OnGasPressed;
            inputReader.GasCanceledEvent += OnGasCanceled;
            inputReader.ReverseEvent += OnReversePressed;
            inputReader.ReverseCanceledEvent += OnReverseCanceled;
            // inputReader.HandBrakePressed += OnHandBrakePressed;
        }

        private void OnDisable()
        {
            inputReader.SteerEvent -= OnSteerPressed;
            inputReader.SteerCanceledEvent -= OnSteerCanceledPressed;
            inputReader.GasEvent -= OnGasPressed;
            inputReader.GasCanceledEvent -= OnGasCanceled;
            inputReader.ReverseEvent -= OnReversePressed;
            inputReader.ReverseCanceledEvent -= OnReverseCanceled;
        }

        private void FixedUpdate()
        {
            if (photonView ? photonView.IsMine : true)
            {
                car.carSpeed = rb.velocity.magnitude *3.6f;
                HandleSmoothSteering();
                //update wheels meshes rotation and position3
                wheelsController.UpdateWheels();
                HandleCarAcceleration();
                HandleBrake();
                HandleWheelsRotation();
                HandleGearSwap();
            }
        }

        private void OnGasCanceled() => _inputDirection = new Vector2(_inputDirection.x, 0);

        private void OnGasPressed() => _inputDirection = new Vector2(_inputDirection.x, 1);
        
        private void OnReversePressed() => _inputDirection = new Vector2(_inputDirection.x, -1);

        private void OnReverseCanceled() => _inputDirection = new Vector2(_inputDirection.x, 0);
        
        private void OnSteerPressed(Vector2 arg0) => _inputDirection = new Vector2(arg0.x, _inputDirection.y);
        
        private void OnSteerCanceledPressed(Vector2 arg0)=> _inputDirection = _inputDirection = new Vector2(0, _inputDirection.y);
        
        private void ApplyDownForce()
        {
            var downForce = car._downForce.Evaluate(rb.velocity.magnitude * 3.6f);
            rb.AddForce(-Vector3.up * downForce);
        }

        private void HandleSmoothSteering()
        {
            //if (input.currentControlScheme.Equals("Keyboard&Mouse") || input.currentControlScheme.Equals("Gamepad"))
            //{
                if (inputReader.SteerPressed && _inputDirection.x != 0)
                {
                    if (Mathf.Abs(_direction) < Mathf.Abs(_inputDirection.x))
                        _direction += (Mathf.Sign(_inputDirection.x)) * _dirDelta * 2;
                    else
                        _direction = _inputDirection.x;
                }
                else if (_direction != 0)
                {
                    if (Mathf.Abs(_direction) > 0.2f)
                        _direction += -1 * (Mathf.Sign(_direction)) * _dirDelta;
                    else
                        _direction = 0;
                }
                else
                {
                    _direction = _inputDirection.x;
                }

                if (inputReader.GasPressed)
                {
                    disconnectPlayer.timeSinceNoInput = 0f;
                    disconnectPlayer.countdownStarted = false;
                    disconnectPlayer._timeToDisconnecting = 45f;
                }
            //}
            //else
            //{
                //_direction = _inputDirection.x;
            //}
        }

        private void HandleGearSwap()
        {
            if (_inputDirection.y > 0 & car.gearNum == 0)
            {
                car.gearNum = 1;
            }

            switch (car.gearType)
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
            if (car.gearNum != 1 && inputReader.ShiftDownGuard)
                car.gearNum--;
            inputReader.ShiftDownGuard = false;
            if (car.gearNum != (car.gears.Length - 1) && inputReader.ShiftUpGuard)
                car.gearNum++;
            inputReader.ShiftUpGuard = false;
        }

        private void AutoShift()
        {
            if (car.engineRpm > car.MAXRpm + 1000 && car.gearNum < car.gears.Length - 1)
            {
                car.gearNum++;
            }
            else if (car.engineRpm <= car.minBrakeRpm + 1000 && car.gearNum > 1)
            {
                car.gearNum--;
            }
        }

        private void HandleCarAcceleration()
        {
            //Debug.Log("Text: " + car.drive);
            if (car && (car.drive == DriveType.FWD || car.drive == DriveType.AWD))
            {
                if (_inputDirection.y < 0)
                {
                    car.gearNum = 0;
                }

                _engine.CalculateEnginePower(wheelsController.Wheel0RPM, rb.velocity.magnitude,
                    inputReader.ClutchPressed, _inputDirection.y);
            }
            else
            {
                if (_inputDirection.y < 0)
                {
                    car.gearNum = 0;
                }

                _engine.CalculateEnginePower(wheelsController.Wheel2RPM, rb.velocity.magnitude,
                    inputReader.ClutchPressed, _inputDirection.y);
            }


            if (Mathf.Approximately(_inputDirection.y, 0) && car.engineRpm <= car.minBrakeRpm &&
                (!inputReader.ClutchPressed))
            {
                //if there is no move forward input - apply the brake so the car can slowly lose speed 
                wheelsController.MoveWheels(0, 0, car.drive);
                wheelsController.ApplyBrake();
            }
            else
            {
                //if max speed not achieved - set motor torque
                wheelsController.MoveWheels(_inputDirection.y, car.totalPower, car.drive);
            }
        }

        private void HandleBrake()
        {
            // if (inputReader.BrakePressed)
            // {
            //    // wheelsController.ApplyBrake(6000);
            // }
            // else
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
    }
}