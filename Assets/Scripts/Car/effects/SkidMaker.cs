using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skid marker class for making easier handling functions in the trigger grouping emmision object and wheelCollider together
/// </summary>
[Serializable]
public class SkidMaker
{
    public Transform emiisionObject;
    public WheelCollider wheel;
}
