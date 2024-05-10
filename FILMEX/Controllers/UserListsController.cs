using FILMEX.Data;
using FILMEX.Models;
using FILMEX.Repos.Interfaces;
using FILMEX.Repos.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FILMEX.Controllers
{
    public class UserListsController : Controller
    {
        private readonly IUserListsController _userListsController;

        public UserListsController(UserListsRepository userListsController)
        {
            _userListsController = userListsController;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetFilmsToWatch()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userListsController.FindUserWithMovies(userId);

            if (user == null)
            {
                return NotFound(); 
            }

            var GetFilmsToWatch = user.MoviesToWatch.ToList();
            return View(GetFilmsToWatch);
        }

        public async Task<IActionResult> GetSeriesToWatch()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userListsController.FindUserWithSeries(userId);

            if (user == null)
            {
                return NotFound();
            }

            var GetSeriesToWatch = user.SeriesToWatch.ToList();
            return View(GetSeriesToWatch);
        }

    }
}
    

