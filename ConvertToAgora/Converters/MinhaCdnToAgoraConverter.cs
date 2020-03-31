using CandidateTesting.JoseAntonio.ConvertToAgora.Templates;
using System.Collections.Generic;

namespace CandidateTesting.JoseAntonio.ConvertToAgora.Converters
{
    /// <summary>
    /// Class that encapsulate the methods for convertion of records from MinhaCdnTemplate to AgoraTemplate
    /// </summary>
    public class MinhaCdnToAgoraConverter
    {
        /// <summary>
        /// Method that convert one record from MinhaCdnTemplate to AgoraTemplate 
        /// </summary>
        /// <param name="minhaCdnRecord">Record to be converted</param>
        /// <returns>Converted record</returns>
        public static AgoraTemplate ToAgoraTemplate(MinhaCdnTemplate minhaCdnRecord)
        {
            string[] headers = minhaCdnRecord.Header.Split(" ");

            string httpMethod = headers[0];
            string uriPath = headers[1];
            int timeTaken = (int)System.Math.Round(minhaCdnRecord.TimeTaken);

            return new AgoraTemplate("Minha CDN", httpMethod, minhaCdnRecord.StatusCode, uriPath, timeTaken, minhaCdnRecord.ResponseSize, minhaCdnRecord.CacheStatus);
        }

        /// <summary>
        /// Method that convert a list of records from MinhaCdnTemplate to AgoraTemplate
        /// </summary>
        /// <param name="minhaCdnRecord">List of records to be converted</param>
        /// <returns>List of converted records</returns>
        public static List<AgoraTemplate> ToAgoraTemplate(List<MinhaCdnTemplate> minhaCdnRecords)
        {
            List<AgoraTemplate> agoraRecords = new List<AgoraTemplate>();

            foreach (MinhaCdnTemplate minhaCdnRecord in minhaCdnRecords)
            {
                AgoraTemplate agoraRecord = ToAgoraTemplate(minhaCdnRecord);

                agoraRecords.Add(agoraRecord);
            }

            return agoraRecords;
        }
    }
}