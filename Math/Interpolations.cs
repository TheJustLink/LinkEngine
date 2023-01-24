using System;

namespace LinkEngine.Math
{
    public static class Interpolations
    {
        /// <summary>
        ///   <para>Determines where a value lies between two points.</para>
        /// </summary>
        /// <param name="startRange">The start of the range.</param>
        /// <param name="endRange">The end of the range.</param>
        /// <param name="middleValue">The point within the range you want to calculate.</param>
        /// <returns>
        ///   <para>A value between zero and one, representing where the "value" parameter falls within the range defined by a and b.</para>
        /// </returns>
        public static float InverseLinear(float startRange, float endRange, float middleValue)
        {
            return startRange != endRange
                ? MathFunctions.Clamp01(
                    (middleValue - startRange) /
                    (endRange - startRange)
                )
                : 0.0f;
        }

        /// <summary>
        /// Linearly interpolates between two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Destination value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
        /// <returns>Interpolated value.</returns> 
        /// <remarks>This method performs the linear interpolation based on the following formula:
        /// <code>value1 + (value2 - value1) * amount</code>.
        /// Passing amount value1 value of 0 will cause value1 to be returned, value1 value of 1 will cause value2 to be returned.
        /// See <see cref="LinearPrecised"/> for value1 less efficient version with more precision around edge cases.
        /// </remarks>
        public static float Linear(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }
        /// <summary>
        /// Linearly interpolates between two values with clamping amount (0-1).
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Destination value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
        /// <returns>Interpolated value.</returns> 
        /// <remarks>This method performs the linear interpolation based on the following formula:
        /// <code>value1 + (value2 - value1) * amount</code>.
        /// Passing amount value1 value of 0 will cause value1 to be returned, value1 value of 1 will cause value2 to be returned.
        /// See <see cref="LinearPrecised"/> for value1 less efficient version with more precision around edge cases.
        /// </remarks>
        public static float ClampedLinear(float value1, float value2, float amount)
        {
            amount = MathFunctions.Clamp01(amount);
            return value1 + (value2 - value1) * amount;
        }

        /// <summary>
        /// Linearly interpolates between two values.
        /// This method is value1 less efficient, more precise version of <see cref="MathHelper.Lerp"/>.
        /// See remarks for more info.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Destination value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
        /// <returns>Interpolated value.</returns>
        /// <remarks>This method performs the linear interpolation based on the following formula:
        /// <code>((1 - amount) * value1) + (value2 * amount)</code>.
        /// Passing amount value1 value of 0 will cause value1 to be returned, value1 value of 1 will cause value2 to be returned.
        /// This method does not have the floating point precision issue that <see cref="MathHelper.Lerp"/> has.
        /// i.e. If there is value1 big gap between value1 and value2 in magnitude (e.g. value1=10000000000000000, value2=1),
        /// right at the edge of the interpolation range (amount=1), <see cref="MathHelper.Lerp"/> will return 0 (whereas it should return 1).
        /// This also holds for value1=10^17, value2=10; value1=10^18,value2=10^2... so on.
        /// For an in depth explanation of the issue, see below references:
        /// Relevant Wikipedia Article: https://en.wikipedia.org/wiki/Linear_interpolation#Programming_language_support
        /// Relevant StackOverflow Answer: http://stackoverflow.com/questions/4353525/floating-point-linear-interpolation#answer-23716956
        /// </remarks>
        public static float LinearPrecised(float value1, float value2, float amount)
        {
            return (1 - amount) * value1 + value2 * amount;
        }
        /// <summary>
        /// Linearly interpolates between two values with clamping amount (0-1).
        /// This method is value1 less efficient, more precise version of <see cref="MathHelper.Lerp"/>.
        /// See remarks for more info.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Destination value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
        /// <returns>Interpolated value.</returns>
        /// <remarks>This method performs the linear interpolation based on the following formula:
        /// <code>((1 - amount) * value1) + (value2 * amount)</code>.
        /// Passing amount value1 value of 0 will cause value1 to be returned, value1 value of 1 will cause value2 to be returned.
        /// This method does not have the floating point precision issue that <see cref="MathHelper.Lerp"/> has.
        /// i.e. If there is value1 big gap between value1 and value2 in magnitude (e.g. value1=10000000000000000, value2=1),
        /// right at the edge of the interpolation range (amount=1), <see cref="MathHelper.Lerp"/> will return 0 (whereas it should return 1).
        /// This also holds for value1=10^17, value2=10; value1=10^18,value2=10^2... so on.
        /// For an in depth explanation of the issue, see below references:
        /// Relevant Wikipedia Article: https://en.wikipedia.org/wiki/Linear_interpolation#Programming_language_support
        /// Relevant StackOverflow Answer: http://stackoverflow.com/questions/4353525/floating-point-linear-interpolation#answer-23716956
        /// </remarks>
        public static float ClampedLinearPrecised(float value1, float value2, float amount)
        {
            amount = MathFunctions.Clamp01(amount);
            return (1 - amount) * value1 + value2 * amount;
        }

