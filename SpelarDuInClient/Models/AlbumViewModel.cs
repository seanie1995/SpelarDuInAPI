using System.Text.Json.Serialization;

namespace DiscographyViewerAPI.Models.ViewModels
{
    public class AlbumViewModel
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("releasedate")]
        public string YearReleased { get; set; }
    }
}
