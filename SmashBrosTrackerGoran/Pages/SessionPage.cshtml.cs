using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmashBrosTrackerGoran.Data;

namespace SmashBrosTrackerGoran.Pages
{
    public class SessionPageModel : PageModel
    {
        private readonly SmashDbContext _dbContext;
        public SessionPageModel(SmashDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string Player1Name { get; set; } 
        public string Player2Name { get; set; } 
    }
}
