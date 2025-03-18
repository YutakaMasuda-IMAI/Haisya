using System;
using System.Text.Json.Serialization;

namespace SeikyuWeb.Models.ReportCommons
{
    public class R_AnkenList
    {
        [JsonPropertyName("Anken_No")]
        public string Anken_No { get; set; }

        [JsonPropertyName("SyasyuDisplay")]
        public string SyasyuDisplay { get; set; }

        [JsonPropertyName("PointDate")]
        public DateTime PointDate { get; set; }

        [JsonPropertyName("PointTime")]
        public DateTime PointTime { get; set; }

        [JsonPropertyName("Address")]
        public string Address { get; set; }

        [JsonPropertyName("UnloadPointDate")]
        public DateTime UnloadPointDate { get; set; }

        [JsonPropertyName("UnloadPointTime")]
        public DateTime UnloadPointTime { get; set; }

        [JsonPropertyName("UnloadAddress")]
        public string UnloadAddress { get; set; }

        [JsonPropertyName("LuggageDisplay")]
        public string LuggageDisplay { get; set; }
    }
}
