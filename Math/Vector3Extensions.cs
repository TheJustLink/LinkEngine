using System;
using System.Numerics;

namespace LinkEngine.Math
{
    public static class Vector3Extensions
    {
        public static Quaternion ToQuaternionFromDegrees(this Vector3 angles)
        {
            return ToQuaternionFromRadians(angles * MathFunctions.DegToRad);
        }
        public static Quaternion ToQuaternionFromRadians(this Vector3 anglesInRadians)
        {
            // Order of rotations: Z first, then X, then Y (mimics typical FPS camera with gimbal lock at top/bottom)
            var halfAngles = anglesInRadians * 0.5f;

            var sinX = MathF.Sin(halfAngles.X);
            var cosX = MathF.Cos(halfAngles.X);
            var sinY = MathF.Sin(halfAngles.Y);
            var cosY = MathF.Cos(halfAngles.Y);
            var sinZ = MathF.Sin(halfAngles.Z);
            var cosZ = MathF.Cos(halfAngles.Z);

            return new Quaternion(
                cosY * sinX * cosZ + sinY * cosX * sinZ,
                sinY * cosX * cosZ - cosY * sinX * sinZ,
                cosY * cosX * sinZ - sinY * sinX * cosZ,
                cosY * cosX * cosZ + sinY * sinX * sinZ
            );
        }

        public static Quaternion ToQuaternionFromAxisInDegrees(this Vector3 axis, float angleInDegrees)
        {
            return ToQuaternionFromAxisInRadians(axis, angleInDegrees * MathFunctions.DegToRad);
        }
        public static Quaternion ToQuaternionFromAxisInRadians(this Vector3 axis, float angleInRadians)
        {
            var halfAngle = angleInRadians * 0.5f;
            var s = MathF.Sin(halfAngle);
            var c = MathF.Cos(halfAngle);

            return new Quaternion(
                axis.X * s,
                axis.Y * s,
                axis.Z * s,
                c
            );
        }
    }
}