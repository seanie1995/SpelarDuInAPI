using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpelarDuInClient.Models.ViewModels
{
    internal class TrackViewModel
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("trackTitle")]
        public string TrackTitle { get; set; }
        [JsonPropertyName("artist")]
        public string Artist { get; set; }
    }
}
