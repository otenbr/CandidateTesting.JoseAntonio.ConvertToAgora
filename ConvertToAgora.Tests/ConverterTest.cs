using Microsoft.VisualStudio.TestTools.UnitTesting;
using CandidateTesting.JoseAntonio.ConvertToAgora.Templates;
using CandidateTesting.JoseAntonio.ConvertToAgora.Converters;
using System.Collections.Generic;

namespace CandidateTesting.JoseAntonio.ConvertToAgora.Tests
{
    [TestClass]
    public class ConverterTest
    {
        [TestMethod]
        public void MustBePossibleToConvertMinhaCdnRecordToAgoraRecord()
        {
            string minhaCdnRecord = "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2";

            string expectedAgoraRecord = "\"Minha CDN\"	GET	200	/robots.txt	100	312	HIT";

            MinhaCdnTemplate minhaCdnTemplate = new MinhaCdnTemplate(minhaCdnRecord);
            AgoraTemplate agoraTemplate = MinhaCdnToAgoraConverter.ToAgoraTemplate(minhaCdnTemplate);

            Assert.AreEqual(expectedAgoraRecord, agoraTemplate.ToString());
        }

        [TestMethod]
        public void MustBePossibleToConvertAListOfMinhaCdnRecordToAgoraRecord()
        {
            List<string> minhaCdnRecords = new List<string>
            {
                "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2",
                "101|200|MISS|\"POST /myImages HTTP/1.1\"|319.4",
                "199|404|MISS|\"GET /not-found HTTP/1.1\"|142.9",
                "312|200|INVALIDATE|\"GET /robots.txt HTTP/1.1\"|245.1"
            };

            List<string> expectedAgoraRecords = new List<string>
            {
                "\"Minha CDN\"	GET	200	/robots.txt	100	312	HIT",
                "\"Minha CDN\"	POST	200	/myImages	319	101	MISS",
                "\"Minha CDN\"	GET	404	/not-found	143	199	MISS",
                "\"Minha CDN\"	GET	200	/robots.txt	245	312	INVALIDATE",
            };

            List<MinhaCdnTemplate> minhaCdnTemplates = new List<MinhaCdnTemplate>();
            foreach (string minhaCdnRecord in minhaCdnRecords)
            {
                minhaCdnTemplates.Add(new MinhaCdnTemplate(minhaCdnRecord));
            }

            List<AgoraTemplate> agoraTemplates = MinhaCdnToAgoraConverter.ToAgoraTemplate((minhaCdnTemplates));

            for (int i = 0; i < agoraTemplates.Count; i++)
            {
                Assert.AreEqual(expectedAgoraRecords[i], agoraTemplates[i].ToString());
            }            
        }
    }
}
