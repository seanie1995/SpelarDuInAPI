namespace SpelarDuInAPI.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public virtual ICollection<GenreViewModel> Genres { get; set; }

        public virtual ICollection<ArtistViewModel> Artists { get; set; }

        public virtual ICollection<TrackViewModel> Tracks { get; set; }


    }
}
