using FILMEX.Data;
using FILMEX.Models;
using FILMEX.Models.Entities;
using FILMEX.Repos;
using FILMEX.Repos.Interfaces;
using FILMEX.Services;
using FILMEX.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Movie = FILMEX.Models.Entities.Movie;

namespace FILMEX.Controllers
{
    public class UserListsController : Controller
    {
        private readonly IUserListsController _userListsController;
        private readonly IUserListsService _userListsService;

        public UserListsController(UserListsRepository userListsController, UserListsService userListsService)
        {
            _userListsController = userListsController;
            _userListsService = userListsService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = await _userListsService.GetUserMSLists(userId);
            return View(viewModel);
        }

        // GET: UserLists/ToWatch
        public async Task<IActionResult> ToWatch()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = await _userListsService.GetUserMSLists(userId);
            return View(viewModel);
        }

        public int GetItemsReleasingTodayCount(string? userId)
        {
            int amount = GetItemsReleasingTodayCount(userId);
            return amount;
        }
    }
}
    

