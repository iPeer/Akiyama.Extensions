namespace AkiyamaExtensions.Extensions
{
    public static partial class NumberExtensions
    {
        /// <summary>
        /// Returns whether the current integer is within the range provided.
        /// <br />This test is <i>inclusive</i> of both <paramref name="min"/> and <paramref name="max"/> unless <paramref name="inclusive"/> is <b>false</b>.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="min">The bottom of the range.</param>
        /// <param name="max">The top of the range</param>
        /// <param name="inclusive">Whether the check should be inclusive of <paramref name="min"/> and <paramref name="max"/> (default <b>true</b>).</param>
        /// <returns><b>true</b> if this integer falls within the specified range, otherwise <b>false</b>.</returns>
        public static bool InRange(this long i, long min, long max, bool inclusive = true)
        {
            if (inclusive)
            {
                return i >= min && i <= max;
            }
            else
            {
                return i > min && i < max;
            }
        }

        /// <summary>
        /// Returns a <see cref="long"/> clamped to <paramref name="maxValue"/>.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="maxValue">The maximum value allowed</param>
        /// <returns><paramref name="maxValue"/> if the current <see cref="long"/> is greater than <paramref name="maxValue"/>, otherwise returns the current <see cref="long"/>.</returns>
        public static long Clamp(this long i, long maxValue)
        {
            if (i > maxValue) { return maxValue; }
            return i;
        }

        /// <summary>
        /// Returns a <see cref="long"/> clamped to <paramref name="minValue"/> and <paramref name="maxValue"/>.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="minValue">The minimum value allowed</param>
        /// <param name="maxValue">The maximum value allowed</param>
        /// <returns><paramref name="minValue"/> if the current <see cref="long"/> is less than <paramref name="minValue"/>, <paramref name="maxValue"/> if the current <see cref="long"/> is greater than <paramref name="maxValue"/>, or the current <see cref="long"/> if neither.</returns>
        public static long Clamp(this long i, long minValue, long maxValue)
        {
            if (i < minValue) { return minValue; }
            else if (i > maxValue) { return maxValue; }
            return i;
        }
    }
}

