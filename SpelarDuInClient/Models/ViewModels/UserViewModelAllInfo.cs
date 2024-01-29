using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpelarDuInClient.Models.ViewModels
{
    public class UserViewModelAllInfo
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("genres")]
        public List<GenreViewModel> Genres { get; set; }
        [JsonPropertyName("artists")]

        public List<ArtistViewModel2> Artists { get; set; }
        [JsonPropertyName("tracks")]

        public List<TrackViewModel2> Tracks { get; set; }

    }
}