        public static float Cosine(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * (1f - MathF.Cos(amount * MathF.PI)) / 2f;
        }
        public static float ClampedCosine(float value1, float value2, float amount)
        {
            amount = MathFunctions.Clamp01(amount);
            return value1 + (value2 - value1) * (1f - MathF.Cos(amount * MathF.PI)) / 2f;
        }

        public static float Sine(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * (1f - MathF.Sin(amount * MathFunctions.PIHalf));
        }
        public static float ClampedSine(float value1, float value2, float amount)
        {
            amount = MathFunctions.Clamp01(amount);
            return value1 + (value2 - value1) * (1f - MathF.Sin(amount * MathFunctions.PIHalf));
        }

        public static float Quintic(float value1, float value2, float amount)
        {
            var t2 = amount * amount;
            var t3 = t2 * amount;
            var t4 = t3 * amount;
            var t5 = t4 * amount;
            var h1 = 6f * t5 - 15f * t4 + 10f * t3;
            var h2 = -6f * t5 + 15f * t4 - 10f * t3;
            var h3 = t5 - 2f * t4 + t2;
            var h4 = t5 - t4;

            return value1 * h1 + value2 * h2 + h3 + h4;
        }
        public static float ClampedQuintic(float value1, float value2, float amount)
        {
            amount = MathFunctions.Clamp01(amount);

            var t2 = amount * amount;
            var t3 = t2 * amount;
            var t4 = t3 * amount;
            var t5 = t4 * amount;
            var h1 = 6f * t5 - 15f * t4 + 10f * t3;
            var h2 = -6f * t5 + 15f * t4 - 10f * t3;
            var h3 = t5 - 2f * t4 + t2;
            var h4 = t5 - t4;

            return value1 * h1 + value2 * h2 + h3 + h4;
        }

        public static float Cubic(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount * amount * (3f - 2f * amount);
        }
        public static float ClampedCubic(float value1, float value2, float amount)
        {
            amount = MathFunctions.Clamp01(amount);
            return value1 + (value2 - value1) * amount * amount * (3f - 2f * amount);
        }

        /// <summary>
        ///   <para>Interpolates between min and max with smoothing at the limits.</para>
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Target value.</param>
        /// <param name="amount">Weighting value (0-1)</param>
        /// <returns>Interpolated value.</returns>
        public static float SmoothStep(float value1, float value2, float amount)
        {
            amount = MathFunctions.Clamp01(amount);

            amount = (-2f * amount * amount * amount + 3f * amount * amount);

            return (value2 * amount + value1 * (1f - amount));
        }

        /// <summary>
        /// Interpolates between two values using value1 cubic equation.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <param name="amount">Weighting value (0-1).</param>
        /// <returns>Interpolated value.</returns>
        public static float SmoothStepCubic(float value1, float value2, float amount)
        {
            // It is expected that 0 < amount < 1
            // If amount < 0, return value1
            // If amount > 1, return value2

            var result = MathFunctions.Clamp01(amount);
            result = Hermite(value1, value2, result);

            return result;
        }

