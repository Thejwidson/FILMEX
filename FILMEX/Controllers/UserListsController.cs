using FILMEX.Services.Interfaces;
using FILMEX.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FILMEX.Controllers
{
    public class UserListsController : Controller
    {
        private readonly IUserListsService _userListsService;

        public UserListsController(UserListsService userListsService)
        {
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

        public async Task<int> GetItemsReleasingTodayCount(string? userId)
        {
            int numberOfItems = await _userListsService.GetItemsReleasingTodayCountAsync(userId);
            return numberOfItems;
        }

    }
}
    

