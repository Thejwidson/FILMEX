using FILMEX.Data;
using FILMEX.Models;
using FILMEX.Models.Entities;
using FILMEX.Models.ViewModels;
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
            var viewModel = new HomeViewModel
            {
                Movies = _userListsController.GetAllMovies(),
                Series = _userListsController.GetAllSeries()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ToWatch()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userListsController.FindUserWithMovies(userId);
            var user2 = await _userListsController.FindUserWithSeries(userId);

            if (user == null)
            {
                return NotFound(); 
            }

            var viewModel = new HomeViewModel
            {
                Movies = user.MoviesToWatch.ToList(),
                Series = user2.SeriesToWatch.ToList()
            };

            return View(viewModel);
        }
    }
}
    