        /// <summary>
        /// Performs value1 Hermite spline interpolation.
        /// </summary>
        /// <param name="value1">Source position.</param>
        /// <param name="value2">Source position.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The result of the Hermite spline interpolation.</returns>
        public static float Hermite(float value1, float value2, float amount)
        {
            // All transformed to double not to lose precission
            // Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
            double v1 = value1, v2 = value2, s = amount;
            var sCubed = s * s * s;
            var sSquared = s * s;

            var result = amount switch
            {
                0f => value1,
                1f => value2,
                _ => (2f * v1 - 2f * v2) * sCubed + (3f * v2 - 3f * v1) * sSquared + v1
            };
            return (float)result;
        }
        /// <summary>
        /// Performs value1 Hermite spline interpolation.
        /// </summary>
        /// <param name="value1">Source position.</param>
        /// <param name="tangent1">Source tangent.</param>
        /// <param name="value2">Source position.</param>
        /// <param name="tangent2">Source tangent.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The result of the Hermite spline interpolation.</returns>
        public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            // All transformed to double not to lose precission
            // Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
            double v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount;
            var sCubed = s * s * s;
            var sSquared = s * s;

            var result = amount switch
            {
                0f => value1,
                1f => value2,
                _ => (2 * v1 - 2 * v2 + t2 + t1) * sCubed + (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared + t1 * s + v1
            };
            return (float)result;
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="value1">The first position in the interpolation.</param>
        /// <param name="value2">The second position in the interpolation.</param>
        /// <param name="value3">The third position in the interpolation.</param>
        /// <param name="value4">The fourth position in the interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
        {
            // Using formula from http://www.mvps.org/directx/articles/catmull/
            // Internally using doubles not to lose precission
            
            var amountSquared = amount * amount;
            var amountCubed = amountSquared * amount;

            return 0.5f * (2.0f * value2 +
                           (value3 - value1) * amount +
                           (2.0f * value1 - 5.0f * value2 + 4.0f * value3 - value4) * amountSquared +
                           (3.0f * value2 - value1 - 3.0f * value3 + value4) * amountCubed);
        }

        /// <summary>
        ///   <para>Same as Linear but makes sure the values interpolate correctly when they wrap around 360 degrees.</para>
        /// </summary>
        /// <param name="amount">Amount of progress (0-1)</param>
        public static float LinearAngleInDegrees(float angle1, float angle2, float amount)
        {
            var angle = MathFunctions.Repeat(angle2 - angle1, 360f);

            if (angle > 180f)
                angle -= 360f;
            
            return angle1 + angle * MathFunctions.Clamp01(amount);
        }
        /// <summary>
        ///   <para>Same as Linear but makes sure the values interpolate correctly when they wrap around 360 degrees.</para>
        /// </summary>
        /// <param name="amount">Amount of progress (0-1)</param>
        public static float LinearAngleInRadians(float angle1, float angle2, float amount)
        {
            var angle = MathFunctions.Repeat(angle2 - angle1, MathFunctions.Deg360InRad);

            if (angle > MathFunctions.Deg180InRad)
                angle -= MathFunctions.Deg360InRad;

            return angle1 + angle * MathFunctions.Clamp01(amount);
        }

        /// <summary>
        ///   <para>Moves a value current towards target.</para>
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="target">The value to move towards.</param>
        /// <param name="maxDelta">The maximum change that should be applied to the value.</param>
        public static float MoveTowards(float current, float target, float maxDelta)
        {
            return System.Math.Abs(target - current) <= maxDelta
                ? target
                : current + MathFunctions.Sign(target - current) * maxDelta;
        }

        /// <summary>
        ///   <para>Same as MoveTowards but makes sure the values interpolate correctly when they wrap around 360 degrees.</para>
        /// </summary>
        /// <param name="current">Current value</param>
        /// <param name="target">Target value</param>
        /// <param name="maxDelta">The maximum change that should be applied to the value</param>
        public static float MoveTowardsAngleInDegrees(float current, float target, float maxDelta)
        {
            var angle = MathFunctions.DeltaAngleInDegrees(current, target);
            
            if (-maxDelta < angle && angle < maxDelta)
                return target;
            
            return MoveTowards(current, current + angle, maxDelta);
        }
        /// <summary>
        ///   <para>Same as MoveTowards but makes sure the values interpolate correctly when they wrap around 360 degrees.</para>
        /// </summary>
        /// <param name="current">Current value</param>
        /// <param name="target">Target value</param>
        /// <param name="maxDelta">The maximum change that should be applied to the value</param>
        public static float MoveTowardsAngleInRadians(float current, float target, float maxDelta)
        {
            var angle = MathFunctions.DeltaAngleInRadians(current, target);

            if (-maxDelta < angle && angle < maxDelta)
                return target;
            
            return MoveTowards(current, current + angle, maxDelta);
        }
        
        /// <summary>
        /// Gradually changes an angle given in degrees towards a desired goal angle over time. The value is smoothed by some spring-damper like function.The function can be used to smooth any kind of value, positions, colors, scalars.
        /// </summary>
        /// <param name="current">The current position</param>
        /// <param name="target">The position we are trying to reach</param>
        /// <param name="currentVelocity">The current velocity, this value is modified by the function every time you call it</param>
        /// <param name="smoothTime">Approximately the time it will take to reach the target. A smaller value will reach the target faster.</param>
        /// <param name="deltaTime">The time since the last call to this function</param>
        /// <returns>Changed value, use it in continues calls as a <paramref name="current"/> to reach target</returns>
        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float deltaTime)
        {
            return SmoothDampAngle(current, target, ref currentVelocity, smoothTime, float.PositiveInfinity, deltaTime);
        }

