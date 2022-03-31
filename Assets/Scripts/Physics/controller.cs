using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    internal enum driveType
    {
        FWD,
        RWD,
        AWD
    };
    [SerializeField] private driveType drive;
    
    [Header("Variables")]
    public float totalPower = 0.0f;
    public float finalDrive = 0.0f;
    public AnimationCurve engineTorque;
    public int gearNum;

    public float[] gears = { };


    private float wheelRPM = 0.0f;
    private float velocity = 0.0f;
    private float engineRPM = 1000.0f;
    public float smoothTime = 0.2f;
    private float vertical = 0.0f;

    private void calculateEnginePower()
    {

        /* TODO: Wheel RPM */

        engineRPM = Mathf.SmoothDamp(engineRPM, 1000 + (Mathf.Abs(wheelRPM) * finalDrive * gears[gearNum]), ref velocity, smoothTime * Time.deltaTime);
        totalPower = engineTorque.Evaluate(engineRPM) * (gears[gearNum]) * finalDrive * vertical;
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
