using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private static float GetCompressionRatio(WheelCollider WheelL)
    {
        WheelHit hit;
        bool groundedL = WheelL.GetGroundHit(out hit);
        if (groundedL)
            return 1 - ((-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance);
        return 0;
    }

    private static WheelHit GetHit(WheelCollider WheelL)
    {
        WheelHit hit;
        bool groundedL = WheelL.GetGroundHit(out hit);
        return hit;
    }
}


