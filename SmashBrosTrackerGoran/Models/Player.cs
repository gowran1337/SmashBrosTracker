
namespace SmashBrosTrackerGoran.Models
{
    public class Player
    {
        public int Id { get; set; }

        
        public string Name { get; set; } // name of the player

        public int TotalStars { get; set; } //lifetime stars earned
        public int KebabsWon { get; set; } // lifetime kebabs won, kebab won by winning best of 5


        //Foreign Keys
        public int? CharacterId { get; set; } 
        public Character Character { get; set; }  // FK to Character
        public List<SessionPlayer> SessionPlayers { get; set; } = new List<SessionPlayer>();
        public List<KebabDebt> DebtsOwed { get; set; } = new();  // Debts player owes
        public List<KebabDebt> DebtsReceived { get; set; } = new();  // Debts player is owed
    }
}
