namespace SmashBrosTrackerGoran.Models
{
    
        public class KebabDebt
        {
            public int Id { get; set; }
            public int OwedById { get; set; }  // The player who lost and owes a kebab
            public Player OwedBy { get; set; }

            public int OwedToId { get; set; }  // The winner who is owed a kebab
            public Player OwedTo { get; set; }

            public int Amount { get; set; } = 1; // Number of kebabs owed (can increase if they lose again)

            public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
        }
    }

