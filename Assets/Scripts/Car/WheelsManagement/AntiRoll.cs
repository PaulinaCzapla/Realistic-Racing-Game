using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classs handling Anti-Roll bars implementations in the fixed update
/// </summary>
public class AntiRoll : MonoBehaviour
{
    
    [SerializeField] private new Rigidbody rigidbody;
	[SerializeField] private List<Axle> axles;


	private void FixedUpdate()
    {
        foreach(var axle in axles)
        {
            var vectorDown = transform.TransformDirection(Vector3.down);
            vectorDown.Normalize();

            float travL = Mathf.Clamp01(GetCompressionRatio(axle.leftWheel));
            float travR = Mathf.Clamp01(GetCompressionRatio(axle.rightWheel));
            float rollForce = (travL - travR) * axle.force;

            if (axle.leftWheel.isGrounded)
                rigidbody.AddForceAtPosition(vectorDown * -rollForce, GetHit(axle.leftWheel).point);

                

            if (axle.rightWheel.isGrounded)
                rigidbody.AddForceAtPosition(vectorDown * rollForce, GetHit(axle.rightWheel).point);
        }
    }

    /// <summary>
    /// Determinned compression ration between suspension and wheel
    /// </summary>
    /// <param name="WheelL">
    /// WheelCollider to determine  the compression ration
    /// </param>
    /// <returns>
    /// The compression ration
    /// </returns>
    private static float GetCompressionRatio(WheelCollider WheelL)
    {
        WheelHit hit;
        bool groundedL = WheelL.GetGroundHit(out hit);
        if (groundedL)
            return 1 - ((-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance);
        return 0;
    }

    /// <summary>
    /// Determines if the wheel hit the ground and takes that point
    /// </summary>
    /// <param name="WheelL">
    /// WheelCollider to be determined
    /// </param>
    /// <returns>
    /// WheelHit object with exact point 
    /// </returns>
    private static WheelHit GetHit(WheelCollider WheelL)
    {
        WheelHit hit;
        bool groundedL = WheelL.GetGroundHit(out hit);
        return hit;
    }
}


