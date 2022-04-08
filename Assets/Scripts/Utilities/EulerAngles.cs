using UnityEngine;

namespace Utilities
{
    public static class EulerAngles
    {
        public static Vector3 GetNewEulerAnglesZ(float newAngleZ, Vector3 currentEuler, float multiplier = -1)
        {
            Vector3 euler = Vector3.zero;

            euler.y = currentEuler.y;
            euler.x = currentEuler.x;
            euler.z = newAngleZ * multiplier;

            return euler;
        }
    }
}