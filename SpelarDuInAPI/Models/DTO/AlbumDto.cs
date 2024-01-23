using System.Text.Json.Serialization;

namespace DiscographyViewerAPI.Models.Dto
{
    public class AlbumDto
    {
        [JsonPropertyName("strAlbum")]
        public string StrAlbum { get; set; }

        [JsonPropertyName("intYearReleased")]
        public string IntYearReleased { get; set; }

    }
}
