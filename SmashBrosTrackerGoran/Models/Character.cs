namespace SmashBrosTrackerGoran.Models
{
    public class Character
    {
        public int Id { get; set; }  // Primary Key
        public string Name { get; set; }  // Character Name (e.g., "Fox", "Marth", etc.)


        // Navigation property: List of players who use this character
        public List<Player> Players { get; set; } = new List<Player>();
    }
}
