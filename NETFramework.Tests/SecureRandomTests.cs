using AkiyamaExtensions.Extensions;
using AkiyamaExtensions.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NETFramework.Tests
{
    [TestClass]
    public class SecureRandomTests
    {

        private TestContext _testContext;

        private const int MAX_NUMBER_OF_TESTS = 5;

        public TestContext TestContext
        {
            get { return this._testContext; }
            set { this._testContext = value; }
        }

        [TestMethod]
        public void TestSecureChoice()
        {
            using (SecureRandom sr = new SecureRandom())
            {
                string[] choices = new string[] { "an", "array", "of", "varying", "strings", "used", "to", "test", "secure", "random's", ".Choice()" };
                for (int x = 0; x < MAX_NUMBER_OF_TESTS; x++)
                {
                    string choice = sr.Choice(choices);
                    this.TestContext.WriteLine(choice);
                    Assert.IsNotNull(choice);
                }
            }
        }

        [TestMethod]
        public void TestSecureChoiceEnumerable()
        {
            string[] choices = new string[] { "an", "array", "of", "varying", "strings", "used", "to", "test", "secure", "random's", ".Choice()" };
            for (int x = 0; x < MAX_NUMBER_OF_TESTS; x++)
            {
                string choice = choices.SecureChoice();
                this.TestContext.WriteLine(choice);
                Assert.IsNotNull(choice);
            }
        }

        [TestMethod]
        public void TestSecureChoiceInvalidEmptyEnumerable()
        {
            using (SecureRandom sr = new SecureRandom())
            {
                Assert.ThrowsException<ArgumentException>(() => { return sr.Choice(new string[0]); });
            }
        }

        [TestMethod]
        public void TestSecureChoiceInvalidNullEnumerable()
        {
            using (SecureRandom sr = new SecureRandom())
            {
                Assert.ThrowsException<ArgumentNullException>(() => { return sr.Choice<string>(items: null); });
            }
        }

        [TestMethod]
        public void Next()
        {

            using (SecureRandom sr = new SecureRandom())
            {
                for (int x = 0; x < MAX_NUMBER_OF_TESTS; x++)
                {
                    int next = sr.Next();
                    this.TestContext.WriteLine($"{next}");
                    Assert.IsTrue(next > 0);
                }
            }

        }

        [TestMethod]
        public void NextWithMax()
        {
            using (SecureRandom sr = new SecureRandom())
            {
                for (int x = 0; x < MAX_NUMBER_OF_TESTS; x++)
                {
                    int max = 3;
                    long next = sr.Next(max);
                    this.TestContext.WriteLine($"{next} (limit: {max})");
                    Assert.IsFalse(next > max);
                }
            }
        }

        [TestMethod]
        public void NextInclusiveWithMaxAtMaxValue()
        {
            using (SecureRandom sr = new SecureRandom())
            {
                for (int x = 0; x < MAX_NUMBER_OF_TESTS; x++)
                {
                    int max = int.MaxValue;
                    int min = int.MaxValue - 1;
                    long next = sr.Next(min, max, inclusive: true);
                    this.TestContext.WriteLine($"{next} (limit: {max})");
                    Assert.IsFalse(next > max);
                }
            }
        }

        [TestMethod]
        public void NextWithMinMax()
        {
            using (SecureRandom sr = new SecureRandom())
            {
                for (int x = 0; x < MAX_NUMBER_OF_TESTS; x++)
                {
                    int max = 6;
                    int min = 2;
                    long next = sr.Next(min, max);
                    this.TestContext.WriteLine($"{next} (limits: low: {min} max: {max})");
                    Assert.IsFalse(next < min && next > max);
                }
            }
        }

        [TestMethod]
        public void RandomStringCollisionTests()
        {
            using (SecureRandom sr = new SecureRandom())
            {
                for (int x = 0; x < MAX_NUMBER_OF_TESTS; x++)
                {
                    string sameCheckLeft = sr.RandomString(25);
                    string sameCheckRight = sr.RandomString(25);
                    this.TestContext.WriteLine($"{sameCheckLeft} vs {sameCheckRight}");
                    Assert.AreNotSame(sameCheckLeft, sameCheckRight);
                }
            }
        }

        [TestMethod]
        public void RandomStringLengthTest()
        {
            using (SecureRandom sr = new SecureRandom())
            {
                for (int x = 0; x < MAX_NUMBER_OF_TESTS; x++)
                {
                    string lenTest = sr.RandomString(25);
                    this.TestContext.WriteLine($"\"{lenTest}\".Length == {lenTest.Length}");
                    Assert.IsTrue(sr.RandomString(25).Length == 25);
                }
            }
        }

        [TestMethod]
        public void RandomDouble()
        {
            using (SecureRandom sr = new SecureRandom())
            {
                for (int x = 0; x < MAX_NUMBER_OF_TESTS; x++)
                {
                    double next = sr.NextDouble();
                    this.TestContext.WriteLine($"{next}");
                    Assert.IsTrue(next <= 1.0);
                }
            }
        }

        [TestMethod]
        public void NextLong()
        {
            using (SecureRandom sr = new SecureRandom())
            {
                for (int x = 0; x < MAX_NUMBER_OF_TESTS; x++)
                {
                    long next = sr.NextLong();
                    this.TestContext.WriteLine($"{next}");
                    Assert.IsTrue(next > 0);
                }
            }
        }

        [TestMethod]
        public void NextLongWithMax()
        {
            using (SecureRandom sr = new SecureRandom())
            {
                for (int x = 0; x < MAX_NUMBER_OF_TESTS; x++)
                {
                    int longMax = 3;
                    long next = sr.NextLong(longMax);
                    this.TestContext.WriteLine($"{next} (limit: {longMax})");
                    Assert.IsFalse(next > longMax);
                }
            }
        }

        [TestMethod]
        public void NextLongWithMinMax()
        {
            using (SecureRandom sr = new SecureRandom())
            {
                for (int x = 0; x < MAX_NUMBER_OF_TESTS; x++)
                {
                    int longMax = 6;
                    int longMin = 2;
                    long next = sr.NextLong(longMin, longMax);
                    this.TestContext.WriteLine($"{next} (limits: low: {longMin} max: {longMax})");
                    Assert.IsFalse(next < longMin && next > longMax);
                }
            }
        }

        [TestMethod]
        public void NextBytesInvalidZeroLenBuffer()
        {
            using (SecureRandom sr = new SecureRandom())
            {
                byte[] b = new byte[0];
                Assert.ThrowsException<ArgumentException>(() => sr.NextBytes(b));
            }
        }
    }
}
