using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpelarDuInClient.Models.ViewModels
{
    internal class ArtistListViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("artistName")]
        public string ArtistName { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
