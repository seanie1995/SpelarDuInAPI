namespace SpelarDuInAPI.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