        /// <summary>
        /// Gradually changes an angle given in degrees towards a desired goal angle over time. The value is smoothed by some spring-damper like function.The function can be used to smooth any kind of value, positions, colors, scalars.
        /// </summary>
        /// <param name="current">The current position</param>
        /// <param name="target">The position we are trying to reach</param>
        /// <param name="currentVelocity">The current velocity, this value is modified by the function every time you call it</param>
        /// <param name="smoothTime">Approximately the time it will take to reach the target. A smaller value will reach the target faster.</param>
        /// <param name="maxSpeed">Optionally allows you to clamp the maximum speed</param>
        /// <param name="deltaTime">The time since the last call to this function</param>
        /// <returns>Changed value, use it in continues calls as a <paramref name="current"/> to reach target</returns>
        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            target = current + MathFunctions.DeltaAngleInDegrees(current, target);
            
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        /// <summary>
        /// Gradually changes a value towards a desired goal over time. The value is smoothed by some spring-damper like function, which will never overshoot. The function can be used to smooth any kind of value, positions, colors, scalars.
        /// </summary>
        /// <param name="current">The current position</param>
        /// <param name="target">The position we are trying to reach</param>
        /// <param name="currentVelocity">The current velocity, this value is modified by the function every time you call it</param>
        /// <param name="smoothTime">Approximately the time it will take to reach the target. A smaller value will reach the target faster.</param>
        /// <param name="deltaTime">The time since the last call to this function</param>
        /// <returns>Changed value, use it in continues calls as a <paramref name="current"/> to reach target</returns>
        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float deltaTime)
        {
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, float.PositiveInfinity, deltaTime);
        }
        
        /// <summary>
        /// Gradually changes a value towards a desired goal over time. The value is smoothed by some spring-damper like function, which will never overshoot. The function can be used to smooth any kind of value, positions, colors, scalars.
        /// </summary>
        /// <param name="current">The current position</param>
        /// <param name="target">The position we are trying to reach</param>
        /// <param name="currentVelocity">The current velocity, this value is modified by the function every time you call it</param>
        /// <param name="smoothTime">Approximately the time it will take to reach the target. A smaller value will reach the target faster.</param>
        /// <param name="maxSpeed">Optionally allows you to clamp the maximum speed</param>
        /// <param name="deltaTime">The time since the last call to this function</param>
        /// <returns>Changed value, use it in continues calls as a <paramref name="current"/> to reach target</returns>
        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = System.Math.Max(0.0001f, smoothTime);
            
            var num1 = 2f / smoothTime;
            var num2 = num1 * deltaTime;
            var num3 = 1f / (1f + num2 + 0.47999998927116394f * num2 * num2 + 0.23499999940395355f * num2 * num2 * num2);
            var num4 = current - target;
            var num5 = target;
            var max = maxSpeed * smoothTime;
            var num6 = System.Math.Clamp(num4, -max, max);
            
            target = current - num6;
            
            var num7 = (currentVelocity + num1 * num6) * deltaTime;

            currentVelocity = (currentVelocity - num1 * num7) * num3;
            
            var num8 = target + (num6 + num7) * num3;
            
            if (num5 - current > 0f == num8 > num5)
            {
                num8 = num5;
                currentVelocity = (num8 - num5) / deltaTime;
            }

            return num8;
        }
    }
}