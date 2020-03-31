using System;
using System.Collections.Generic;

namespace CandidateTesting.JoseAntonio.ConvertToAgora.Templates
{
    /// <summary>
    /// Class that represent the "Agora" format
    /// </summary>
    public class AgoraTemplate : IBaseTemplate
    {
        public string Provider { get; private set; }
        public string HttpMethod { get; private set; }
        public int StatusCode { get; private set; }
        public string UriPath { get; private set; }
        public int TimeTaken { get; private set; }
        public int ResponseSize { get; private set; }
        public string CacheStatus { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="responseSize">Response size</param>
        /// <param name="responseSize">Response size</param>
        /// <param name="statusCode">HTTP status code</param>
        /// <param name="cacheStatus">Status of cache</param>
        /// <param name="header">Header. Contains: HTTP Method, Path, Protocol </param>
        /// <param name="timeTaken">Time taken by the request</param>
        public AgoraTemplate(string provider, string httpMethod, int statusCode, string uriPath, int timeTaken, int responseSize, string cacheStatus)
        {
            Provider = provider;
            HttpMethod = httpMethod;
            StatusCode = statusCode;
            UriPath = uriPath;
            TimeTaken = timeTaken;
            ResponseSize = responseSize;
            CacheStatus = cacheStatus;
        }

        /// <summary>
        /// Constructor for use of an representation string 
        /// </summary>
        /// <param name="line">Representation for the record</param>
        public AgoraTemplate(string line)
        {
            ValidateLine(line);
        }

        /// <summary>
        /// Return an enumarable of the fields in the right order.
        /// </summary>
        /// <returns>Enumarable of fields</returns>
        public IEnumerable<object> GetFields()
        {
            yield return $"\"{Provider}\"";
            yield return HttpMethod;
            yield return StatusCode;
            yield return UriPath;
            yield return TimeTaken;
            yield return ResponseSize;
            yield return CacheStatus;
        }

        /// <summary>
        /// Return an formated string with the field values separated by tabulation.
        /// </summary>
        /// <returns>Formated string</returns>
        public override string ToString()
        {
            return string.Join("\t", GetFields());
        }

        /// <summary>
        /// If the line is a valid structure for the template, the object is created.
        /// </summary>
        /// <param name="line">Representation for the record.</param>
        public void ValidateLine(string line)
        {
            string[] fields = line.Split("\t");

            string provider, httpMethod, uriPath, cacheStatus;
            int statusCode, timeTaken, responseSize;

            try
            {
                fields = line.Split("\t");

                provider = fields[0];
                httpMethod = fields[1];
                statusCode = Convert.ToInt32(fields[2]);
                uriPath = fields[3];
                timeTaken = Convert.ToInt32(fields[4]);
                responseSize = Convert.ToInt32(fields[5]);
                cacheStatus = fields[6];
            }
            catch
            {
                throw new Exception($"The line \"{line}\" is invalid for the \"Agora\" format.");
            }

            Provider = provider;
            HttpMethod = httpMethod;
            StatusCode = statusCode;
            UriPath = uriPath;
            TimeTaken = timeTaken;
            ResponseSize = responseSize;
            CacheStatus = cacheStatus;
        }
    }
}
