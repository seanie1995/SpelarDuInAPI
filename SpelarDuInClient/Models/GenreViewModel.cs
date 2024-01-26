using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpelarDuInAPIClient.Models
{
    internal class GenreViewModel
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        
        [JsonPropertyName("Genre")]
        public string GenreName { get; set; }
    }
}
