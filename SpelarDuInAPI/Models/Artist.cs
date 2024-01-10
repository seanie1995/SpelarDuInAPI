namespace SpelarDuInAPI.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string ArtistName { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
