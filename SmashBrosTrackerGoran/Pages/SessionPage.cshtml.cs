using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmashBrosTrackerGoran.Data;
using SmashBrosTrackerGoran.Models;

namespace SmashBrosTrackerGoran.Pages
{
    public class SessionPageModel : PageModel
    {
        private readonly SmashDbContext _dbContext;
        public SessionPageModel(SmashDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int Player1CharacterId { get; set; }
        public int Player2CharacterId { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Character Player1Character { get; set; }
        public Character Player2Character { get; set; }

        public List<Character> Characters { get; set; } = new List<Character>();
        public List<Player> Players { get; set; } = new List<Player>();

        public void OnGet(int Player1Id, int Player2Id, int Player1CharacterId, int Player2CharacterId)
{
    this.Player1Id = Player1Id;
    this.Player2Id = Player2Id;
    this.Player1CharacterId = Player1CharacterId;
    this.Player2CharacterId = Player2CharacterId;

    Player1 = _dbContext.Players.FirstOrDefault(p => p.Id == Player1Id) ?? new Player { Name = "Player 1" };
    Player2 = _dbContext.Players.FirstOrDefault(p => p.Id == Player2Id) ?? new Player { Name = "Player 2 zebri" };
    Player1Character = _dbContext.Characters.FirstOrDefault(c => c.Id == Player1CharacterId) ?? new Character { Name = "Unknown" };
    Player2Character = _dbContext.Characters.FirstOrDefault(c => c.Id == Player2CharacterId) ?? new Character { Name = "Unknown" };
}
    }
}
