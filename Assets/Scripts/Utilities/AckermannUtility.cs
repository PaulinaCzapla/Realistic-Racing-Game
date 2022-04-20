using UnityEngine;

namespace Utilities
{
    public static class AckermannUtility
    {
        public static float GetSecondaryAngle(float primaryAngle, float separation, float width) 
        {
            if (Mathf.Abs(primaryAngle) < 1)
                primaryAngle = Mathf.Abs(primaryAngle);
            float close = separation / Mathf.Tan(Mathf.Abs(primaryAngle) * Mathf.Deg2Rad);
            float far = close + width;
            return Mathf.Sign(primaryAngle) * Mathf.Atan(separation / far) * Mathf.Rad2Deg;
        }
    }
}