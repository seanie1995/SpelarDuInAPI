namespace SpelarDuInAPI.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}
