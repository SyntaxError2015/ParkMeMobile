using Newtonsoft.Json;

namespace ParkMeMobile.Common.Models
{
    public class Slot
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("isOccupied")]
        public bool IsOccupied { get; set; }

        [JsonProperty("park")]
        public Park Park { get; set; }

        [JsonProperty("position")]
        public Point Position { get; set; }
    }
}
