using System.Numerics;

namespace LinkEngine.Math
{
    public static class FloatExtensions
    {
        public static Quaternion ToQuaternionInDegrees(this float angleInDegrees, Vector3 axis) =>
            axis.ToQuaternionFromAxisInDegrees(angleInDegrees);
        
        public static Quaternion ToQuaternionFromXAxisInDegrees(this float angleInDegrees) =>
            Vector3.UnitX.ToQuaternionFromAxisInDegrees(angleInDegrees);
        public static Quaternion ToQuaternionFromYAxisInDegrees(this float angleInDegrees) =>
            Vector3.UnitY.ToQuaternionFromAxisInDegrees(angleInDegrees);
        public static Quaternion ToQuaternionFromZAxisInDegrees(this float angleInDegrees) =>
            Vector3.UnitZ.ToQuaternionFromAxisInDegrees(angleInDegrees);

        public static Quaternion ToQuaternionFromXAxisInRadians(this float angleInRadians) =>
            Vector3.UnitX.ToQuaternionFromAxisInRadians(angleInRadians);
        public static Quaternion ToQuaternionFromYAxisInRadians(this float angleInRadians) =>
            Vector3.UnitY.ToQuaternionFromAxisInRadians(angleInRadians);
        public static Quaternion ToQuaternionFromZAxisInRadians(this float angleInRadians) =>
            Vector3.UnitZ.ToQuaternionFromAxisInRadians(angleInRadians);

        public static Quaternion ToQuaternionFromAxisInDegrees(this float angleInDegrees, Vector3 axis) =>
            axis.ToQuaternionFromAxisInDegrees(angleInDegrees);
        public static Quaternion ToQuaternionFromAxisInRadians(this float angleInRadians, Vector3 axis) =>
            axis.ToQuaternionFromAxisInRadians(angleInRadians);
    }
}