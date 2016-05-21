using System;
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
                    return JsonConvert.DeserializeObject<T>(stream.ToString());
                }
            }
        }
    }
}