using System;
using System.Numerics;

namespace LinkEngine.Math
{
    [Serializable]
    public readonly struct Vector2 : IEquatable<Vector2>
    {
        /// <summary>
        /// Defines an instance with all components set to positive infinity.
        /// </summary>
        public static readonly Vector2 PositiveInfinity = new(float.PositiveInfinity, float.PositiveInfinity);
        /// <summary>
        /// Defines an instance with all components set to negative infinity.
        /// </summary>
        public static readonly Vector2 NegativeInfinity = new(float.NegativeInfinity, float.NegativeInfinity);

        /// <summary>
        /// Returns the vector (0,0).
        /// </summary>
        public static readonly Vector2 Zero = new();
        /// <summary>
        /// Returns the vector (1,1).
        /// </summary>
        public static readonly Vector2 One = new(1f, 1f);
        /// <summary>
        /// Returns the vector (-1,-1).
        /// </summary>
        public static readonly Vector2 Negative = new(-1f, -1f);
        /// <summary>
        /// Returns the vector (0.5,0.5).
        /// </summary>
        public static readonly Vector2 Half = new(0.5f, 0.5f);
        /// <summary>
        /// Returns the vector (2,2).
        /// </summary>
        public static readonly Vector2 Two = new(2f, 2f);

        /// <summary>
        /// Returns the vector (1,0).
        /// </summary>
        public static readonly Vector2 UnitX = new(1f, 0f);
        /// <summary>
        /// Returns the vector (0,1).
        /// </summary>
        public static readonly Vector2 UnitY = new(0f, 1f);

        /// <summary>
        /// Returns the vector (1,0).
        /// </summary>
        public static readonly Vector2 Right = new(1f, 0f);
        /// <summary>
        /// Returns the vector (-1,0).
        /// </summary>
        public static readonly Vector2 Left = new(-1f, 0f);
        /// <summary>
        /// Returns the vector (0,1).
        /// </summary>
        public static readonly Vector2 Up = new(0f, 1f);
        /// <summary>
        /// Returns the vector (0,-1).
        /// </summary>
        public static readonly Vector2 Down = new(0f, -1f);
        /// <summary>
        /// Returns the vector (1,1).
        /// </summary>
        public static readonly Vector2 RightUp = One;
        /// <summary>
        /// Returns the vector (1,-1).
        /// </summary>
        public static readonly Vector2 RightDown = Right + Down;
        /// <summary>
        /// Returns the vector (-1,1).
        /// </summary>
        public static readonly Vector2 LeftUp = Left + Up;
        /// <summary>
        /// Returns the vector (-1,-1).
        /// </summary>
        public static readonly Vector2 LeftDown = Negative;
        
        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public readonly float X;
        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public readonly float Y;
        
        /// <summary>
        /// Constructs a vector whose elements are all the single specified value.
        /// </summary>
        /// <param name="value">The element to fill the vector with.</param>
        public Vector2(float value) : this(value, value) { }
        /// <summary>
        /// Constructs a vector with the given individual elements.
        /// </summary>
        /// <param name="x">The X component.</param>
        /// <param name="y">The Y component.</param>
        public Vector2(float x, float y) => (X, Y) = (x, y);

        /// <summary>
        /// Gets or sets the value at the index of the Vector. Index can be high then 1, than it will be wrapped.
        /// </summary>
        /// <param name="index">The index of the component from the Vector.</param>
        public float this[int index] => (index % 2) switch
        {
            0 => X,
            1 => Y
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
        /// Get a Vector2 with the X and Y components of this instance.
        /// </summary>
        public Vector2 XY => new(X, Y);
        /// <summary>
        /// Get a Vector2 with the Y and X components of this instance.
        /// </summary>
        public Vector2 YX => new(Y, X);
        
        /// <summary>
        /// Returns the Vector3 by Vector2.
        /// </summary>
        /// <returns>The Vector3.</returns>
        public Vector3 Vector3 => this;
        
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
        public float LengthSquared => Vector.IsHardwareAccelerated ? Dot(this, this) : X * X + Y * Y;

        /// <summary>
        /// Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <returns>The square root vector.</returns>
        public Vector2 SquareRooted => new(MathF.Sqrt(X), MathF.Sqrt(Y));
        /// <summary>
        /// Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <returns>The square root vector.</returns>
        public Vector2 SquareRootedFast => new(MathFunctions.InverseSqrtFast(X), MathFunctions.InverseSqrtFast(Y));
        /// <summary>
        /// Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <returns>The normalized vector.</returns>
        public Vector2 Normalized => this / Length;
        /// <summary>
        /// Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <returns>The normalized vector.</returns>
        public Vector2 NormalizedFast => this / LengthFast;
        /// <summary>
        /// Returns a direction vector (normalized vector).
        /// </summary>
        /// <returns>The direction vector.</returns>
        public Vector2 Direction => Normalized;
        /// <summary>
        /// Returns a direction vector (normalized vector).
        /// </summary>
        /// <returns>The direction vector.</returns>
        public Vector2 DirectionFast => NormalizedFast;

        /// <summary>
        /// Gets the perpendicular vector on the right side of this vector.
        /// </summary>
        public Vector2 PerpendicularRight => new(Y, -X);
        /// <summary>
        /// Gets the perpendicular vector on the left side of this vector.
        /// </summary>
        public Vector2 PerpendicularLeft => new(-Y, X);

        /// <summary>
        /// Returns the reflection of a vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="normal">The normal of the surface being reflected off.</param>
        /// <returns>The reflected vector.</returns>
        public Vector2 GetReflectionBy(Vector2 normal)
        {
            return this - 2f * Dot(this, normal) * normal;
        }

        /// <summary>
        /// Returns the dot product of two vectors.
        /// </summary>
        /// <param name="value">The second vector.</param>
        /// <returns>The dot product.</returns>
        public float GetDotProductWith(Vector2 value)
        {
            return X * value.X + Y * value.Y;
        }
        /// <summary>
        /// Computes the cross product of two vectors.
        /// </summary>
        /// <param name="vector">The second vector.</param>
        /// <returns>The cross product.</returns>
        public float GetCrossProductWith(Vector2 vector)
        {
            return X * vector.Y - Y * vector.X;
        }

        /// <summary>
        /// Returns the vector between the two given points.
        /// </summary>
        /// <param name="target">The target point.</param>
        /// <returns>The vector.</returns>
        public Vector2 GetVectorTo(Vector2 target)
        {
            return target - this;
        }
        /// <summary>
        /// Returns the direction vector between the two given points.
        /// </summary>
        /// <param name="target">The target point.</param>
        /// <returns>The direction vector.</returns>
        public Vector2 GetDirectionTo(Vector2 target)
        {
            return GetVectorTo(target).Normalized;
        }
        /// <summary>
        /// Returns the direction vector between the two given points.
        /// </summary>
        /// <param name="target">The target point.</param>
        /// <returns>The direction vector.</returns>
        public Vector2 GetDirectionFastTo(Vector2 target)
        {
            return GetVectorTo(target).NormalizedFast;
        }

        /// <summary>
        /// Returns the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="target">The target point.</param>
        /// <returns>The distance.</returns>
        public float GetDistanceTo(Vector2 target)
        {
            return GetVectorTo(target).Length;
        }
        /// <summary>
        /// Returns the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="target">The target point.</param>
        /// <returns>The distance.</returns>
        public float GetDistanceFastTo(Vector2 target)
        {
            return GetVectorTo(target).LengthFast;
        }
        /// <summary>
        /// Returns the Euclidean distance squared between the two given points.
        /// </summary>
        /// <param name="target">The target point.</param>
        /// <returns>The distance squared.</returns>
        public float GetDistanceSquaredTo(Vector2 target)
        {
            return GetVectorTo(target).LengthSquared;
        }

        public Vector2 WithX(float x) => new(x, Y);
        public Vector2 WithY(float y) => new(X, y);

        /// <summary>
        /// Copies the contents of the vector into the given array, starting from the given index.
        /// </summary>
        public void CopyTo(float[] array, int index = 0)
        {
            array[index] = X;
            array[index + 1] = Y;
        }

        /// <summary>
        /// Returns a boolean indicating whether the given Vector2 is equal to this Vector2 instance.
        /// </summary>
        /// <param name="other">The Vector2 to compare this instance to.</param>
        /// <returns>True if the other Vector2 is equal to this instance; False otherwise.</returns>
        public bool Equals(Vector2 other)
        {
            return X == other.X
                && Y == other.Y;
        }
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this Vector2 instance.
        /// </summary>
        /// <param name="obj">The Object to compare against.</param>
        /// <returns>True if the Object is equal to this Vector2; False otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Vector2 vector2 && Equals(vector2);
        }
        /// <summary>
        /// Returns a String representing this Vector2 instance.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static Vector2 CreateWithX(float x) => new(x, 0f);
        public static Vector2 CreateWithY(float y) => new(0f, y);

        /// <summary>
        /// Returns the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance.</returns>
        public static float Distance(Vector2 value1, Vector2 value2) => value1.GetDistanceTo(value2);
        /// <summary>
        /// Returns the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance.</returns>
        public static float DistanceFast(Vector2 value1, Vector2 value2) => value1.GetDistanceFastTo(value2);
        /// <summary>
        /// Returns the Euclidean distance squared between the two given points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance squared.</returns>
        public static float DistanceSquared(Vector2 value1, Vector2 value2) => value1.GetDistanceSquaredTo(value2);

        /// <summary>
        /// Returns the dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The dot product.</returns>
        public static float Dot(Vector2 value1, Vector2 value2)
        {
            return value1.X * value2.X + value1.Y * value2.Y;
        }
        /// <summary>
        /// Computes the cross product of two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The cross product.</returns>
        public static float Cross(Vector2 vector1, Vector2 vector2)
        {
            return vector1.X * vector2.Y - vector1.Y * vector2.X;
        }

        /// <summary>
        /// Returns a vector whose elements are the minimum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <returns>The minimized vector.</returns>
        public static Vector2 Min(Vector2 value1, Vector2 value2)
        {
            return new Vector2(
                (value1.X < value2.X) ? value1.X : value2.X,
                (value1.Y < value2.Y) ? value1.Y : value2.Y);
        }
        /// <summary>
        /// Returns a vector whose elements are the maximum of each of the pairs of elements in the two source vectors
        /// </summary>
        /// <param name="value1">The first source vector</param>
        /// <param name="value2">The second source vector</param>
        /// <returns>The maximized vector</returns>
        public static Vector2 Max(Vector2 value1, Vector2 value2)
        {
            return new Vector2(
                (value1.X > value2.X) ? value1.X : value2.X,
                (value1.Y > value2.Y) ? value1.Y : value2.Y);
        }
        /// <summary>
        /// Returns a vector whose elements are the absolute values of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The absolute value vector.</returns>
        public static Vector2 Abs(Vector2 value)
        {
            return new Vector2(MathF.Abs(value.X), MathF.Abs(value.Y));
        }
        
        /// <summary>
        /// Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The square root vector.</returns>
        public static Vector2 SquareRoot(Vector2 value) => value.SquareRooted;
        /// <summary>
        /// Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The square root vector.</returns>
        public static Vector2 SquareRootFast(Vector2 value) => value.SquareRootedFast;
        /// <summary>
        /// Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        public static Vector2 Normalize(Vector2 value) => value.Normalized;
        /// <summary>
        /// Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        public static Vector2 NormalizeFast(Vector2 value) => value.NormalizedFast;
        /// <summary>
        /// Returns a direction vector (normalized vector).
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The direction vector.</returns>
        public static Vector2 GetDirection(Vector2 value) => value.Direction;
        /// <summary>
        /// Returns a direction vector (normalized vector).
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The direction vector.</returns>
        public static Vector2 GetDirectionFast(Vector2 value) => value.DirectionFast;

        /// <summary>
        /// Returns the reflection of a vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="vector">The source vector.</param>
        /// <param name="normal">The normal of the surface being reflected off.</param>
        /// <returns>The reflected vector.</returns>
        public static Vector2 Reflect(Vector2 vector, Vector2 normal) => vector.GetReflectionBy(normal);
        /// <summary>
        /// Restricts a vector between a min and max value.
        /// </summary>
        /// <param name="value1">The source vector.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
        {
            // This compare order is very important!!!
            // We must follow HLSL behavior in the case user specified min value is bigger than max value.
            var x = value1.X;
            x = (min.X > x) ? min.X : x;  // max(x, minx)
            x = (max.X < x) ? max.X : x;  // min(x, maxx)

            var y = value1.Y;
            y = (min.Y > y) ? min.Y : y;  // max(y, miny)
            y = (max.Y < y) ? max.Y : y;  // min(y, maxy)

            return new Vector2(x, y);
        }
        /// <summary>
        /// Clamped linearly interpolates between two vectors based on the given weighting.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of the second source vector.</param>
        /// <returns>The interpolated vector.</returns>
        public static Vector2 LerpClamped(Vector2 value1, Vector2 value2, float amount)
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
        public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector2 Transform(Vector2 position, Matrix3x2 matrix)
        {
            return new Vector2(
                position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M31,
                position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M32
            );
        }
        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector2 Transform(Vector2 position, Matrix4x4 matrix)
        {
            return new Vector2(
                position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41,
                position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42
            );
        }
        /// <summary>
        /// Transforms a vector normal by the given matrix.
        /// </summary>
        /// <param name="normal">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector2 TransformNormal(Vector2 normal, Matrix3x2 matrix)
        {
            return new Vector2(
                normal.X * matrix.M11 + normal.Y * matrix.M21,
                normal.X * matrix.M12 + normal.Y * matrix.M22
            );
        }
        /// <summary>
        /// Transforms a vector normal by the given matrix.
        /// </summary>
        /// <param name="normal">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector2 TransformNormal(Vector2 normal, Matrix4x4 matrix)
        {
            return new Vector2(
                normal.X * matrix.M11 + normal.Y * matrix.M21,
                normal.X * matrix.M12 + normal.Y * matrix.M22
            );
        }
        /// <summary>
        /// Transforms a vector by the given Quaternion rotation value.
        /// </summary>
        /// <param name="value">The source vector to be rotated.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector2 Transform(Vector2 value, Quaternion rotation)
        {
            var x2 = rotation.X + rotation.X;
            var y2 = rotation.Y + rotation.Y;
            var z2 = rotation.Z + rotation.Z;

            var wz2 = rotation.W * z2;
            var xx2 = rotation.X * x2;
            var xy2 = rotation.X * y2;
            var yy2 = rotation.Y * y2;
            var zz2 = rotation.Z * z2;

            return new Vector2(
                value.X * (1f - yy2 - zz2) + value.Y * (xy2 - wz2),
                value.X * (xy2 + wz2) + value.Y * (1f - xx2 - zz2)
            );
        }
        
        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The summed vector.</returns>
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }
        /// <summary>
        /// Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The difference vector.</returns>
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }
        /// <summary>
        /// Multiplies two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The product vector.</returns>
        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X * right.X, left.Y * right.Y);
        }
        /// <summary>
        /// Divides the first vector by the second.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X / right.X, left.Y / right.Y);
        }
        /// <summary>
        /// Negates a given vector.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The negated vector.</returns>
        public static Vector2 operator -(Vector2 value)
        {
            return Zero - value;
        }
        /// <summary>
        /// Returns a boolean indicating whether the two given vectors are equal.
        /// </summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns>True if the vectors are equal; False otherwise.</returns>
        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.X == right.X
                && left.Y == right.Y;
        }
        /// <summary>
        /// Returns a boolean indicating whether the two given vectors are not equal.
        /// </summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns>True if the vectors are not equal; False if they are equal.</returns>
        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return left.X != right.X
                || left.Y != right.Y;
        }
        
        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The summed vector.</returns>
        public static Vector2 Add(Vector2 left, Vector2 right) => left + right;
        /// <summary>
        /// Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The difference vector.</returns>
        public static Vector2 Subtract(Vector2 left, Vector2 right) => left - right;
        /// <summary>
        /// Multiplies two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The product vector.</returns>
        public static Vector2 Multiply(Vector2 left, Vector2 right) => left * right;
        /// <summary>
        /// Divides the first vector by the second.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        public static Vector2 Divide(Vector2 left, Vector2 right) => left / right;
        /// <summary>
        /// Negates a given vector.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The negated vector.</returns>
        public static Vector2 Negate(Vector2 value) => -value;
        
        public static implicit operator Vector2(byte value) => new(value);
        public static implicit operator Vector2(sbyte value) => new(value);
        public static implicit operator Vector2(short value) => new(value);
        public static implicit operator Vector2(ushort value) => new(value);
        public static implicit operator Vector2(int value) => new(value);
        public static implicit operator Vector2(uint value) => new(value);
        public static implicit operator Vector2(long value) => new(value);
        public static implicit operator Vector2(ulong value) => new(value);
        
        public static implicit operator Vector2(float value) => new(value);
        public static implicit operator Vector2(double value) => new((float)value);
        public static implicit operator Vector2(decimal value) => new((float)value);

        public static implicit operator Vector2(System.Numerics.Vector2 value) => new(value.X, value.Y);
        public static implicit operator Vector2(System.Numerics.Vector3 value) => new(value.X, value.Y);
        public static implicit operator Vector2(System.Numerics.Vector4 value) => new(value.X, value.Y);

        public static implicit operator System.Numerics.Vector2(Vector2 value) => new(value.X, value.Y);
        public static implicit operator System.Numerics.Vector3(Vector2 value) => new(value.X, value.Y, 0f);
        public static implicit operator System.Numerics.Vector4(Vector2 value) => new(value.X, value.Y, 0f, 0f);
        
        public static implicit operator Vector2(Vector3 value) => new(value.X, value.Y);
    }
}