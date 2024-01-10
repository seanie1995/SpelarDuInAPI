namespace SpelarDuInAPI.Models.ViewModels
{
    public class TrackViewModel
    {
        public int Id { get; set; }
        public string TrackTitle { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
