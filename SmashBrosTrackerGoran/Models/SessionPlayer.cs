using Microsoft.EntityFrameworkCore;

namespace SmashBrosTrackerGoran.Models
{
    public class SessionPlayer
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int CharacterId { get; set; } // New property
        public Character Character { get; set; } // Navigation property

        public int StarsEarned { get; set; }
        
    }
}
