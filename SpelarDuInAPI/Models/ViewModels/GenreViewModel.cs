using System.Text.Json.Serialization;

namespace SpelarDuInAPI.Models.ViewModels
{
    public class GenreViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Genre")]
        public string GenreName { get; set; }


    }
}
