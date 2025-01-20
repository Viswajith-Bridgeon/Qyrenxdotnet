using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class GeoResponse
    {
        [JsonPropertyName("lat")]
        public string Lat { get; set; }

        [JsonPropertyName("lon")]
        public string Lon { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }
    }
}
