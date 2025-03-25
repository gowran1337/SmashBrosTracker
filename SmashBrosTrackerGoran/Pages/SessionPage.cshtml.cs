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
        public int CharacterId1 { get; set; }
        public int CharacterId2 { get; set; }

        public List<Character> Characters { get; set; } = new List<Character>();
        public List<Player> Players { get; set; } = new List<Player>();

        public async Task OnGetAsync(int player1Id, int player2Id,
                               int character1Id, int character2Id)
        {
            // Store the parameters in properties
            Player1Id = player1Id;
            Player2Id = player2Id;
            CharacterId1 = character1Id;
            CharacterId2 = character2Id;
        }
    }
}
