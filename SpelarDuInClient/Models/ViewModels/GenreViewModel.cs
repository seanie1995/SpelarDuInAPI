using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpelarDuInClient.Models.ViewModels
{
    public class GenreViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        
        [JsonPropertyName("Genre")]
        public string GenreName { get; set; }
    }
}
