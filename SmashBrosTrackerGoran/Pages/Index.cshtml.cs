using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmashBrosTrackerGoran.Data;
using SmashBrosTrackerGoran.Models;

namespace SmashBrosTrackerGoran.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SmashDbContext _context;//database context
        public IndexModel(SmashDbContext context)//constructor for database?
        {
            _context = context;
        }

        [BindProperty]
        public int Player1Id { get; set; }
        [BindProperty]
        public int Player2Id { get; set; }
        [BindProperty]
        public int Player1CharacterId { get; set; }
        [BindProperty]
        public int Player2CharacterId { get; set; }
        [BindProperty]
        public string SelectedPlayerName { get; set; }

        public Character Character { get; set; }
        public Player Player { get; set; }
        public List<Character> Characters { get; set; } = new List<Character>();
        public List<Player> Players { get; set; } = new List<Player>();

        public List<Session> Sessions { get; set; }

        public async Task OnGetAsync()// get the characters and players from the database and put them in the list
        {
            Characters = await _context.Characters.ToListAsync();
            Players = await _context.Players.ToListAsync();

            Sessions = await _context.Sessions //get the sessions from the database
               .Include(s => s.SessionPlayers)
                   .ThenInclude(sp => sp.Player)
               .Include(s => s.SessionPlayers)
                   .ThenInclude(sp => sp.Character)
               .OrderByDescending(s => s.Date)
               .ToListAsync();
        }       

        public async Task<IActionResult> OnPostDeletePlayer(int id) // correct int gets sent from .cshtml to delete chosen player
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddPlayerMethod()
        {
            Player = new Player
            {
                Name = SelectedPlayerName,
                TotalStars = 0,
                KebabsWon = 0,
            };

            _context.Players.Add(Player);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
        public IActionResult OnPostStartSession()
        {
            return RedirectToPage("/SessionPage", new
            {
                Player1Id = Player1Id,
                Player2Id = Player2Id,
                Player1CharacterId = Player1CharacterId,
                Player2CharacterId = Player2CharacterId
            });
        }
     
    }
}
