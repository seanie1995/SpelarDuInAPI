using System.Text.Json.Serialization;

namespace SpelarDuInClient.Models.ViewModels
{
    public class ArtistViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("artistName")]
        public string ArtistName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}