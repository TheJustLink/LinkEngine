using System;
using System.Numerics;

namespace LinkEngine.Math
{
    public static class QuaternionExtensions
    {
        public static Vector3 ToEulerInDegrees(this Quaternion quaternion) => ToEulerInRadians(quaternion) * MathFunctions.RadToDeg;
        public static Vector3 ToEulerInRadians(this Quaternion quaternion)
        {
            var sqw = quaternion.W * quaternion.W;
            var sqx = quaternion.X * quaternion.X;
            var sqy = quaternion.Y * quaternion.Y;
            var sqz = quaternion.Z * quaternion.Z;
            
            var unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
            var test = quaternion.X * quaternion.W - quaternion.Y * quaternion.Z;
            
            // singularity at north pole
            if (test > 0.4995f * unit)
            {
                return MathFunctions.NormalizeAnglesInRadians(new Vector3(
                    MathFunctions.PIHalf,
                    2f * MathF.Atan2(quaternion.Y, quaternion.X),
                    0f
                ));
            }

            // singularity at south pole
            if (test < -0.4995f * unit)
            {
                return MathFunctions.NormalizeAnglesInRadians(new Vector3(
                    -MathFunctions.PIHalf,
                    -2f * MathF.Atan2(quaternion.Y, quaternion.X),
                    0f
                ));
            }
            
            var q = new Quaternion(quaternion.W, quaternion.Z, quaternion.X, quaternion.Y);

            return MathFunctions.NormalizeAnglesInRadians(new Vector3(
                // Yaw
                MathF.Asin(2f * (q.X * q.Z - q.W * q.Y)),
                // Pitch
                MathF.Atan2(2f * q.X * q.W + 2f * q.Y * q.Z, 1f - 2f * (q.Z * q.Z + q.W * q.W)),
                // Roll
                MathF.Atan2(2f * q.X * q.Y + 2f * q.Z * q.W, 1f - 2f * (q.Y * q.Y + q.Z * q.Z))
            ));
        }
    }
}