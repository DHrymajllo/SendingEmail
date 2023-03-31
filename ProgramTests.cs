#pragma warning disable IDE0005
using ElasticEmailClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;
using System;

namespace SendingEmail
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Upload_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var program = new Program();
            string actionUrl = null;
            NameValueCollection values = null;
            Stream[] paramFileStream = null;
            string[] filenames = null;

            // Act
            var result = Program.Upload(
                actionUrl,
                values,
                paramFileStream,
                filenames);

            // Assert
            Assert.Fail();
        }
    }
}
