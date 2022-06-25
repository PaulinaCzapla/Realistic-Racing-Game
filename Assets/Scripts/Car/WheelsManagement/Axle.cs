using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Axle class grouping together objects needed for equasions
/// </summary>
[Serializable]
public class Axle
{
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public float force;
}