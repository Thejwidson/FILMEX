using FILMEX.Data;
using FILMEX.Models;
using FILMEX.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FILMEX.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ILogger<AdminController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        private async Task<List<string>> GetUserRoles(User user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        public async Task<IActionResult> UserRolesManagement()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesModel = new List<UserRolesModel>();
            foreach (User user in users)
            {
                var thisUser = new UserRolesModel();
                thisUser.UserID = user.Id;
                thisUser.Email = user.Email;
                thisUser.FirstName = user.FirstName;
                thisUser.LastName = user.LastName;
                thisUser.Roles = await GetUserRoles(user);
                userRolesModel.Add(thisUser);
            }
            return View(userRolesModel);
        }

        
    }

    
}


