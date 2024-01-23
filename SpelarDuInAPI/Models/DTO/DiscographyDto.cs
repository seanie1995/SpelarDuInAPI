using System.Text.Json.Serialization;

namespace DiscographyViewerAPI.Models.Dto
{
    public class DiscographyDto
    {
             
        [JsonPropertyName("album")]
        public List<AlbumDto> Album { get; set; }
        
    }
}
