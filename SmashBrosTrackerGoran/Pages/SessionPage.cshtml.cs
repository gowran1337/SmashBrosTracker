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
        [BindProperty] //properties bound to form
        public int Player1Id { get; set; }
        [BindProperty]
        public int Player2Id { get; set; }
        [BindProperty]
        public int Player1CharacterId { get; set; }
        [BindProperty]
        public int Player2CharacterId { get; set; }
        [BindProperty]
        public int Player1Stars { get; set; }
        [BindProperty]
        public int Player2Stars { get; set; }   
        [BindProperty]
        public int Player1Kebabs { get; set; }
        [BindProperty]
        public int Player2Kebabs { get; set; }
        [BindProperty]
        public int PlayerKebabs { get; set; }

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Character Player1Character { get; set; }
        public Character Player2Character { get; set; }

        public async Task<IActionResult> OnGetAsync(int Player1Id, int Player2Id, int Player1CharacterId, int Player2CharacterId)
        {
            try
            {
                this.Player1Id = Player1Id;
                this.Player2Id = Player2Id;
                this.Player1CharacterId = Player1CharacterId;
                this.Player2CharacterId = Player2CharacterId;

                // Check if players exist
                Player1 = await _dbContext.Players.FindAsync(Player1Id);
                Player2 = await _dbContext.Players.FindAsync(Player2Id);

                // Check if characters exist
                Player1Character = await _dbContext.Characters.FindAsync(Player1CharacterId);
                Player2Character = await _dbContext.Characters.FindAsync(Player2CharacterId);

            
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("Index");
            }
        }

        public async Task<IActionResult> OnPostAddSession()
        {
            try
            {
                
                var player1 = await _dbContext.Players.FindAsync(Player1Id);
                var player2 = await _dbContext.Players.FindAsync(Player2Id);


                // First create the session
                var session = new Session
                {
                    Date = DateTime.Now,
                    WinnerId = Player1Stars > Player2Stars ? Player1Id : Player2Id
                };

                // Add the session to the context first
                _dbContext.Sessions.Add(session);
                await _dbContext.SaveChangesAsync();

                // Now create the session players with the session ID
                var sessionPlayer1 = new SessionPlayer
                {
                    SessionId = session.Id,
                    PlayerId = Player1Id,
                    CharacterId = Player1CharacterId,
                    StarsEarned = Player1Stars,
                    
                };

                var sessionPlayer2 = new SessionPlayer
                {
                    SessionId = session.Id,
                    PlayerId = Player2Id,
                    CharacterId = Player2CharacterId,
                    StarsEarned = Player2Stars,
                    
                };

                // Add the session players
                _dbContext.SessionPlayers.Add(sessionPlayer1);
                _dbContext.SessionPlayers.Add(sessionPlayer2);
                
                player1.TotalStars += Player1Stars;
                player2.TotalStars += Player2Stars;
                player1.KebabsWon += Player1Kebabs;
                player2.KebabsWon += Player2Kebabs;


                await _dbContext.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                // Log the error and redirect to index
                // You might want to add proper error logging here
                return RedirectToPage("Index");
            }
        }
    }
}
