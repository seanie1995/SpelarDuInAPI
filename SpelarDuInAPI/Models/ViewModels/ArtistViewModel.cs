namespace SpelarDuInAPI.Models.ViewModels
{
    public class ArtistViewModel
    {
        public string ArtistName { get; set; }
        public string? Description { get; set; }
        public TrackViewModel[] Tracks { get; set; }
    }
}
