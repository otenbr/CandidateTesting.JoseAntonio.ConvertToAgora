using CandidateTesting.JoseAntonio.ConvertToAgora.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CandidateTesting.JoseAntonio.ConvertToAgora.Tests
{
    [TestClass]
    public class AgoraTemplateTest
    {
        [TestMethod]
        public void ObjectCreatedFromStringShouldGenerateTheSameString()
        {
            string agoraRecord = "\"Minha CDN\"	GET	200	/robots.txt	100	312	HIT";
            AgoraTemplate agoraTemplate = new AgoraTemplate(agoraRecord);

            Assert.AreEqual("\"Minha CDN\"	GET	200	/robots.txt	100	312	HIT", agoraTemplate.ToString());
        }

        [TestMethod]
        public void ObjectCreatedFromInvalidStringShouldThrowException()
        {
            // String without HTTP Method
            string agoraRecord = "\"Minha CDN\"	200	/robots.txt	100	312	HIT";

            Assert.ThrowsException<Exception>(() => new AgoraTemplate(agoraRecord));
        }
    }
}
