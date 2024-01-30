using System.Text.Json.Serialization;

namespace SpelarDuInClient.Models.ViewModels
{
    public class DiscographyViewModel
    {

        [JsonPropertyName("album")]
        public List<AlbumViewModel> Album { get; set; }

    }
}
