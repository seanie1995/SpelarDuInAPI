using System.Text.Json.Serialization;

namespace DiscographyViewerAPI.Models.Dto
{
    public class AlbumDto
    {
        [JsonPropertyName("strAlbum")] // album name
        public string StrAlbum { get; set; }

        [JsonPropertyName("intYearReleased")] // release date
        public string IntYearReleased { get; set; }

    }
}
