using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Axle
{
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public float force;
}