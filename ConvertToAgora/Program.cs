using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;
using CandidateTesting.JoseAntonio.ConvertToAgora.Templates;
using CandidateTesting.JoseAntonio.ConvertToAgora.Converters;

namespace CandidateTesting.JoseAntonio.ConvertToAgora
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                DisplayErrorMessage("The program expects at least one parameter.");
                return;
            }

            // Display help message
            if (args.Length == 1 && (args[0] == "-h" || args[0] == "--help" || args[0] == "?"))
            {
                DisplayHelpMessage();
                return;
            }

            // Get the url for source file
            string sourceUrl = args[0];
            // Get the path for destination of the converted file.
            string targetPath = args.Length > 1 ? args[1] : Path.GetFileName(sourceUrl);

            string fileContent;
            try
            {
                // Get the content of source file
                fileContent = GetFileContentFromUrl(sourceUrl);
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex.Message);
                return;
            }

            // Extract the source records
            List<MinhaCdnTemplate> minhaCdnRecords = ExtractMinhaCdnRecords(fileContent);
            // Convert records to the Agora template
            List<AgoraTemplate> agoraRecords = MinhaCdnToAgoraConverter.ToAgoraTemplate(minhaCdnRecords);

            // Verify if the informed path is a directory, if true the directory wil be created.
            if (!string.IsNullOrEmpty(Path.GetDirectoryName(targetPath)) && !Directory.Exists(Path.GetDirectoryName(targetPath)))
            {
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
                }
                catch(Exception ex)
                {
                    DisplayErrorMessage(ex.Message);
                    return;
                }
            }

            // Write the agora file content to the targetPath.
            File.WriteAllText(targetPath, GenerateAgoraFileContent(agoraRecords));

            DisplaySuccessfullyCreatedMessage(targetPath);
        }

        /// <summary>
        /// Method that receive a content of file and return a list of records in the MinhaCdn format.
        /// </summary>
        /// <param name="fileContent">Content of source file</param>
        /// <returns>List of records</returns>
        private static List<MinhaCdnTemplate> ExtractMinhaCdnRecords(string fileContent)
        {
            List<MinhaCdnTemplate> minhaCdnRecords = new List<MinhaCdnTemplate>();
            foreach (string line in fileContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                MinhaCdnTemplate minhaCdn = new MinhaCdnTemplate(line);

                minhaCdnRecords.Add(minhaCdn);
            }

            return minhaCdnRecords;
        }

        /// <summary>
        /// Method that generate the content of file in the Agora format.
        /// </summary>
        /// <param name="records">List of records</param>
        /// <returns>File content</returns>
        static string GenerateAgoraFileContent(List<AgoraTemplate> records)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("#Version: 1.0");
            builder.AppendLine($"#Date: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            builder.AppendLine("#Fields: provider http-method status-code uri-path time-taken response-size cache-status");

            foreach (AgoraTemplate record in records)
            {
                builder.AppendLine(record.ToString());
            }

            return builder.ToString();
        }

        /// <summary>
        /// Method that get the content of file from the source url.
        /// </summary>
        /// <param name="sourceUrl">Url of source file</param>
        /// <returns>File content</returns>
        static string GetFileContentFromUrl(string sourceUrl)
        {
            string fileContent;

            try
            {
                HttpWebRequest request = HttpWebRequest.CreateHttp(sourceUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        fileContent = reader.ReadToEnd();

                        reader.Close();
                    }

                    response.Close();
                }

            }
            catch (UriFormatException ex)
            {
                throw new Exception("The format of source url is not valid.", ex.InnerException);
            }
            catch (WebException ex)
            {
                throw new Exception("It was not possible to get the file content.", ex.InnerException);
            }

            return fileContent;
        }

        /// <summary>
        /// Message displayed if the program is called without parameters.
        /// </summary>
        static void DisplayErrorMessage(string message)
        {
            Console.Error.WriteLine($@"
{message}

For help, type:
    ConvertToAgora -h
    ConvertToAgora --help
    ConvertToAgora ?
");
        }
       
        /// <summary>
        /// Message displayed if the user call for help.
        /// </summary>
        static void DisplayHelpMessage()
        {
            Console.Out.WriteLine(@"
DESCRIPTION
    Convert the content of the url given for the file in Agora format.

SYNTAX
    ConvertToAgora sourceUrl [targetPath]

PARAMETERS
    sourceUrl       Url of the file to be converted.
    targetPath      Specifies the directory and/or filename of the destination for converted file.
                    If the targetPath not be informed, will be assumed the filename for sourceUrl.

EXAMPLE
    ConvertToAgora http://logstorage.com/minhaCdn1.txt
    ConvertToAgora http://logstorage.com/minhaCdn1.txt ./output/minhaCdn1.txt
");
        }

        /// <summary>
        /// Message displayed if the file is successfully created.
        /// </summary>
        static void DisplaySuccessfullyCreatedMessage(string targetPath)
        {
            Console.Out.WriteLine($@"
The file was successfully created in the path: {targetPath} 
");
        }
    }
}
