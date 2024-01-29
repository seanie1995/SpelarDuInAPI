using System.Text.Json.Serialization;

namespace SpelarDuInClient.Models.ViewModels
{
    public class TrackViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("trackTitle")]
        public string TrackTitle { get; set; }
    }
}