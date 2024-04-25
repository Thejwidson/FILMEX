using FILMEX.Data;
using FILMEX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FILMEX.Controllers
{
    public class UserListsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public UserListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetFilmsToWatch()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users
                .Include(u => u.MoviesToWatch)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound(); 
            }

            var GetFilmsToWatch = user.MoviesToWatch.ToList();
            return View(GetFilmsToWatch);
        }
    
    }
}
    

