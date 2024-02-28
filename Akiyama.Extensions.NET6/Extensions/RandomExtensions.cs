namespace Akiyama.Extensions
{

    // TODO: Add a secure random generator
    // https://stackoverflow.com/questions/42426420/how-can-i-generate-a-cryptographically-secure-random-integer-within-a-range

    public static partial class RandomExtensions
    {
        /// <summary>
        /// Returns random entry from the specified <see cref="IEnumerable{T}"/>.
        /// <br />If the <see cref="IEnumerable{T}"/> contains only one entry then this method will return that entry.
        /// <br />Throws <see cref="ArgumentException"/> if the <see cref="IEnumerable{T}"/> is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random"></param>
        /// <param name="choices">The <see cref="IEnumerable{T}"/> to pick a value from</param>
        /// <returns>A random entry from the <paramref name="choices"/> <see cref="IEnumerable{T}"/></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T Choice<T>(this Random random, IEnumerable<T> choices)
        {
            if (choices.Count() == 0) { throw new ArgumentException("Provided list of options was empty."); }
            if (choices.Count() == 1) { return choices.First(); }
            int index = random.Next(choices.Count());
            return choices.ElementAt(index);
        }

        /// <summary>
        /// Returns a non-negative random integer between 0 and <paramref name="maxVal"/> inclusive.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="maxVal">The maximum possible return value</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int NextInclusive(this Random random, int maxVal) => NextInclusive(random, 0, maxVal);
        /// <summary>
        /// Returns a non-negative random integer between <paramref name="minVal"/> and <paramref name="maxVal"/> inclusive.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="minVal">The minimum possible return value</param>
        /// <param name="maxVal">The maximum possible return value</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int NextInclusive(this Random random, int minVal, int maxVal)
        {
            return random.Next(minVal, maxVal + 1);
        }

#if NET5_0_OR_GREATER
        // UTODO: docstring
        public static int NextSecure(this Random r, int maxVal) => NextSecure(r, 0, maxVal);
        public static int NextSecure(this Random random, int minVal, int maxVal) => System.Security.Cryptography.RandomNumberGenerator.GetInt32(minVal, maxVal);

#endif


#if NET6_0_OR_GREATER
        /// <summary>
        /// Returns a non-negative random 64-bit integer between 0 and <paramref name="maxVal"/> inclusive.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="maxVal">The maximum possible return value</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static long NextInclusive(this Random r, long maxVal) => NextInclusive(r, 0L, maxVal);
        /// <summary>
        /// Returns a non-negative random 64-bit integer between <paramref name="minVal"/> and <paramref name="maxVal"/> inclusive.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="maxVal">The maximum possible return value</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static long NextInclusive(this Random r, long minVal, long maxVal)
        {
            return r.NextInt64(minVal, maxVal + 1L);
        }
#endif

    }
}
