using System;
using System.Collections.Generic;
using System.Globalization;

namespace CandidateTesting.JoseAntonio.ConvertToAgora.Templates
{
    /// <summary>
    /// Class that represent the "MINHA CDN" format
    /// </summary>
    public class MinhaCdnTemplate : IBaseTemplate
    {
        public int ResponseSize { get; private set; }
        public int StatusCode { get; private set; }
        public string CacheStatus { get; private set; }
        public string Header { get; private set; }
        public double TimeTaken { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="responseSize">Response size</param>
        /// <param name="statusCode">HTTP status code</param>
        /// <param name="cacheStatus">Status of cache</param>
        /// <param name="header">Header. Contains: HTTP Method, Path, Protocol </param>
        /// <param name="timeTaken">Time taken by the request</param>
        public MinhaCdnTemplate(int responseSize, int statusCode, string cacheStatus, string header, double timeTaken)
        {
            ResponseSize = responseSize;
            StatusCode = statusCode;
            CacheStatus = cacheStatus;
            Header = header;
            TimeTaken = timeTaken;
        }

        /// <summary>
        /// Constructor for use of an representation string 
        /// </summary>
        /// <param name="line">Representation for the record</param>
        public MinhaCdnTemplate(string line)
        {
            ValidateLine(line);
        }

        /// <summary>
        /// Return an enumarable of the fields in the right order.
        /// </summary>
        /// <returns>Enumarable of fields</returns>
        public IEnumerable<object> GetFields()
        {
            yield return ResponseSize;
            yield return StatusCode;
            yield return CacheStatus;
            yield return $"\"{Header}\"";
            yield return TimeTaken;
        }

        /// <summary>
        /// Return an formated string with the field values separated by pipe.
        /// </summary>
        /// <returns>Formated string</returns>
        public override string ToString()
        {
            return string.Join("|", GetFields());
        }

        /// <summary>
        /// If the line is a valid structure for the template, the object is created.
        /// </summary>
        /// <param name="line">Representation for the record</param>
        public void ValidateLine(string line)
        {
            int responseSize, statusCode;
            string cacheStatus, header;
            double timeTaken;

            try
            {
                string[] fields = line.Split("|");

                responseSize = Convert.ToInt32(fields[0]);
                statusCode = Convert.ToInt32(fields[1]);
                cacheStatus = fields[2];
                header = fields[3].Replace("\"", "");
                timeTaken = Convert.ToDouble(fields[4], new CultureInfo("en-US"));
            }
            catch
            {
                throw new Exception($"The line \"{line}\" is invalid for the \"MINHA CDN\" format.");
            }

            ResponseSize = responseSize;
            StatusCode = statusCode;
            CacheStatus = cacheStatus;
            Header = header;
            TimeTaken = timeTaken;
        }
    }
}
