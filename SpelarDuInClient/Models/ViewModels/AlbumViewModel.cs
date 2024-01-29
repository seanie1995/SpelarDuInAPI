using System.Text.Json.Serialization;

namespace SpelarDuInClient.Models.ViewModels
{
    public class AlbumViewModel
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("releasedate")]
        public string YearReleased { get; set; }
    }
}
