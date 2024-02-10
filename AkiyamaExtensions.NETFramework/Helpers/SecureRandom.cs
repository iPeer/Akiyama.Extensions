using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace AkiyamaExtensions.Helpers
{
    /// <summary>
    /// Represents a cryptographically safe pseudo-random number generator.
    /// </summary>
    public class SecureRandom : IDisposable
    {

        private bool _disposed = false;

        private readonly RNGCryptoServiceProvider _random;

        public readonly char[] AlphaNumericCharacters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public readonly char[] SpecialCharacters = "!\"£$%^&*()[]{};'#:@~,./<>?\\".ToCharArray();
        
        /// <summary>
        /// Initialises a new instance of the <see cref="SecureRandom"/> class
        /// </summary>
        public SecureRandom()
        {
            this._random = new RNGCryptoServiceProvider();
        }

        public T Choice<T>(IEnumerable<T> items)
        {
            if (items == null) {  throw new ArgumentNullException("items"); }
            if (items.Count() == 0) { throw new ArgumentException("'items' must contain at least one entry"); }
            if (items.Count() == 1) { return items.First(); }
            int index = Next(items.Count(), inclusive: false);
            return items.ElementAt(index);
        }

        /// <inheritdoc cref="Random.Next()"/>
        public int Next()
        {
            return Math.Abs((int)GetRandomUInt());
        }
        /// <summary>
        /// Cryptographically generate a random integer
        /// </summary>
        /// <param name="maxVal">The maximum value</param>
        /// <param name="inclusive">If <b>true</b>, includes <paramref name="maxVal"/> as a possible return value, otherwise behaves the same as <see cref="Random.Next(int)"/></param>
        /// <returns>A cryptographically safe random integer between 0 and <paramref name="maxVal"/>.<br />If <paramref name="inclusive"/> is <b>true</b>, this includes <paramref name="maxVal"/>, otherwise behaves the same as <see cref="Random.Next(int)"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int Next(int maxVal, bool inclusive = false) => Next(0, maxVal, inclusive);
        /// <summary>
        /// Cryptographically generate a random integer
        /// </summary>
        /// <param name="maxVal"></param>
        /// <param name="inclusive"></param>
        /// <returns>A cryptographically safe random integer between <paramref name="minVal"/> and <paramref name="maxVal"/>.<br />If <paramref name="inclusive"/> is <b>true</b>, this includes <paramref name="maxVal"/>, otherwise behaves the same as <see cref="Random.Next(int)"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int Next(int minVal, int maxVal, bool inclusive = false)
        {

            if (minVal == maxVal) { return minVal; }
            if (minVal > maxVal) { throw new ArgumentOutOfRangeException("Minimum value cannot exceed maximum value."); }

            // Convert the values to longs to deal with numbers that exceed int.MinValue or int.MaxValue
            // Is this the right approach? I honestly I have no idea, but it passes 100,000 tests
            long max = (long)maxVal + (inclusive ? 1 : 0);
            long min = (long)minVal;

            long diff = max - min;
            long upperBoundary = int.MaxValue / diff * diff;

            uint ret;
            do
            {
                ret = GetRandomUInt();
            } while (ret >= upperBoundary);

            return (int)(minVal + (ret % diff));

        }

        /// <summary>
        /// Generate a random non-negative <see cref="long"/>
        /// </summary>
        /// <returns>A non-negative cryptographically secure <see cref="long"/></returns>
        public long NextLong()
        {
            return Math.Abs((long)GetRandomULong());
        }
        /// <summary>
        /// Generates a cryptographically generated <see cref="long"/>.
        /// </summary>
        /// <param name="maxVal">The maximum value</param>
        /// <param name="inclusive">If <b>true</b>, includes <paramref name="maxVal"/> as a possible return value</param>
        /// <returns>A cryptographically secure <see cref="long"/> between 0 and <paramref name="maxVal"/>. If <paramref name="inclusive"/> is <b>true</b>, the result is inclusive of <paramref name="maxVal"/>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public long NextLong(long maxVal, bool inclusive = false) => NextLong(0, maxVal, inclusive);
        /// <summary>
        /// Generates a cryptographically generated <see cref="long"/>.
        /// </summary>
        /// <param name="minVal">The minimum value</param>
        /// <param name="maxVal">The maximum value</param>
        /// <param name="inclusive">If <b>true</b>, includes <paramref name="maxVal"/> as a possible return value</param>
        /// <returns>A cryptographically secure <see cref="long"/> between <paramref name="minVal"/> and <paramref name="maxVal"/>. If <paramref name="inclusive"/> is <b>true</b>, the result is inclusive of <paramref name="maxVal"/>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public long NextLong(long minVal, long maxVal, bool inclusive = false)
        {

            if (minVal == maxVal) { return minVal; }
            if (minVal > maxVal) { throw new ArgumentException("Minimum value cannot exceed maximum value."); }

            ulong range = (ulong)((maxVal + (inclusive ? 1 : 0)) - minVal);

            ulong rng;
            do
            {
                rng = GetRandomULong();

            } while (rng > ulong.MaxValue - ((ulong.MaxValue & range) + 1) % range);

            return (long)(rng % range) + minVal;

        }

        /// <summary>
        /// Generates a cryptographically secure random string of characters with the length <paramref name="length"/>.
        /// <br />If <paramref name="characters"/> is <b>not</b> null, the characters in that <see cref="IEnumerable{T}"/> will be used to pick characters from, otherwise 0-9 and A-Z (upper and lowercase) are used.
        /// <br /><br /><b>While this method is cryptographically secure, it should not be used for generating things such as passwords or passphrases.</b>
        /// </summary>
        /// <param name="length">The length of string to generate. Must be between 0 and <see cref="Int32.MaxValue"/></param>
        /// <param name="characters">An <see cref="IEnumerable{T}"/> of characters to use for the random generation. If <b>null</b>, a default of <c>0-9+A-Z+a-z</c> will be used.</param>
        /// <returns>A cryptographically secure random string of length <paramref name="length"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string RandomString(int length = 10, IEnumerable<char> characters = null)
        {
            if (length < 1) { throw new ArgumentOutOfRangeException("Length of requested string must be at least 1 character."); }
            char[] chars = new char[length];
            int x = 0;
            while (x < length)
            {
                int index = this.Next((characters ?? this.AlphaNumericCharacters).Count(), inclusive: false);
                char c = (characters ?? this.AlphaNumericCharacters).ElementAt(index);
                chars.SetValue(c, x++);
            }
            return new string(chars);
        }

        private uint GetRandomUInt()
        {
            byte[] rBytes = RandomBytes(sizeof(uint));
            return BitConverter.ToUInt32(rBytes, 0);
        }

        private ulong GetRandomULong()
        {
            byte[] rBytes = RandomBytes(sizeof(ulong));
            return BitConverter.ToUInt64(rBytes, 0);
        }

        /// <inheritdoc cref="Random.NextDouble()"/>
        public double NextDouble()
        {
            return (double)(GetRandomUInt() / (1.0d + uint.MaxValue));
        }

        /// <summary>
        /// Returns a <see cref="byte[]"/> of length <paramref name="length"/> containing random bytes that have been crytographically securely generated.
        /// </summary>
        /// <param name="length">The number of bytes to generate</param>
        /// <param name="NoZeroBytes">If <b>true</b>, excludes bytes that are of 0 in value</param>
        /// <returns>A cryptographically safe <see cref="byte[]"/> of length <paramref name="length"/> containing random bytes.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private byte[] RandomBytes(int length, bool NoZeroBytes = false)
        {
            if (length < 1) { throw new ArgumentOutOfRangeException("Length of requested array must be at least 1"); }
            byte[] buff = new byte[length];
            if (NoZeroBytes) { this._random.GetNonZeroBytes(buff); }
            else { this._random.GetBytes(buff); }
            return buff;
        }

        /// <summary>
        /// Fills <paramref name="buffer"/> with randomly generated bytes, starting from the beginning of the buffer
        /// </summary>
        /// <param name="buffer">The <see cref="byte"/> buffer to fill</param>
        /// <param name="NoZeroBytes">Should bytes with a value of 0 be excluded?</param>
        /// <exception cref="ArgumentException"></exception>
        public void NextBytes(byte[] buffer, bool NoZeroBytes = false)
        {
            if (buffer.Length < 1) { throw new ArgumentException("Specified buffer must be at least 1 byte long."); }
            if (NoZeroBytes) { this._random.GetNonZeroBytes(buffer); }
            else { this._random.GetBytes(buffer); }
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            if (!this._disposed)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) { return; }
            if (disposing)
            {
                _random?.Dispose();
            }
            this._disposed = true;
        }
    }
}
