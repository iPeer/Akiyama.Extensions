using System.Collections.Generic;
using System;
using System.Linq;
using Akiyama.Helpers;

namespace Akiyama.Extensions
{
    public static partial class IEnumerableExtensions
    {

        /// <summary>
        /// Returns a random entry from <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/> from which the choice is made.</param>
        /// <returns>A random entry from <paramref name="enumerable"/>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static T Choice<T>(this IEnumerable<T> enumerable) => Choice(enumerable, new Random());
        /// <summary>
        /// Returns a random entry from <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/> from which the choice is made.</param>
        /// <param name="randomSeed">The seed to supply to the <see cref="Random"/> instance.</param>
        /// <returns>A random entry from <paramref name="enumerable"/>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static T Choice<T>(this IEnumerable<T> enumerable, int randomSeed) => Choice(enumerable, new Random(randomSeed));
        /// <summary>
        /// Returns a random entry from <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/> from which the choice is made.</param>
        /// <param name="random">The instance of an already created <see cref="Random"/> to use to choose an entry.</param>
        /// <returns>A random entry from <paramref name="enumerable"/>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static T Choice<T>(this IEnumerable<T> enumerable, Random random)
        {
            return random.Choice(enumerable);
        }

        public static T SecureChoice<T>(this IEnumerable<T> enumerable)
        {
            // UTODO: Implement non-NET Framework version
#if NETSTANDARD2_1_OR_GREATER
#else
            using (SecureRandom sr = new SecureRandom())
            {
                int index = sr.Next(enumerable.Count(), inclusive: false);
                return enumerable.ElementAt(index);
            }
#endif
        }

    }
}
