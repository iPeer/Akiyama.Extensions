using System;

namespace AkiyamaExtensions.Extensions
{
    public static partial class StringExtensions
    {

        /// <summary>
        /// Remove all whitespace from this <see cref="String"/>.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>This string with all whitespace removed.</returns>
        public static string RemoveWhitespace(this string s)
        {
            return string.Join("", s.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        /// <summary>
        /// Return a random character from within a <see cref="String"/>.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="allowWhitespace">Should whitespace characters be returned?</param>
        /// <returns>A random character from the specified <see cref="String"/>.</returns>
        public static char RandomChar(this string s, bool allowWhitespace = true) => RandomChar(s, new Random(), allowWhitespace);
        /// <summary>
        /// Return a random character from within a <see cref="String"/>.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="randomSeed">The seed to use for the <see cref="Random"/> instance.</param>
        /// <param name="allowWhitespace">Should whitespace characters be returned?</param>
        /// <returns>A random character from the specified <see cref="String"/>.</returns>
        public static char RandomChar(this string s, int randomSeed, bool allowWhitespace = true) => RandomChar(s, new Random(randomSeed), allowWhitespace);
        /// <summary>
        /// Return a random character from within a <see cref="String"/>.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="random">A pre-defined instance of <see cref="Random"/> to use for picking the <see cref="char"/>.</param>
        /// <param name="allowWhitespace">Should whitespace characters be returned?</param>
        /// <returns>A random character from the specified <see cref="String"/>.</returns>
        public static char RandomChar(this string s, Random random, bool allowWhitespace = true)
        {
            char[] charArray = (allowWhitespace ? s : s.RemoveWhitespace()).ToCharArray();
            return charArray.Choice(random);
        }

    }
}
