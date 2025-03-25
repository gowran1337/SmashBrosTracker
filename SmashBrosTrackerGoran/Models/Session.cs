using System.Numerics;

namespace SmashBrosTrackerGoran.Models
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        
        public int WinnerId { get; set; }
        public Player Winner { get; set; }

        public List<SessionPlayer> SessionPlayers { get; set; } = new List<SessionPlayer>();
    }
}
