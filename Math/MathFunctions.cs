using System;
using System.Numerics;

namespace LinkEngine.Math
{
    public static class MathFunctions
    {
        private static volatile float s_floatMinNormal = 1.17549435E-38f;
        private static volatile float s_floatMinDenormal = float.Epsilon;
        private static readonly bool s_isFlushToZeroEnabled = s_floatMinDenormal == 0.0;

        /// <summary>
        ///   <para>A tiny floating point value (Read Only).</para>
        /// </summary>
        public static float Epsilon = s_isFlushToZeroEnabled ? s_floatMinNormal : s_floatMinDenormal;

        /// <summary>
        ///   <para>Degrees-to-radians conversion constant.</para>
        /// </summary>
        public const float DegToRad = 0.0174532924f;
        /// <summary>
        ///   <para>Radians-to-degrees conversion constant.</para>
        /// </summary>
        public const float RadToDeg = 57.29578f;

        public const float Deg360InRad = (float)(System.Math.PI * 2.0);
        public const float Deg180InRad = (float)System.Math.PI;
        public const float Deg90InRad = PIHalf;
        public const float Deg45InRad = (float)(System.Math.PI * 0.25);
        public const float Deg30InRad = (float)(System.Math.PI * 0.16666666666666666);
        public const float Deg15InRad = (float)(System.Math.PI * 0.083333333333333329);

        /// <summary>
        /// Represents the log base ten of e(0.4342945).
        /// </summary>
        public const float Log10E = 0.4342945f;

        /// <summary>
        /// Represents the log base two of e(1.442695).
        /// </summary>
        public const float Log2E = 1.442695f;

        /// <summary>
        /// Represents the value of pi divided by two(1.57079637).
        /// </summary>
        public const float PIHalf = (float)(System.Math.PI * 0.5);

        /// <summary>
        /// Represents the value of pi divided by four(0.7853982).
        /// </summary>
        public const float PIOver4 = (float)(System.Math.PI * 0.25);
        /// <summary>
        /// Represents the value of pi times two(6.28318548).
        /// </summary>
        public const float TwoPI = (float)(System.Math.PI * 2.0);

        /// <summary>
        /// Represents the value of pi times two(6.28318548).
        /// This is an alias of TwoPi.
        /// </summary>
        public const float Tau = TwoPI;

        private const double RadiansFactor = 0.017453292519943295769236907684886;
        private const double ToDegreesFactor = 57.295779513082320876798154814105;

        /// <summary>
        /// Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized barycentric (areal) coordinates.
        /// </summary>
        /// <param name="value1">The coordinate on one axis of vertex 1 of the defining triangle.</param>
        /// <param name="value2">The coordinate on the same axis of vertex 2 of the defining triangle.</param>
        /// <param name="value3">The coordinate on the same axis of vertex 3 of the defining triangle.</param>
        /// <param name="amount1">The normalized barycentric (areal) coordinate b2, equal to the weighting factor for vertex 2, the coordinate of which is specified in value2.</param>
        /// <param name="amount2">The normalized barycentric (areal) coordinate b3, equal to the weighting factor for vertex 3, the coordinate of which is specified in value3.</param>
        /// <returns>Cartesian coordinate of the specified point with respect to the axis being used.</returns>
        public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
        {
            return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
        }

        /// <summary>
        /// Calculates the absolute value of the difference of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>Distance between the two values.</returns>
        public static float Distance(float value1, float value2) => System.Math.Abs(value1 - value2);
        
        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>The angle in degrees.</returns>
        /// <remarks>
        /// This method uses double precission internally,
        /// though it returns single float
        /// Factor = 180 / pi
        /// </remarks>
        public static float ToDegrees(float radians) => (float)(radians * ToDegreesFactor);

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>The angle in radians.</returns>
        /// <remarks>
        /// This method uses double precission internally,
        /// though it returns single float
        /// Factor = pi / 180
        /// </remarks>
        public static float ToRadians(float degrees) => (float)(degrees * RadiansFactor);

        /// <summary>
        /// Reduces a given angle to a value between π and -π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
        public static float WrapAngle(float angle)
        {
            if (angle is > -MathF.PI and <= MathF.PI)
                return angle;
            
            angle %= TwoPI;

            return angle switch
            {
                <= -MathF.PI => angle + TwoPI,
                > MathF.PI => angle - TwoPI,
                _ => angle
            };
        }

        /// <summary>
        /// Determines if value is powered by two.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <returns><c>true</c> if <c>value</c> is powered by two; otherwise <c>false</c>.</returns>
        public static bool IsPowerOfTwo(int value)
        {
            return value > 0 && (value & (value - 1)) == 0;
        }

        public static float CopySign(float x, float y)
        {
            // This method is required to work for all inputs,
            // including NaN, so we operate on the raw bits.
            
            var xbits = BitConverter.SingleToInt32Bits(x);
            var ybits = BitConverter.SingleToInt32Bits(y);

            // If the sign bits of x and y are not the same,
            // flip the sign bit of x and return the new value;
            // otherwise, just return x

            if ((xbits ^ ybits) < 0)
            {
                return BitConverter.Int32BitsToSingle(xbits ^ int.MinValue);
            }

            return x;
        }
        /// <summary>
        ///   <para>Returns the sign of f.</para>
        /// </summary>
        /// <param name="value"></param>
        public static float Sign(float value) => value >= 0f ? 1f : -1f;

        public static float Clamp01(float value)
        {
            return value < 0f
                ? 0f
                : value > 1f
                    ? 1f
                    : value;
        }

        /// <summary>
        ///   <para>Loops the value, so that it is never larger than length and never smaller than 0.</para>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="loopLength"></param>
        public static float Repeat(float value, float loopLength)
        {
            return System.Math.Clamp(value - MathF.Floor(value / loopLength) * loopLength, 0f, loopLength);
        }

