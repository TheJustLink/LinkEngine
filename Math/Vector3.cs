using System;
using System.Numerics;

namespace LinkEngine.Math
{
    [Serializable]
    public readonly struct Vector3 : IEquatable<Vector3>
    {
        /// <summary>
        /// Defines an instance with all components set to positive infinity.
        /// </summary>
        public static readonly Vector3 PositiveInfinity = new(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        /// <summary>
        /// Defines an instance with all components set to negative infinity.
        /// </summary>
        public static readonly Vector3 NegativeInfinity = new(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

        /// <summary>
        /// Returns the vector (0,0,0).
        /// </summary>
        public static readonly Vector3 Zero = new();
        /// <summary>
        /// Returns the vector (1,1,1).
        /// </summary>
        public static readonly Vector3 One = new(1f, 1f, 1f);
        /// <summary>
        /// Returns the vector (-1,-1,-1).
        /// </summary>
        public static readonly Vector3 Negative = new(-1f, -1f, -1f);
        /// <summary>
        /// Returns the vector (0.5,0.5,0.5).
        /// </summary>
        public static readonly Vector3 Half = new(0.5f, 0.5f, 0.5f);
        /// <summary>
        /// Returns the vector (2,2,2).
        /// </summary>
        public static readonly Vector3 Two = new(2f, 2f, 2f);

        /// <summary>
        /// Returns the vector (1,0,0).
        /// </summary>
        public static readonly Vector3 UnitX = new(1f, 0f, 0f);
        /// <summary>
        /// Returns the vector (0,1,0).
        /// </summary>
        public static readonly Vector3 UnitY = new(0f, 1f, 0f);
        /// <summary>
        /// Returns the vector (0,0,1).
        /// </summary>
        public static readonly Vector3 UnitZ = new(0f, 0f, 1f);

        /// <summary>
        /// Returns the vector (1,0,0).
        /// </summary>
        public static readonly Vector3 Right = new(1f, 0f, 0f);
        /// <summary>
        /// Returns the vector (-1,0,0).
        /// </summary>
        public static readonly Vector3 Left = new(-1f, 0f, 0f);
        /// <summary>
        /// Returns the vector (0,1,0).
        /// </summary>
        public static readonly Vector3 Up = new(0f, 1f, 0f);
        /// <summary>
        /// Returns the vector (0,-1,0).
        /// </summary>
        public static readonly Vector3 Down = new(0f, -1f, 0f);
        /// <summary>
        /// Returns the vector (0,0,1).
        /// </summary>
        public static readonly Vector3 Forward = new(0f, 0f, 1f);
        /// <summary>
        /// Returns the vector (0,0,-1).
        /// </summary>
        public static readonly Vector3 Backward = new(0f, 0f, -1f);
        
        /// <summary>
        /// Returns the vector (1,1,1).
        /// </summary>
        public static readonly Vector3 RightUpForward = One;
        /// <summary>
        /// Returns the vector (1,1,1).
        /// </summary>
        public static readonly Vector3 RightUpBackward = Right + Up + Backward;
        
        /// <summary>
        /// Returns the vector (1,-1,1).
        /// </summary>
        public static readonly Vector3 RightDownForward = Right + Down + Forward;
        /// <summary>
        /// Returns the vector (1,-1,-1).
        /// </summary>
        public static readonly Vector3 RightDownBackward = Right + Down + Backward;
        
        /// <summary>
        /// Returns the vector (-1,1,1).
        /// </summary>
        public static readonly Vector3 LeftUpForward = Left + Up + Forward;
        /// <summary>
        /// Returns the vector (-1,1,-1).
        /// </summary>
        public static readonly Vector3 LeftUpBackward = Left + Up + Backward;
        
        /// <summary>
        /// Returns the vector (-1,-1,1).
        /// </summary>
        public static readonly Vector3 LeftDownForward = Left + Down + Forward;
        /// <summary>
        /// Returns the vector (-1,-1,-1).
        /// </summary>
        public static readonly Vector3 LeftDownBackward = Negative;

        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public readonly float X;
        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public readonly float Y;
        /// <summary>
        /// The Z component of the vector.
        /// </summary>
        public readonly float Z;

        /// <summary>
        /// Constructs a vector whose elements are all the single specified value.
        /// </summary>
        /// <param name="value">The element to fill the vector with.</param>
        public Vector3(float value) : this(value, value, value) { }
        /// <summary>
        /// Constructs a vector with the given individual elements.
        /// </summary>
        /// <param name="x">The X component.</param>
        /// <param name="y">The Y component.</param>
        /// <param name="z">The Y component.</param>
        public Vector3(float x, float y, float z) => (X, Y, Z) = (x, y, z);
        /// <summary>
        /// Constructs a Vector3 from the given Vector2 and a third value.
        /// </summary>
        /// <param name="value">The Vector to extract X and Y components from.</param>
        /// <param name="z">The Z component.</param>
        public Vector3(Vector2 value, float z) : this(value.X, value.Y, z) { }

        /// <summary>
        /// Gets or sets the value at the index of the Vector. Index can be high then 2, than it will be wrapped.
        /// </summary>
        /// <param name="index">The index of the component from the Vector.</param>
        public float this[int index] => (index % 3) switch
        {
            0 => X,
            1 => Y,
            2 => Z
        };

        /// <summary>
        /// Get a first component of vector (X).
        /// </summary>
        public float Component1 => X;
        /// <summary>
        /// Get a second component of vector (Y).
        /// </summary>
        public float Component2 => Y;
        /// <summary>
        /// Get a third component of vector (Z).
        /// </summary>
        public float Component3 => Z;

        /// <summary>
        /// Get a Vector2 with the X and Y components of this instance.
        /// </summary>
        public Vector2 XY => new(X, Y);
        /// <summary>
        /// Get a Vector2 with the X and Z components of this instance.
        /// </summary>
        public Vector2 XZ => new(X, Z);
        /// <summary>
        /// Get a Vector2 with the Y and X components of this instance.
        /// </summary>
        public Vector2 YX => new(Y, X);
        /// <summary>
        /// Get a Vector2 with the Y and Z components of this instance.
        /// </summary>
        public Vector2 YZ => new(Y, Z);
        /// <summary>
        /// Get a Vector2 with the Z and X components of this instance.
        /// </summary>
        public Vector2 ZX => new(Z, X);
        /// <summary>
        /// Get a Vector2 with the Z and Y components of this instance.
        /// </summary>
        public Vector2 ZY => new(Z, Y);

        /// <summary>
        /// Get a Vector3 with the X, Z, and Y components of this instance.
        /// </summary>
        public Vector3 XZY => new(X, Z, Y);
        /// <summary>
        /// Get a Vector3 with the Y, X, and Z components of this instance.
        /// </summary>
        public Vector3 YXZ => new(Y, X, Z);
        /// <summary>
        /// Get a Vector3 with the Y, Z, and X components of this instance.
        /// </summary>
        public Vector3 YZX => new(Y, Z, X);
        /// <summary>
        /// Get a Vector3 with the Z, X, and Y components of this instance.
        /// </summary>
        public Vector3 ZXY => new(Z, X, Y);
        /// <summary>
        /// Get a Vector3 with the Z, Y, and X components of this instance.
        /// </summary>
        public Vector3 ZYX => new(Z, Y, X);
        
        /// <summary>
        /// Returns the Vector2 by Vector3.
        /// </summary>
        /// <returns>The Vector2.</returns>
        public Vector2 Vector2 => this;
        
        /// <summary>
        /// Returns the length of the vector.
        /// </summary>
        /// <returns>The vector's length.</returns>
        public float Length => MathF.Sqrt(LengthSquared);
        /// <summary>
        /// Returns the length of the vector.
        /// </summary>
        /// <returns>The vector's length.</returns>
        public float LengthFast => MathFunctions.InverseSqrtFast(LengthSquared);
        /// <summary>
        /// Returns the length of the vector squared. This operation is cheaper than Length.
        /// </summary>
        /// <returns>The vector's length squared.</returns>
        public float LengthSquared => Vector.IsHardwareAccelerated ? Dot(this, this) : X * X + Y * Y + Z * Z;

        /// <summary>
        /// Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <returns>The square root vector.</returns>
        public Vector3 SquareRooted => new(MathF.Sqrt(X), MathF.Sqrt(Y), MathF.Sqrt(Z));
        /// <summary>
        /// Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <returns>The square root vector.</returns>
        public Vector3 SquareRootedFast => new(MathFunctions.InverseSqrtFast(X), MathFunctions.InverseSqrtFast(Y), MathFunctions.InverseSqrtFast(Z));
        /// <summary>
        /// Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <returns>The normalized vector.</returns>
        public Vector3 Normalized => this / Length;
        /// <summary>
        /// Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <returns>The normalized vector.</returns>
        public Vector3 NormalizedFast => this / LengthFast;
        /// <summary>
        /// Returns a direction vector (normalized vector).
        /// </summary>
        /// <returns>The direction vector.</returns>
        public Vector3 Direction => Normalized;
        /// <summary>
        /// Returns a direction vector (normalized vector).
        /// </summary>
        /// <returns>The direction vector.</returns>
        public Vector3 DirectionFast => NormalizedFast;

        /// <summary>
        /// Returns the point between the two given points.
        /// </summary>
        /// <param name="targetPoint">The target point.</param>
        /// <returns>The point.</returns>
        public Vector3 GetVectorTo(Vector3 targetPoint)
        {
            return targetPoint - this;
        }
        /// <summary>
        /// Returns the direction point between the two given points.
        /// </summary>
        /// <param name="targetPoint">The target point.</param>
        /// <returns>The direction point.</returns>
        public Vector3 GetDirectionTo(Vector3 targetPoint)
        {
            return GetVectorTo(targetPoint).Normalized;
        }
        /// <summary>
        /// Returns the direction point between the two given points.
        /// </summary>
        /// <param name="targetPoint">The target point.</param>
        /// <returns>The direction point.</returns>
        public Vector3 GetDirectionFastTo(Vector3 targetPoint)
        {
            return GetVectorTo(targetPoint).NormalizedFast;
        }

        /// <summary>
        /// Returns the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="targetPoint">The target point.</param>
        /// <returns>The distance.</returns>
        public float GetDistanceTo(Vector3 targetPoint)
        {
            return GetVectorTo(targetPoint).Length;
        }
        /// <summary>
        /// Returns the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="targetPoint">The target point.</param>
        /// <returns>The distance.</returns>
        public float GetDistanceFastTo(Vector3 targetPoint)
        {
            return GetVectorTo(targetPoint).LengthFast;
        }
        /// <summary>
        /// Returns the Euclidean distance squared between the two given points.
        /// </summary>
        /// <param name="targetPoint">The target point.</param>
        /// <returns>The distance squared.</returns>
        public float GetDistanceSquaredTo(Vector3 targetPoint)
        {
            return GetVectorTo(targetPoint).LengthSquared;
        }

        /// <summary>
        /// Returns the reflection of a vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="normal">The normal of the surface being reflected off.</param>
        /// <returns>The reflected vector.</returns>
        public Vector3 GetReflectionBy(Vector3 normal)
        {
            return this - Two * Dot(this, normal) * normal;
        }

        public Vector3 WithX(float x) => new(x, Y, Z);
        public Vector3 WithY(float y) => new(X, y, Z);
        public Vector3 WithZ(float z) => new(X, Y, z);

        public Vector3 WithXY(float x, float y) => new(x, y, Z);
        public Vector3 WithXZ(float x, float z) => new(x, Y, z);
        public Vector3 WithYZ(float y, float z) => new(X, y, z);

        public Vector3 WithXY(Vector2 xy) => new(xy.Component1, xy.Component2, Z);
        public Vector3 WithXZ(Vector2 xz) => new(xz.Component1, Y, xz.Component2);
        public Vector3 WithYZ(Vector2 yz) => new(X, yz.Component1, yz.Component2);

        public Quaternion ToQuaternionFromDegrees()
        {
            return (this * MathFunctions.DegToRad3).ToQuaternionFromRadians();
        }
        public Quaternion ToQuaternionFromRadians()
        {
            // Order of rotations: Z first, then X, then Y (mimics typical FPS camera with gimbal lock at top/bottom)
            var halfAngles = this * Half;

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
        public Quaternion ToQuaternionFromAxisInDegrees(float angleInDegrees)
        {
            return ToQuaternionFromAxisInRadians(angleInDegrees * MathFunctions.DegToRad);
        }
        public Quaternion ToQuaternionFromAxisInRadians(float angleInRadians)
        {
            var halfAngle = angleInRadians * 0.5f;
            var s = MathF.Sin(halfAngle);
            var c = MathF.Cos(halfAngle);

            return new Quaternion(this * s, c);
        }

        /// <summary>
        /// Copies the contents of the vector into the given array, starting from the given index.
        /// </summary>
        public void CopyTo(float[] array, int index = 0)
        {
            array[index] = X;
            array[index + 1] = Y;
            array[index + 2] = Z;
        }

        /// <summary>
        /// Returns a boolean indicating whether the given Vector3 is equal to this Vector3 instance.
        /// </summary>
        /// <param name="other">The Vector3 to compare this instance to.</param>
        /// <returns>True if the other Vector3 is equal to this instance; False otherwise.</returns>
        public bool Equals(Vector3 other)
        {
            return X == other.X
                && Y == other.Y
                && Z == other.Z;
        }
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this Vector3 instance.
        /// </summary>
        /// <param name="obj">The Object to compare against.</param>
        /// <returns>True if the Object is equal to this Vector3; False otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Vector3 vector && Equals(vector);
        }
        /// <summary>
        /// Returns a String representing this Vector3 instance.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        /// <summary>
        /// Returns the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="point1">The first point.</param>
        /// <param name="point2">The second point.</param>
        /// <returns>The distance.</returns>
        public static float Distance(Vector3 point1, Vector3 point2) => point1.GetDistanceTo(point2);
        /// <summary>
        /// Returns the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="point1">The first point.</param>
        /// <param name="point2">The second point.</param>
        /// <returns>The distance.</returns>
        public static float DistanceFast(Vector3 point1, Vector3 point2) => point1.GetDistanceFastTo(point2);
        /// <summary>
        /// Returns the Euclidean distance squared between the two given points.
        /// </summary>
        /// <param name="point1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance squared.</returns>
        public static float DistanceSquared(Vector3 point1, Vector3 value2) => point1.GetDistanceSquaredTo(value2);

        public static Vector3 CreateWithX(float x) => new(x, 0f, 0f);
        public static Vector3 CreateWithY(float y) => new(0f, y, 0f);
        public static Vector3 CreateWithZ(float z) => new(0f, 0f, z);
        
        public static Vector3 CreateWithXY(float x, float y) => new(x, y, 0f);
        public static Vector3 CreateWithXZ(float x, float z) => new(x, 0f, z);
        public static Vector3 CreateWithYZ(float y, float z) => new(0f, y, z);

        public static Vector3 CreateWithXY(Vector2 xy) => new(xy.Component1, xy.Component2, 0f);
        public static Vector3 CreateWithXZ(Vector2 xz) => new(xz.Component1, 0f, xz.Component2);
        public static Vector3 CreateWithYZ(Vector2 yz) => new(0f, yz.Component1, yz.Component2);

        public static Quaternion ToQuaternionFromDegrees(Vector3 anglesInDegrees) => anglesInDegrees.ToQuaternionFromDegrees();
        public static Quaternion ToQuaternionFromRadians(Vector3 anglesInRadians) => anglesInRadians.ToQuaternionFromRadians();
        public static Quaternion ToQuaternionFromAxisInDegrees(Vector3 axis, float angleInDegrees) => axis.ToQuaternionFromAxisInDegrees(angleInDegrees);
        public static Quaternion ToQuaternionFromAxisInRadians(Vector3 axis, float angleInRadians) => axis.ToQuaternionFromAxisInRadians(angleInRadians);

        /// <summary>
        /// Returns the dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The dot product.</returns>
        public static float Dot(Vector3 value1, Vector3 value2)
        {
            return value1.X * value2.X + value1.Y * value2.Y + value1.Z * value2.Z;
        }
        /// <summary>
        /// Computes the cross product of two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The cross product.</returns>
        public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(
                vector1.Y * vector2.Z - vector1.Z * vector2.Y,
                vector1.Z * vector2.X - vector1.X * vector2.Z,
                vector1.X * vector2.Y - vector1.Y * vector2.X
            );
        }

        /// <summary>
        /// Returns a vector whose elements are the minimum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <returns>The minimized vector.</returns>
        public static Vector3 Min(Vector3 value1, Vector3 value2)
        {
            return new Vector3(
                (value1.X < value2.X) ? value1.X : value2.X,
                (value1.Y < value2.Y) ? value1.Y : value2.Y,
                (value1.Z < value2.Z) ? value1.Z : value2.Z
            );
        }
        /// <summary>
        /// Returns a vector whose elements are the maximum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <returns>The maximized vector.</returns>
        public static Vector3 Max(Vector3 value1, Vector3 value2)
        {
            return new Vector3(
                (value1.X > value2.X) ? value1.X : value2.X,
                (value1.Y > value2.Y) ? value1.Y : value2.Y,
                (value1.Z > value2.Z) ? value1.Z : value2.Z
            );
        }
        /// <summary>
        /// Returns a vector whose elements are the absolute values of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The absolute value vector.</returns>
        public static Vector3 Abs(Vector3 value)
        {
            return new Vector3(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z));
        }

        /// <summary>
        /// Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The square root vector.</returns>
        public static Vector3 SquareRoot(Vector3 value) => value.SquareRooted;
        /// <summary>
        /// Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The square root vector.</returns>
        public static Vector3 SquareRootFast(Vector3 value) => value.SquareRootedFast;
        /// <summary>
        /// Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        public static Vector3 Normalize(Vector3 value) => value.Normalized;
        /// <summary>
        /// Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        public static Vector3 NormalizeFast(Vector3 value) => value.NormalizedFast;
        /// <summary>
        /// Returns a direction vector (normalized vector).
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The direction vector.</returns>
        public static Vector3 GetDirection(Vector3 value) => value.Direction;
        /// <summary>
        /// Returns a direction vector (normalized vector).
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The direction vector.</returns>
        public static Vector3 GetDirectionFast(Vector3 value) => value.DirectionFast;

        /// <summary>
        /// Returns the reflection of a vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="vector">The source vector.</param>
        /// <param name="normal">The normal of the surface being reflected off.</param>
        /// <returns>The reflected vector.</returns>
        public static Vector3 Reflect(Vector3 vector, Vector3 normal) => vector.GetReflectionBy(normal);
        /// <summary>
        /// Restricts a vector between a min and max value.
        /// </summary>
        /// <param name="value1">The source vector.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The restricted vector.</returns>
        public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max)
        {
            // This compare order is very important!!!
            // We must follow HLSL behavior in the case user specified min value is bigger than max value.
            var x = value1.X;
            x = (min.X > x) ? min.X : x;  // max(x, minx)
            x = (max.X < x) ? max.X : x;  // min(x, maxx)

            var y = value1.Y;
            y = (min.Y > y) ? min.Y : y;  // max(y, miny)
            y = (max.Y < y) ? max.Y : y;  // min(y, maxy)

            var z = value1.Z;
            z = (min.Z > z) ? min.Z : z;  // max(z, minz)
            z = (max.Z < z) ? max.Z : z;  // min(z, maxz)

            return new Vector3(x, y, z);
        }
        /// <summary>
        /// Clamped linearly interpolates between two vectors based on the given weighting.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of the second source vector.</param>
        /// <returns>The interpolated vector.</returns>
        public static Vector3 LerpClamped(Vector3 value1, Vector3 value2, float amount)
        {
            return Lerp(value1, value2, System.Math.Clamp(amount, 0f, 1f));
        }
        /// <summary>
        /// Linearly interpolates between two vectors based on the given weighting.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of the second source vector.</param>
        /// <returns>The interpolated vector.</returns>
        public static Vector3 Lerp(Vector3 value1, Vector3 value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector3 Transform(Vector3 position, Matrix4x4 matrix)
        {
            return new Vector3(
                position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41,
                position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42,
                position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43
            );
        }
        /// <summary>
        /// Transforms a vector normal by the given matrix.
        /// </summary>
        /// <param name="normal">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector3 TransformNormal(Vector3 normal, Matrix4x4 matrix)
        {
            return new Vector3(
                normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31,
                normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32,
                normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33
            );
        }
        /// <summary>
        /// Transforms a vector by the given Quaternion rotation value.
        /// </summary>
        /// <param name="value">The source vector to be rotated.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector3 Transform(Vector3 value, Quaternion rotation)
        {
            var x2 = rotation.X + rotation.X;
            var y2 = rotation.Y + rotation.Y;
            var z2 = rotation.Z + rotation.Z;

            var wx2 = rotation.W * x2;
            var wy2 = rotation.W * y2;
            var wz2 = rotation.W * z2;
            var xx2 = rotation.X * x2;
            var xy2 = rotation.X * y2;
            var xz2 = rotation.X * z2;
            var yy2 = rotation.Y * y2;
            var yz2 = rotation.Y * z2;
            var zz2 = rotation.Z * z2;

            return new Vector3(
                value.X * (1.0f - yy2 - zz2) + value.Y * (xy2 - wz2) + value.Z * (xz2 + wy2),
                value.X * (xy2 + wz2) + value.Y * (1.0f - xx2 - zz2) + value.Z * (yz2 - wx2),
                value.X * (xz2 - wy2) + value.Y * (yz2 + wx2) + value.Z * (1.0f - xx2 - yy2)
            );
        }

        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The summed vector.</returns>
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }
        /// <summary>
        /// Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The difference vector.</returns>
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }
        /// <summary>
        /// Multiplies two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The product vector.</returns>
        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }
        /// <summary>
        /// Divides the first vector by the second.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        public static Vector3 operator /(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
        }
        /// <summary>
        /// Negates a given vector.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The negated vector.</returns>
        public static Vector3 operator -(Vector3 value)
        {
            return Zero - value;
        }
        /// <summary>
        /// Returns a boolean indicating whether the two given vectors are equal.
        /// </summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns>True if the vectors are equal; False otherwise.</returns>
        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return left.X == right.X
                && left.Y == right.Y
                && left.Z == right.Z;
        }
        /// <summary>
        /// Returns a boolean indicating whether the two given vectors are not equal.
        /// </summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns>True if the vectors are not equal; False if they are equal.</returns>
        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return left.X != right.X
                || left.Y != right.Y
                || left.Z != right.Z;
        }

        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The summed vector.</returns>
        public static Vector3 Add(Vector3 left, Vector3 right) => left + right;
        /// <summary>
        /// Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The difference vector.</returns>
        public static Vector3 Subtract(Vector3 left, Vector3 right) => left - right;
        /// <summary>
        /// Multiplies two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The product vector.</returns>
        public static Vector3 Multiply(Vector3 left, Vector3 right) => left * right;
        /// <summary>
        /// Divides the first vector by the second.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        public static Vector3 Divide(Vector3 left, Vector3 right) => left / right;
        /// <summary>
        /// Negates a given vector.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The negated vector.</returns>
        public static Vector3 Negate(Vector3 value) => -value;
        
        public static implicit operator Vector3(byte value) => new(value);
        public static implicit operator Vector3(sbyte value) => new(value);
        public static implicit operator Vector3(short value) => new(value);
        public static implicit operator Vector3(ushort value) => new(value);
        public static implicit operator Vector3(int value) => new(value);
        public static implicit operator Vector3(uint value) => new(value);
        public static implicit operator Vector3(long value) => new(value);
        public static implicit operator Vector3(ulong value) => new(value);

        public static implicit operator Vector3(float value) => new(value);
        public static implicit operator Vector3(double value) => new((float)value);
        public static implicit operator Vector3(decimal value) => new((float)value);

        public static implicit operator Vector3(System.Numerics.Vector2 value) => new(value.X, value.Y, 0f);
        public static implicit operator Vector3(System.Numerics.Vector3 value) => new(value.X, value.Y, value.Z);
        public static implicit operator Vector3(System.Numerics.Vector4 value) => new(value.X, value.Y, value.Z);

        public static implicit operator System.Numerics.Vector2(Vector3 value) => new(value.X, value.Y);
        public static implicit operator System.Numerics.Vector3(Vector3 value) => new(value.X, value.Y, value.Z);
        public static implicit operator System.Numerics.Vector4(Vector3 value) => new(value.X, value.Y, value.Z, 0f);
        
        public static implicit operator Vector3(Vector2 value) => new(value.X, value.Y, 0f);
    }
}