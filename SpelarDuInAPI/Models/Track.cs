namespace SpelarDuInAPI.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string TrackTitle { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
