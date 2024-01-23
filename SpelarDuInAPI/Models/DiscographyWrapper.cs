using System.Text.Json.Serialization;

namespace DiscographyViewerAPI
{
    public class DiscographyWrapper
    {
        // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);

        
        public class Album
        {
            [JsonPropertyName("strAlbum")]
            public string StrAlbum { get; set; }

            [JsonPropertyName("intYearReleased")]
            public string IntYearReleased { get; set; }
        }

        public class Root
        {
            [JsonPropertyName("album")]
            public List<Album> Album { get; set; }
        }


    }
}