        /// <summary>
        ///   <para>PingPong returns a value that will increment and decrement between the value 0 and length.</para>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        public static float PingPong(float value, float length)
        {
            value = Repeat(value, length * 2f);
            return length - System.Math.Abs(value - length);
        }

        /// <summary>
        ///   <para>Calculates the shortest difference between two given angles given in degrees.</para>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        public static float DeltaAngleInDegrees(float current, float target)
        {
            var num = Repeat(target - current, 360f);
            
            if (num > 180f)
                num -= 360f;
            
            return num;
        }
        /// <summary>
        ///   <para>Calculates the shortest difference between two given angles given in radians.</para>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        public static float DeltaAngleInRadians(float current, float target)
        {
            var num = Repeat(target - current, Deg360InRad);

            if (num > Deg180InRad)
                num -= Deg360InRad;

            return num;
        }
        
        public static Vector3 NormalizeAnglesInDegrees(Vector3 angles)
        {
            return new Vector3(
                NormalizeAngleInDegrees(angles.X),
                NormalizeAngleInDegrees(angles.Y),
                NormalizeAngleInDegrees(angles.Z)
            );
        }
        public static float NormalizeAngleInDegrees(float angle)
        {
            var modAngle = angle % 360f;

            return modAngle < 0f
                ? modAngle + 360f
                : modAngle;
        }
        public static Vector3 NormalizeAnglesInRadians(Vector3 angles)
        {
            return new Vector3(
                NormalizeAngleInRadians(angles.X),
                NormalizeAngleInRadians(angles.Y),
                NormalizeAngleInRadians(angles.Z)
            );
        }
        public static float NormalizeAngleInRadians(float angle)
        {
            var modAngle = angle % Deg360InRad;

            return modAngle < 0f
                ? modAngle + Deg360InRad
                : modAngle;
        }

        /// <summary>
        ///   <para>Returns the smallest of two or more values.</para>
        /// </summary>
        /// <param name="values"></param>
        public static float Min(params float[] values)
        {
            var length = values.Length;
            
            if (length == 0)
                return 0.0f;

            var num = values[0];

            for (var index = 1; index < length; ++index)
            {
                if (values[index] < num)
                    num = values[index];
            }

            return num;
        }

        /// <summary>
        ///   <para>Returns the smallest of two or more values.</para>
        /// </summary>
        /// <param name="values"></param>
        public static int Min(params int[] values)
        {
            var length = values.Length;

            if (length == 0)
                return 0;

            var num = values[0];
            
            for (var index = 1; index < length; ++index)
            {
                if (values[index] < num)
                    num = values[index];
            }

            return num;
        }

        /// <summary>
        ///   <para>Returns largest of two or more values.</para>
        /// </summary>
        /// <param name="values"></param>
        public static float Max(params float[] values)
        {
            var length = values.Length;
            
            if (length == 0)
                return 0.0f;
            
            var num = values[0];

            for (var index = 1; index < length; ++index)
            {
                if (values[index] > num)
                    num = values[index];
            }
            
            return num;
        }

        /// <summary>
        ///   <para>Returns the largest of two or more values.</para>
        /// </summary>
        /// <param name="values"></param>
        public static int Max(params int[] values)
        {
            var length = values.Length;

            if (length == 0)
                return 0;

            var num = values[0];

            for (var index = 1; index < length; ++index)
            {
                if (values[index] > num)
                    num = values[index];
            }

            return num;
        }

        public static float Gamma(float value, float absmax, float gamma)
        {
            var isValueLessThenZero = value < 0f;
            
            var num1 = System.Math.Abs(value);
            
            if (num1 > absmax)
                return isValueLessThenZero ? -num1 : num1;

            var num2 = MathF.Pow(num1 / absmax, gamma) * absmax;
            
            return isValueLessThenZero ? -num2 : num2;
        }

        /// <summary>
        ///   <para>Compares two floating point values and returns true if they are similar.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool Approximately(float a, float b)
        {
            return System.Math.Abs(b - a) <
                   System.Math.Max(1E-06f * System.Math.Max(System.Math.Abs(a), System.Math.Abs(b)), Epsilon * 8f);
        }

        public static bool LineIntersection(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4, ref Vector2 result)
        {
            var num1 = point2.X - point1.X;
            var num2 = point2.Y - point1.Y;
            var num3 = point4.X - point3.X;
            var num4 = point4.Y - point3.Y;
            
            var num5 = num1 * num4 - num2 * num3;

            if (num5 == 0f)
                return false;

            var num6 = point3.X - point1.X;
            var num7 = point3.Y - point1.Y;

            var num8 = (num6 * num4 - num7 * num3) / num5;
            
            result.X = point1.X + num8 * num1;
            result.Y = point1.Y + num8 * num2;
            
            return true;
        }
        public static bool LineSegmentIntersection(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4, ref Vector2 result)
        {
            var num1 = point2.X - point1.X;
            var num2 = point2.Y - point1.Y;
            var num3 = point4.X - point3.X;
            var num4 = point4.Y - point3.Y;
            
            var num5 = num1 * num4 - num2 * num3;
            
            if (num5 == 0f)
                return false;

            var num6 = point3.X - point1.X;
            var num7 = point3.Y - point1.Y;
            
            var num8 = (num6 * num4 - num7 * num3) / num5;
            
            if (num8 is < 0f or > 1f)
                return false;

            var num9 = (num6 * num2 - num7 * num1) / num5;
            
            if (num9 is < 0f or > 1f)
                return false;
            
            result.X = point1.X + num8 * num1;
            result.Y = point1.Y + num8 * num2;

            return true;
        }
    }
}