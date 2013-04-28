using System;
using System.IO;
using System.Net;
using System.Web;

namespace SkyDriveAPI.Utils
{
    internal class SkyDriveResponse
    {
        public static string ProcessRequest(string URI)
        {
            string json = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(URI.ToString());
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                json = reader.ReadToEnd();
            }

            return json;
        }
    }
}