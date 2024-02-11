using Microsoft.VisualStudio.TestTools.UnitTesting;
using NETFramework.Tests.WinFormsTests;
using System.Windows.Forms;
using AkiyamaExtensions.Extensions.WinForms;
using System.Collections.Generic;

namespace NETFramework.Tests
{
    /// <summary>
    /// Summary description for FormTests
    /// </summary>
    [TestClass]
    public class FormTests
    {
        public FormTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestCountGetAllControls()
        {
            using (Form a = new TestForm())
            {
                List<Control> l = a.GetAllControls();
                foreach (Control c in l)
                {
                    TestContext.WriteLine(c.Name == string.Empty ? $"{{blank}} ({c.GetType()})" : c.Name);
                }
                Assert.AreEqual(17, l.Count);
            }
        }

        [TestMethod]
        public void TestCountGetAllControlsOfType_Test1()
        {
            using (Form a = new TestForm())
            {
                List<Control> l = a.GetAllControls<TextBox>();
                foreach (Control c in l)
                {
                    TestContext.WriteLine(c.Name == string.Empty ? $"{{blank}} ({c.GetType()})" : c.Name);
                }
                // This is 4 because System.Windows.Forms.NumericUpDown contains its own element which counts as a text box
                Assert.AreEqual(4, l.Count);
            }
        }

        [TestMethod]
        public void TestCountGetAllControlsOfType_Test2()
        {
            using (Form a = new TestForm())
            {
                List<Control> l = a.GetAllControls<Label>();
                foreach (Control c in l)
                {
                    TestContext.WriteLine(c.Name == string.Empty ? $"{{blank}} ({c.GetType()})" : c.Name);
                }
                Assert.AreEqual(4, l.Count);
            }
        }

    }
}
