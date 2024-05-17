using FILMEX.Models.Entities;
using FILMEX.Models.ViewModels;
using FILMEX.Repos;
using FILMEX.Repos.Interfaces;
using FILMEX.Services.Interfaces;
using FILMEX.Views.UserLists;
using Microsoft.AspNetCore.Mvc;

namespace FILMEX.Services
{
    public class UserListsService : IUserListsService
    {
        private readonly IUserListsController _userListsController;

        public UserListsService(UserListsRepository userListsController)
        {
            _userListsController = userListsController;
        }

        public async Task<HomeViewModel> GetUserMSListsHome(string userId) 
        {
            var userMovies = await _userListsController.FindUserWithMovies(userId);
            var userSeries = await _userListsController.FindUserWithSeries(userId);

            var viewModel = new HomeViewModel
            {
                Movies = userMovies?.MoviesToWatch.ToList() ?? new List<Movie>(),
                Series = userSeries?.SeriesToWatch.ToList() ?? new List<Series>()
            };

            return viewModel;
        }

        public async Task<ToWatchViewModel> GetUserMSLists(string userId)
        {
            var userMovies = await _userListsController.FindUserWithMovies(userId);
            var userSeries = await _userListsController.FindUserWithSeries(userId);

            var viewModel = new ToWatchViewModel
            {
                Movies = userMovies?.MoviesToWatch.ToList() ?? new List<Movie>(),
                Series = userSeries?.SeriesToWatch.ToList() ?? new List<Series>()
            };

            return viewModel;
        }

    }
}
