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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
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

        public async Task<IActionResult> ManageUsersRole(string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);
            if (user == null)
            {
                return NotFound(); 
            }

            var model = new List<UserRolesManagement>();
            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRolesModel = new UserRolesManagement
                {
                    UserID = userID,
                    RoleID = role.Id,
                    RoleName = role.Name
                };


                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesModel.SelectedRole = true;
                }
                else
                {
                    userRolesModel.SelectedRole = false;
                }

                model.Add(userRolesModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUsersRole(List<UserRolesManagement> model, string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);
            if (user == null)
            {
                return View();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.SelectedRole).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("UserRolesManagement");
        }


    }

    
}


