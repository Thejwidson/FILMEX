using FILMEX.Data;
using FILMEX.Models;
using FILMEX.Models.Entities;
using FILMEX.Models.ViewModels;
using FILMEX.Repos.Interfaces;
using FILMEX.Repos.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Movie = FILMEX.Models.Entities.Movie;

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

        public static string GetRemainingTime(DateTime releaseDate)
        {
            TimeSpan remainingTime = releaseDate - DateTime.Now;

            if (remainingTime.TotalMilliseconds <= 0){
                return "Released";
            }

            int days = (int)remainingTime.TotalDays;
            int hours = remainingTime.Hours;
            int minutes = remainingTime.Minutes;

            return $"{days} days, {hours} hours, {minutes} minutes";
        }

        public int GetItemsReleasingTodayCount(string? userId)
        {
            DateTime today = DateTime.Today;
            DateTime now = DateTime.Now;
            var userMovies = _userListsController.FindUserWithMoviesNotAsync(userId);
            var userSeries = _userListsController.FindUserWithSeriesNotAsync(userId);

            // Count the movies releasing today or already released
            var movieCount = userMovies.MoviesToWatch.Count(movie => movie.PublishDate.Date == today && movie.PublishDate >= now);

            // Count the series releasing today or already released
            var seriesCount = userSeries.SeriesToWatch.Count(serie => serie.PublishDate.Date == today && serie.PublishDate >= now);

            // Return the total count of movies and series releasing today or already released
            return movieCount + seriesCount;
        }
    }
}
    

