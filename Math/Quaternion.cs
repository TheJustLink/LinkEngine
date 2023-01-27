using System;
using System.Numerics;

namespace LinkEngine.Math
{
    /// <summary>
    /// A structure encapsulating a four-dimensional vector (x,y,z,w), 
    /// which is used to efficiently rotate an object about the (x,y,z) vector by the angle theta, where w = cos(theta/2).
    /// </summary>
    [Serializable]
    public readonly struct Quaternion : IEquatable<Quaternion>
    {
        public const float SlerpEpsilon = 1e-6f;

        /// <summary>
        /// Defines an instance with all components set to positive infinity.
        /// </summary>
        public static readonly Quaternion PositiveInfinity = new(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        /// <summary>
        /// Defines an instance with all components set to negative infinity.
        /// </summary>
        public static readonly Quaternion NegativeInfinity = new(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

        /// <summary>
        /// Returns a zero Quaternion (0,0,0,0).
        /// </summary>
        public static readonly Quaternion Zero = new(0, 0, 0, 0);
        /// <summary>
        /// Returns a Quaternion representing no rotation (0,0,0,1).
        /// </summary>
        public static readonly Quaternion Identity = new(0, 0, 0, 1);
        /// <summary>
        /// Returns a one Quaternion (1,1,1,1).
        /// </summary>
        public static readonly Quaternion One = new(1, 1, 1, 1);
        /// <summary>
        /// Returns a one Quaternion (-1,-1,-1,-1).
        /// </summary>
        public static readonly Quaternion Negative = new(-1, -1, -1, -1);
        /// <summary>
        /// Returns a one Quaternion (0.5,0.5,0.5,0.5).
        /// </summary>
        public static readonly Quaternion Half = new(0.5f, 0.5f, 0.5f, 0.5f);

        /// <summary>
        /// Returns the vector (1,0,0,0).
        /// </summary>
        public static readonly Quaternion UnitX = new(1f, 0f, 0f, 0f);
        /// <summary>
        /// Returns the vector (0,1,0,0).
        /// </summary>
        public static readonly Quaternion UnitY = new(0f, 1f, 0f, 0f);
        /// <summary>
        /// Returns the vector (0,0,1,0).
        /// </summary>
        public static readonly Quaternion UnitZ = new(0f, 0f, 1f, 0f);
        /// <summary>
        /// Returns the vector (0,0,0,1).
        /// </summary>
        public static readonly Quaternion UnitW = new(0f, 0f, 0f, 1f);

        /// <summary>
        /// Returns a Quaternion with 15 degrees rotation around the X-axis.
        /// </summary>
        public static readonly Quaternion X15 = new(0.258819045102521f, 0, 0, 0.965925826289068f);
        /// <summary>
        /// Returns a Quaternion with 30 degrees rotation around the X-axis.
        /// </summary>
        public static readonly Quaternion X30 = new(0.258819045102521f, 0, 0, 0.965925826289068f);
        /// <summary>
        /// Returns a Quaternion with 45 degrees rotation around the X-axis.
        /// </summary>
        public static readonly Quaternion X45 = new(0.3826834323650898f, 0, 0, 0.9238795325112867f);
        /// <summary>
        /// Returns a Quaternion with 90 degrees rotation around the X-axis.
        /// </summary>
        public static readonly Quaternion X90 = new(0.70710676908493042f, 0, 0, 0.70710676908493042f);
        /// <summary>
        /// Returns a Quaternion with 180 degrees rotation around the X-axis.
        /// </summary>
        public static readonly Quaternion X180 = new(1, 0, 0, 0);

        /// <summary>
        /// Returns a Quaternion with 15 degrees rotation around the Y-axis.
        /// </summary>
        public static readonly Quaternion Y15 = new(0, 0.258819045102521f, 0, 0.965925826289068f);
        /// <summary>
        /// Returns a Quaternion with 30 degrees rotation around the Y-axis.
        /// </summary>
        public static readonly Quaternion Y30 = new(0, 0.258819045102521f, 0, 0.965925826289068f);
        /// <summary>
        /// Returns a Quaternion with 45 degrees rotation around the Y-axis.
        /// </summary>
        public static readonly Quaternion Y45 = new(0, 0.3826834323650898f, 0, 0.9238795325112867f);
        /// <summary>
        /// Returns a Quaternion with 90 degrees rotation around the Y-axis.
        /// </summary>
        public static readonly Quaternion Y90 = new(0, 0.70710676908493042f, 0, 0.70710676908493042f);
        /// <summary>
        /// Returns a Quaternion with 180 degrees rotation around the Y-axis.
        /// </summary>
        public static readonly Quaternion Y180 = new(0, 1, 0, 0);

        /// <summary>
        /// Returns a Quaternion with 15 degrees rotation around the Z-axis.
        /// </summary>
        public static readonly Quaternion Z15 = new(0, 0, 0.258819045102521f, 0.965925826289068f);
        /// <summary>
        /// Returns a Quaternion with 30 degrees rotation around the Z-axis.
        /// </summary>
        public static readonly Quaternion Z30 = new(0, 0, 0.258819045102521f, 0.965925826289068f);
        /// <summary>
        /// Returns a Quaternion with 45 degrees rotation around the Z-axis.
        /// </summary>
        public static readonly Quaternion Z45 = new(0, 0, 0.3826834323650898f, 0.9238795325112867f);
        /// <summary>
        /// Returns a Quaternion with 90 degrees rotation around the Z-axis.
        /// </summary>
        public static readonly Quaternion Z90 = new(0, 0, 0.70710676908493042f, 0.70710676908493042f);
        /// <summary>
        /// Returns a Quaternion with 180 degrees rotation around the Z-axis.
        /// </summary>
        public static readonly Quaternion Z180 = new(0, 0, 1, 0);

        /// <summary>
        /// Specifies the X-value of the vector component of the Quaternion.
        /// </summary>
        public readonly float X;
        /// <summary>
        /// Specifies the Y-value of the vector component of the Quaternion.
        /// </summary>
        public readonly float Y;
        /// <summary>
        /// Specifies the Z-value of the vector component of the Quaternion.
        /// </summary>
        public readonly float Z;
        /// <summary>
        /// Specifies the rotation component of the Quaternion.
        /// </summary>
        public readonly float W;

        /// <summary>
        /// Constructs a Quaternion from the given components.
        /// </summary>
        /// <param name="x">The X component of the Quaternion.</param>
        /// <param name="y">The Y component of the Quaternion.</param>
        /// <param name="z">The Z component of the Quaternion.</param>
        /// <param name="w">The W component of the Quaternion.</param>
        public Quaternion(float x, float y, float z, float w) => (X, Y, Z, W) = (x, y, z, w);
        /// <summary>
        /// Constructs a Quaternion from the given vector and rotation parts.
        /// </summary>
        /// <param name="vectorPart">The vector part of the Quaternion.</param>
        /// <param name="scalarPart">The rotation part of the Quaternion.</param>
        public Quaternion(Vector3 vectorPart, float scalarPart) =>
            (X, Y, Z, W) = (vectorPart.X, vectorPart.Y, vectorPart.Z, scalarPart);
        /// <summary>
        /// Constructs a Quaternion from the <see cref="System.Numerics.Quaternion"/>
        /// </summary>
        /// <param name="quaternion">The Quaternion from System.Numerics.</param>
        public Quaternion(System.Numerics.Quaternion quaternion) =>
            (X, Y, Z, W) = (quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
        /// <summary>
        /// Constructs a Quaternion from euler angles in radians.
        /// </summary>
        /// <param name="eulerInRadians">Euler angles in radians.</param>
        public Quaternion(Vector3 eulerInRadians) => this = eulerInRadians.ToQuaternionFromRadians();

        /// <summary>
        /// Gets or sets the value at the index of the Vector. Index can be high then 2, than it will be wrapped.
        /// </summary>
        /// <param name="index">The index of the component from the Vector.</param>
        public float this[int index] => (index % 4) switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            3 => W
        };

        /// <summary>
        /// Get a first component of quaternion (X).
        /// </summary>
        public float Component1 => X;
        /// <summary>
        /// Get a second component of quaternion (Y).
        /// </summary>
        public float Component2 => Y;
        /// <summary>
        /// Get a third component of quaternion (Z).
        /// </summary>
        public float Component3 => Z;
        /// <summary>
        /// Get a fourth component of quaternion (W).
        /// </summary>
        public float Component4 => W;

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
        /// Returns whether the Quaternion is the identity Quaternion.
        /// </summary>
        public bool IsIdentity => this == Identity;

        /// <summary>
        /// Calculates the length of the Quaternion.
        /// </summary>
        /// <returns>The computed length of the Quaternion.</returns>
        public float Length => MathF.Sqrt(LengthSquared);

        /// <summary>
        /// Calculates the length squared of the Quaternion. This operation is cheaper than Length().
        /// </summary>
        /// <returns>The length squared of the Quaternion.</returns>
        public float LengthSquared => X * X + Y * Y + Z * Z + W * W;

        /// <summary>
        /// Divides each component of the Quaternion by the length of the Quaternion.
        /// </summary>
        /// <returns>The normalized Quaternion.</returns>
        public Quaternion Normalized => this * (1f / Length);

        /// <summary>
        /// Creates the conjugate of a specified Quaternion.
        /// </summary>
        /// <returns>A new Quaternion that is the conjugate of the specified one.</returns>
        public Quaternion Conjugated => new(-X, -Y, -Z, W);

        /// <summary>
        /// Returns the inverse of a Quaternion.
        /// </summary>
        /// <returns>The inverted Quaternion.</returns>
        public Quaternion Inversed => Conjugated * (1f / LengthSquared);

        /// <summary>
        /// Flips the sign of each component of the quaternion.
        /// </summary>
        /// <returns>The negated Quaternion.</returns>
        public Quaternion Negated => -this;

        /// <summary>
        /// Returns a rotation in degrees that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis; applied in that order.
        /// </summary>
        public Vector3 EulerInDegrees => EulerInRadians * MathFunctions.RadToDeg;
        /// <summary>
        /// Returns a rotation in radians that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis; applied in that order.
        /// </summary>
        public Vector3 EulerInRadians
        {
            get
            {
                // if normalised is one, otherwise is correction factor
                var unit = LengthSquared;
                var test = X * W - Y * Z;

                // singularity at north pole
                if (test > 0.4995f * unit)
                {
                    return MathFunctions.NormalizeAnglesInRadians(new Vector3(
                        MathFunctions.PIHalf,
                        2f * MathF.Atan2(Y, X),
                        0f
                    ));
                }

                // singularity at south pole
                if (test < -0.4995f * unit)
                {
                    return MathFunctions.NormalizeAnglesInRadians(new Vector3(
                        -MathFunctions.PIHalf,
                        -2f * MathF.Atan2(Y, X),
                        0f
                    ));
                }

                var q = new Quaternion(W, Z, X, Y);

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

        public Quaternion WithEulerXInRadians(float x) => EulerInRadians.WithX(x).ToQuaternionFromRadians();
        public Quaternion WithEulerYInRadians(float y) => EulerInRadians.WithY(y).ToQuaternionFromRadians();
        public Quaternion WithEulerZInRadians(float z) => EulerInRadians.WithZ(z).ToQuaternionFromRadians();

        public Quaternion WithEulerXYInRadians(float x, float y) => EulerInRadians.WithXY(x, y).ToQuaternionFromRadians();
        public Quaternion WithEulerXZInRadians(float x, float z) => EulerInRadians.WithXZ(x, z).ToQuaternionFromRadians();
        public Quaternion WithEulerYZInRadians(float y, float z) => EulerInRadians.WithYZ(y, z).ToQuaternionFromRadians();

        public Quaternion WithEulerXYInRadians(Vector2 xy) => EulerInRadians.WithXY(xy).ToQuaternionFromRadians();
        public Quaternion WithEulerXZInRadians(Vector2 xz) => EulerInRadians.WithXZ(xz).ToQuaternionFromRadians();
        public Quaternion WithEulerYZInRadians(Vector2 yz) => EulerInRadians.WithYZ(yz).ToQuaternionFromRadians();

        public Quaternion WithEulerXInDegrees(float x) => EulerInDegrees.WithX(x).ToQuaternionFromDegrees();
        public Quaternion WithEulerYInDegrees(float y) => EulerInDegrees.WithY(y).ToQuaternionFromDegrees();
        public Quaternion WithEulerZInDegrees(float z) => EulerInDegrees.WithZ(z).ToQuaternionFromDegrees();

        public Quaternion WithEulerXYInDegrees(float x, float y) => EulerInDegrees.WithXY(x, y).ToQuaternionFromDegrees();
        public Quaternion WithEulerXZInDegrees(float x, float z) => EulerInDegrees.WithXZ(x, z).ToQuaternionFromDegrees();
        public Quaternion WithEulerYZInDegrees(float y, float z) => EulerInDegrees.WithYZ(y, z).ToQuaternionFromDegrees();

        public Quaternion WithEulerXYInDegrees(Vector2 xy) => EulerInDegrees.WithXY(xy).ToQuaternionFromDegrees();
        public Quaternion WithEulerXZInDegrees(Vector2 xz) => EulerInDegrees.WithXZ(xz).ToQuaternionFromDegrees();
        public Quaternion WithEulerYZInDegrees(Vector2 yz) => EulerInDegrees.WithYZ(yz).ToQuaternionFromDegrees();

        
        public static Quaternion CreateWithEulerXInRadians(float x) => Vector3.CreateWithX(x).ToQuaternionFromRadians();
        public static Quaternion CreateWithEulerYInRadians(float y) => Vector3.CreateWithY(y).ToQuaternionFromRadians();
        public static Quaternion CreateWithEulerZInRadians(float z) => Vector3.CreateWithZ(z).ToQuaternionFromRadians();

        public static Quaternion CreateWithEulerXYInRadians(float x, float y) => Vector3.CreateWithXY(x, y).ToQuaternionFromRadians();
        public static Quaternion CreateWithEulerXZInRadians(float x, float z) => Vector3.CreateWithXZ(x, z).ToQuaternionFromRadians();
        public static Quaternion CreateWithEulerYZInRadians(float y, float z) => Vector3.CreateWithYZ(y, z).ToQuaternionFromRadians();

        public static Quaternion CreateWithEulerXYInRadians(Vector2 xy) => Vector3.CreateWithXY(xy).ToQuaternionFromRadians();
        public static Quaternion CreateWithEulerXZInRadians(Vector2 xz) => Vector3.CreateWithXZ(xz).ToQuaternionFromRadians();
        public static Quaternion CreateWithEulerYZInRadians(Vector2 yz) => Vector3.CreateWithYZ(yz).ToQuaternionFromRadians();

        public static Quaternion CreateWithEulerXInDegrees(float x) => Vector3.CreateWithX(x).ToQuaternionFromDegrees();
        public static Quaternion CreateWithEulerYInDegrees(float y) => Vector3.CreateWithY(y).ToQuaternionFromDegrees();
        public static Quaternion CreateWithEulerZInDegrees(float z) => Vector3.CreateWithZ(z).ToQuaternionFromDegrees();

        public static Quaternion CreateWithEulerXYInDegrees(float x, float y) => Vector3.CreateWithXY(x, y).ToQuaternionFromDegrees();
        public static Quaternion CreateWithEulerXZInDegrees(float x, float z) => Vector3.CreateWithXZ(x, z).ToQuaternionFromDegrees();
        public static Quaternion CreateWithEulerYZInDegrees(float y, float z) => Vector3.CreateWithYZ(y, z).ToQuaternionFromDegrees();

        public static Quaternion CreateWithEulerXYInDegrees(Vector2 xy) => Vector3.CreateWithXY(xy).ToQuaternionFromDegrees();
        public static Quaternion CreateWithEulerXZInDegrees(Vector2 xz) => Vector3.CreateWithXZ(xz).ToQuaternionFromDegrees();
        public static Quaternion CreateWithEulerYZInDegrees(Vector2 yz) => Vector3.CreateWithYZ(yz).ToQuaternionFromDegrees();

        /// <summary>
        /// Divides each component of the Quaternion by the length of the Quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        /// <returns>The normalized Quaternion.</returns>
        public static Quaternion Normalize(Quaternion value) => value.Normalized;
        /// <summary>
        /// Creates the conjugate of a specified Quaternion.
        /// </summary>
        /// <param name="value">The Quaternion of which to return the conjugate.</param>
        /// <returns>A new Quaternion that is the conjugate of the specified one.</returns>
        public static Quaternion Conjugate(Quaternion value) => value.Conjugated;
        /// <summary>
        /// Returns the inverse of a Quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        /// <returns>The inverted Quaternion.</returns>
        public static Quaternion Inverse(Quaternion value) => value.Inversed;
        /// <summary>
        /// Flips the sign of each component of the quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        /// <returns>The negated Quaternion.</returns>
        public static Quaternion Negate(Quaternion value) => value.Negated;

        /// <summary>
        /// Returns a rotation in degrees that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis; applied in that order.
        /// </summary>
        public static Vector3 ToEulerInDegrees(Quaternion value) => value.EulerInDegrees;
        /// <summary>
        /// Returns a rotation in radians that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis; applied in that order.
        /// </summary>
        public static Vector3 ToEulerInRadians(Quaternion value) => value.EulerInRadians;

        /// <summary>
        /// Creates a Quaternion from a normalized vector axis and an angle to rotate about the vector.
        /// </summary>
        /// <param name="axis">The unit vector to rotate around.
        /// This vector must be normalized before calling this function or the resulting Quaternion will be incorrect.</param>
        /// <param name="angle">The angle, in radians, to rotate around the vector.</param>
        /// <returns>The created Quaternion.</returns>
        public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
        {
            var halfAngle = angle * 0.5f;
            
            var s = MathF.Sin(halfAngle);
            var c = MathF.Cos(halfAngle);

            return new(axis.X * s, axis.Y * s, axis.Z * s, c);
        }

        /// <summary>
        /// Creates a new Quaternion from the given yaw, pitch, and roll, in radians.
        /// </summary>
        /// <param name="yaw">The yaw angle, in radians, around the Y-axis.</param>
        /// <param name="pitch">The pitch angle, in radians, around the X-axis.</param>
        /// <param name="roll">The roll angle, in radians, around the Z-axis.</param>
        /// <returns></returns>
        public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            //  Roll first, about axis the object is facing, then
            //  pitch upward, then yaw to face into the new heading

            var halfRoll = roll * 0.5f;
            var sr = MathF.Sin(halfRoll);
            var cr = MathF.Cos(halfRoll);

            var halfPitch = pitch * 0.5f;
            var sp = MathF.Sin(halfPitch);
            var cp = MathF.Cos(halfPitch);

            var halfYaw = yaw * 0.5f;
            var sy = MathF.Sin(halfYaw);
            var cy = MathF.Cos(halfYaw);
            
            return new Quaternion(
                cy * sp * cr + sy * cp * sr,
                sy * cp * cr - cy * sp * sr,
                cy * cp * sr - sy * sp * cr,
                cy * cp * cr + sy * sp * sr
            );
        }

        /// <summary>
        /// Creates a Quaternion from the given rotation matrix.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <returns>The created Quaternion.</returns>
        public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
        {
            float qx, qy, qz, qw;

            var trace = matrix.M11 + matrix.M22 + matrix.M33;

            if (trace > 0f)
            {
                var s = MathF.Sqrt(trace + 1f);
                
                qw = s * 0.5f;
                s = 0.5f / s;

                qx = (matrix.M23 - matrix.M32) * s;
                qy = (matrix.M31 - matrix.M13) * s;
                qz = (matrix.M12 - matrix.M21) * s;
            }
            else
            {
                if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
                {
                    var s = MathF.Sqrt(1.0f + matrix.M11 - matrix.M22 - matrix.M33);
                    
                    var invS = 0.5f / s;
                    
                    qx = 0.5f * s;
                    qy = (matrix.M12 + matrix.M21) * invS;
                    qz = (matrix.M13 + matrix.M31) * invS;
                    qw = (matrix.M23 - matrix.M32) * invS;
                }
                else if (matrix.M22 > matrix.M33)
                {
                    var s = MathF.Sqrt(1.0f + matrix.M22 - matrix.M11 - matrix.M33);
                    
                    var invS = 0.5f / s;
                    
                    qx = (matrix.M21 + matrix.M12) * invS;
                    qy = 0.5f * s;

                    qz = (matrix.M32 + matrix.M23) * invS;
                    qw = (matrix.M31 - matrix.M13) * invS;
                }
                else
                {
                    var s = MathF.Sqrt(1.0f + matrix.M33 - matrix.M11 - matrix.M22);
                    
                    var invS = 0.5f / s;
                    
                    qx = (matrix.M31 + matrix.M13) * invS;
                    qy = (matrix.M32 + matrix.M23) * invS;
                    qz = 0.5f * s;
                    qw = (matrix.M12 - matrix.M21) * invS;
                }
            }

            return new Quaternion(qx, qy, qz, qw);
        }

        /// <summary>
        /// Calculates the dot product of two Quaternions.
        /// </summary>
        /// <param name="quaternion1">The first source Quaternion.</param>
        /// <param name="quaternion2">The second source Quaternion.</param>
        /// <returns>The dot product of the Quaternions.</returns>
        public static float Dot(Quaternion quaternion1, Quaternion quaternion2)
        {
            return quaternion1.X * quaternion2.X +
                   quaternion1.Y * quaternion2.Y +
                   quaternion1.Z * quaternion2.Z +
                   quaternion1.W * quaternion2.W;
        }

        /// <summary>
        /// Interpolates between two quaternions, using spherical linear interpolation.
        /// </summary>
        /// <param name="quaternion1">The first source Quaternion.</param>
        /// <param name="quaternion2">The second source Quaternion.</param>
        /// <param name="amount">The relative weight of the second source Quaternion in the interpolation.</param>
        /// <returns>The interpolated Quaternion.</returns>
        public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
        {
            var cosOmega = Dot(quaternion1, quaternion2);

            var flip = false;

            if (cosOmega < 0f)
            {
                flip = true;
                cosOmega = -cosOmega;
            }

            float s1, s2;

            if (cosOmega > (1f - SlerpEpsilon))
            {
                // Too close, do straight linear interpolation.
                s1 = 1f - amount;
                s2 = flip ? -amount : amount;
            }
            else
            {
                var omega = MathF.Acos(cosOmega);
                var invSinOmega = 1f / MathF.Sin(omega);

                s1 = MathF.Sin((1f - amount) * omega) * invSinOmega;
                s2 = flip
                    ? -MathF.Sin(amount * omega) * invSinOmega
                    : MathF.Sin(amount * omega) * invSinOmega;
            }

            return quaternion1 * s1 + quaternion2 * s2;
        }
        /// <summary>
        ///  Linearly interpolates between two quaternions.
        /// </summary>
        /// <param name="quaternion1">The first source Quaternion.</param>
        /// <param name="quaternion2">The second source Quaternion.</param>
        /// <param name="amount">The relative weight of the second source Quaternion in the interpolation.</param>
        /// <returns>The interpolated Quaternion.</returns>
        public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
        {
            return ((quaternion2 * MathFunctions.Sign(Dot(quaternion1, quaternion2)) - quaternion1) * amount).Normalized;
        }

        /// <summary>
        /// Concatenates two Quaternions; the result represents the value1 rotation followed by the value2 rotation.
        /// quaternion2 * quaternion1 instead of quaternion1 * quaternion2.
        /// </summary>
        /// <param name="quaternion1">The first Quaternion rotation in the series.</param>
        /// <param name="quaternion2">The second Quaternion rotation in the series.</param>
        /// <returns>A new Quaternion representing the concatenation of the value1 rotation followed by the value2 rotation.</returns>
        public static Quaternion Concatenate(Quaternion quaternion1, Quaternion quaternion2)
        {
            // Concatenate rotation is actually q2 * q1 instead of q1 * q2.
            return quaternion2 * quaternion1;
        }

        /// <summary>
        /// Adds two Quaternions element-by-element.
        /// </summary>
        /// <param name="value1">The first source Quaternion.</param>
        /// <param name="value2">The second source Quaternion.</param>
        /// <returns>The result of adding the Quaternions.</returns>
        public static Quaternion Add(Quaternion value1, Quaternion value2) => value1 + value2;
        /// <summary>
        /// Subtracts one Quaternion from another.
        /// </summary>
        /// <param name="value1">The first source Quaternion.</param>
        /// <param name="value2">The second Quaternion, to be subtracted from the first.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Quaternion Subtract(Quaternion value1, Quaternion value2) => value1 - value2;
        /// <summary>
        /// Multiplies two Quaternions together.
        /// </summary>
        /// <param name="value1">The Quaternion on the left side of the multiplication.</param>
        /// <param name="value2">The Quaternion on the right side of the multiplication.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Quaternion Multiply(Quaternion value1, Quaternion value2) => value1 * value2;
        /// <summary>
        /// Multiplies a Quaternion by a scalar value.
        /// </summary>
        /// <param name="value1">The source Quaternion.</param>
        /// <param name="value2">The scalar value.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Quaternion Multiply(Quaternion value1, float value2) => value1 * value2;
        /// <summary>
        /// Divides a Quaternion by another Quaternion.
        /// </summary>
        /// <param name="value1">The source Quaternion.</param>
        /// <param name="value2">The divisor.</param>
        /// <returns>The result of the division.</returns>
        public static Quaternion Divide(Quaternion value1, Quaternion value2) => value1 / value2;

        /// <summary>
        /// Flips the sign of each component of the quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        /// <returns>The negated Quaternion.</returns>
        public static Quaternion operator -(Quaternion value)
        {
            return new Quaternion(-value.X, -value.Y, -value.Z, -value.W);
        }
        /// <summary>
        /// Adds two Quaternions element-by-element.
        /// </summary>
        /// <param name="value1">The first source Quaternion.</param>
        /// <param name="value2">The second source Quaternion.</param>
        /// <returns>The result of adding the Quaternions.</returns>
        public static Quaternion operator +(Quaternion value1, Quaternion value2)
        {
            return new Quaternion(value1.X + value2.X, value1.Y + value2.Y, value1.Z + value2.Z, value1.W + value2.W);
        }
        /// <summary>
        /// Subtracts one Quaternion from another.
        /// </summary>
        /// <param name="value1">The first source Quaternion.</param>
        /// <param name="value2">The second Quaternion, to be subtracted from the first.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Quaternion operator -(Quaternion value1, Quaternion value2)
        {
            return new Quaternion(value1.X - value2.X, value1.Y - value2.Y, value1.Z - value2.Z, value1.W - value2.W);
        }
        /// <summary>
        /// Multiplies two Quaternions together.
        /// </summary>
        /// <param name="value1">The Quaternion on the left side of the multiplication.</param>
        /// <param name="value2">The Quaternion on the right side of the multiplication.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Quaternion operator *(Quaternion value1, Quaternion value2)
        {
            // cross(av, bv)
            var cx = value1.Y * value2.Z - value1.Z * value2.Y;
            var cy = value1.Z * value2.X - value1.X * value2.Z;
            var cz = value1.X * value2.Y - value1.Y * value2.X;

            var dot = value1.X * value2.X + value1.Y * value2.Y + value1.Z * value2.Z;
            
            return new Quaternion(
                value1.X * value2.W + value2.X * value1.W + cx,
                value1.Y * value2.W + value2.Y * value1.W + cy,
                value1.Z * value2.W + value2.Z * value1.W + cz,
                value1.W * value2.W - dot
            );
        }
        /// <summary>
        /// Multiplies a Quaternion by a scalar value.
        /// </summary>
        /// <param name="value1">The source Quaternion.</param>
        /// <param name="value2">The scalar value.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Quaternion operator *(Quaternion value1, float value2)
        {
            return new Quaternion(value1.X * value2, value1.Y * value2, value1.Z * value2, value1.W * value2);
        }
        /// <summary>
        /// Divides a Quaternion by another Quaternion.
        /// </summary>
        /// <param name="value1">The source Quaternion.</param>
        /// <param name="value2">The divisor.</param>
        /// <returns>The result of the division.</returns>
        public static Quaternion operator /(Quaternion value1, Quaternion value2)
        {
            return value1 * value2.Inversed;
        }
        
        /// <summary>
        /// Returns a boolean indicating whether the two given Quaternions are equal.
        /// </summary>
        /// <param name="value1">The first Quaternion to compare.</param>
        /// <param name="value2">The second Quaternion to compare.</param>
        /// <returns>True if the Quaternions are equal; False otherwise.</returns>
        public static bool operator ==(Quaternion value1, Quaternion value2)
        {
            return value1.X == value2.X
                && value1.Y == value2.Y
                && value1.Z == value2.Z
                && value1.W == value2.W;
        }
        /// <summary>
        /// Returns a boolean indicating whether the two given Quaternions are not equal.
        /// </summary>
        /// <param name="value1">The first Quaternion to compare.</param>
        /// <param name="value2">The second Quaternion to compare.</param>
        /// <returns>True if the Quaternions are not equal; False if they are equal.</returns>
        public static bool operator !=(Quaternion value1, Quaternion value2)
        {
            return value1.X != value2.X
                || value1.Y != value2.Y
                || value1.Z != value2.Z
                || value1.W != value2.W;
        }
        
        /// <summary>
        /// Returns a boolean indicating whether the given Quaternion is equal to this Quaternion instance.
        /// </summary>
        /// <param name="other">The Quaternion to compare this instance to.</param>
        /// <returns>True if the other Quaternion is equal to this instance; False otherwise.</returns>
        public bool Equals(Quaternion other)
        {
            return X == other.X &&
                   Y == other.Y &&
                   Z == other.Z &&
                   W == other.W;
        }
        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this Quaternion instance.
        /// </summary>
        /// <param name="obj">The Object to compare against.</param>
        /// <returns>True if the Object is equal to this Quaternion; False otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Quaternion quaternion
                && Equals(quaternion);
        }
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }

        /// <summary>
        /// Returns a String representing this Quaternion instance.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return $"(X:{X} Y:{Y} Z:{Z} W:{W})";
        }

        public static implicit operator Quaternion(System.Numerics.Quaternion value) => new(value.X, value.Y, value.Z, value.W);
        public static implicit operator System.Numerics.Quaternion(Quaternion value) => new(value.X, value.Y, value.Z, value.W);
    }
}