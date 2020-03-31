using CandidateTesting.JoseAntonio.ConvertToAgora.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CandidateTesting.JoseAntonio.ConvertToAgora.Tests
{
    [TestClass]
    public class MinhaCdnTemplateTest
    {
        [TestMethod]
        public void ObjectCreatedFromStringShouldGenerateTheSameString()
        {
            string agoraRecord = "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2";
            MinhaCdnTemplate agoraTemplate = new MinhaCdnTemplate(agoraRecord);

            Assert.AreEqual("312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2", agoraTemplate.ToString());
        }

        [TestMethod]
        public void ObjectCreatedFromInvalidStringShouldThrowException()
        {
            // String without HTTP Status Code
            string agoraRecord = "312|HIT|\"GET /robots.txt HTTP/1.1\"|100.2";

            Assert.ThrowsException<Exception>(() => new MinhaCdnTemplate(agoraRecord));
        }
    }
}
