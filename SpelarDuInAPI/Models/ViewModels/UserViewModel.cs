using System.Text.Json.Serialization;

namespace SpelarDuInAPI.Models.ViewModels
{
    public class UserViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Name")]
        public string UserName { get; set; }
    }
}
