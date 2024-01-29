﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpelarDuInClient.Models
{
    internal class TrackViewModel
    {

        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("TrackTitle")]
        public string TrackTitle { get; set; }
        [JsonPropertyName("Artist")]
        public string Artist { get; set; }
    }
}