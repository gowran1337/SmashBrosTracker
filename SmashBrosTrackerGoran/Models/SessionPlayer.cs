namespace SmashBrosTrackerGoran.Models
{
    public class SessionPlayer
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int StarsEarned { get; set; }
    }
}
