using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParkMeMobile.Common.Http
{
    public static class HttpClient
    {
        public static async Task<T> Get<T>(string url)
        {
            var request = (HttpWebRequest) WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (var response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (var stream = response.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        var objString = streamReader.ReadToEnd();

                        var result = JsonConvert.DeserializeObject<T>(objString);
                        return result;
                    }
                }
            }
        }
    }
}