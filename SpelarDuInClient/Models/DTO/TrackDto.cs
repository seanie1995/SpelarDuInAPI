using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelarDuInClient.Models.DTO
{
    internal class TrackDto
    {
        public int Id { get; set; }
        public string TrackTitle { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
   
    }
}
