using Microsoft.VisualStudio.TestTools.UnitTesting;
using CandidateTesting.JoseAntonio.ConvertToAgora.Templates;
using CandidateTesting.JoseAntonio.ConvertToAgora.Converters;

namespace CandidateTesting.JoseAntonio.ConvertToAgora.Tests
{
    [TestClass]
    public class ConverterTest
    {
        [TestMethod]
        public void ShoulBePossibleToConvertMinhCdnRecordToAgoraRecord()
        {
            string[] inputs = new string[]
            {
                "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2",
                "101|200|MISS|\"POST /myImages HTTP/1.1\"|319.4",
                "199|404|MISS|\"GET /not-found HTTP/1.1\"|142.9",
                "312|200|INVALIDATE|\"GET /robots.txt HTTP/1.1\"|245.1"
            };

            string[] expecteds = new string[]
            {
                "\"Minha CDN\"	GET	200	/robots.txt	100	312	HIT",
                "\"Minha CDN\"	POST	200	/myImages	319	101	MISS",
                "\"Minha CDN\"	GET	404	/not-found	143	199	MISS",
                "\"Minha CDN\"	GET	200	/robots.txt	245	312	INVALIDATE",
            };

            for(int i = 0; i < inputs.Length; i++)
            {
                MinhaCdnTemplate minhaCdnTemplate = new MinhaCdnTemplate(inputs[i]);

                AgoraTemplate agoraTemplate = MinhaCdnToAgoraConverter.ToAgoraTemplate(minhaCdnTemplate);

                Assert.AreEqual(expecteds[i], agoraTemplate.ToString());
            }
        }
    }
}
