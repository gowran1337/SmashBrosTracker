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
        
      
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int Player1Character { get; set; }
        public int Player2Character { get; set; }
        [BindProperty]
        public string SelectedPlayerName { get; set; }
        

        public Player Player { get; set; }
        public List<Character> Characters { get; set; } = new List<Character>(); 
        public List<Player> Players { get; set; } = new List<Player>();

        public async Task OnGetAsync()// get the characters and players from the database and put them in the list
        {
            Characters = await _context.Characters.ToListAsync();
            Players = await _context.Players.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeletePlayer(int id)
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
        public IActionResult OnPostStartSession(int Player1Id, int Player2Id, int Player1CharacterId, int Player2CharacterId)
        {
            return Redirect($"/SessionPage?Player1Id={Player1Id}&Player2Id={Player2Id}&Player1CharacterId={Player1CharacterId}&Player2CharacterId={Player2CharacterId}");
        }
    }
}
