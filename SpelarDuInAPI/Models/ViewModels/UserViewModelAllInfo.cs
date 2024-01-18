namespace SpelarDuInAPI.Models.ViewModels
{
    public class UserViewModelAllInfo
    {
        public string UserName { get; set; }
        public List<GenreViewModel> Genres { get; set; }

        public List<ArtistViewModel> Artists { get; set; }

        public List<TrackViewModel> Tracks { get; set; }

    }
}
