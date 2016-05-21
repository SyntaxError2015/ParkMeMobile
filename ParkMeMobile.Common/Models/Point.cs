using Newtonsoft.Json;

namespace ParkMeMobile.Common.Models
{
    public class Point
    {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }
    }
}
