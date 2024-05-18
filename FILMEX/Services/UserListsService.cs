using FILMEX.Models.Entities;
using FILMEX.Repos;
using FILMEX.Repos.Interfaces;
using FILMEX.Services.Interfaces;
using FILMEX.Models;

namespace FILMEX.Services
{
    public class UserListsService : IUserListsService
    {
        private readonly IUserListsController _userListsController;

        public UserListsService(UserListsRepository userListsController)
        {
            _userListsController = userListsController;
        }

        public async Task<ToWatchViewModel> GetUserMSLists(string userId)
        {
            var userMovies = await _userListsController.FindUserWithMovies(userId);
            var userSeries = await _userListsController.FindUserWithSeries(userId);

            var movieViewModels = userMovies?.MoviesToWatch.Select(movie => new MovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                TimeUntilRelease = GetRemainingTime(movie.PublishDate)
            }).ToList();

            var seriesViewModels = userSeries?.SeriesToWatch.Select(serie => new SeriesViewModel
            {
                Id = serie.Id,
                Title = serie.Title,
                TimeUntilRelease = GetRemainingTime(serie.PublishDate)
            }).ToList();

            var viewModel = new ToWatchViewModel
            {
                Movies = movieViewModels ?? new List<MovieViewModel>(),
                Series = seriesViewModels ?? new List<SeriesViewModel>()
            };

            return viewModel;
        }

        public string GetRemainingTime(DateTime releaseDate)
        {
            TimeSpan remainingTime = releaseDate - DateTime.Now;

            if (remainingTime.TotalMilliseconds <= 0)
            {
                return "Released";
            }

            int days = (int)remainingTime.TotalDays;
            int hours = remainingTime.Hours;
            int minutes = remainingTime.Minutes;

            return $"{days} days, {hours} hours, {minutes} minutes";
        }

        public async Task<int> GetItemsReleasingTodayCountAsync(string userId)
        {
            DateTime today = DateTime.Today;
            DateTime now = DateTime.Now;
            var userMovies = await _userListsController.FindUserWithMovies(userId);
            var userSeries = await _userListsController.FindUserWithSeries(userId);

            var movieCount = userMovies.MoviesToWatch.Count(movie => movie.PublishDate.Date == today && movie.PublishDate >= now);
            var seriesCount = userSeries.SeriesToWatch.Count(serie => serie.PublishDate.Date == today && serie.PublishDate >= now);

            return movieCount + seriesCount;
        }
    }
}
