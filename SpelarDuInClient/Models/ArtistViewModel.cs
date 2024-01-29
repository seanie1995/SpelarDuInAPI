using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpelarDuInClient.Models
{
    internal class ArtistViewModel
    {
        [JsonPropertyName("artistName")]
        public string ArtistName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("tracks")]
        public TrackViewModel[] Tracks { get; set; }
    }
}
