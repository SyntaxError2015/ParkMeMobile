using Newtonsoft.Json;

namespace ParkMeMobile.Common.Models
{
    public class Park
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("appUserId")]
        public string AppUserId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("position")]
        public Point Position { get; set; }

        [JsonProperty("slots")]
        public Slot[] Slots { get; set; }
    }
}
