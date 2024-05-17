using FILMEX.Models.ViewModels;
using FILMEX.Views.UserLists;

namespace FILMEX.Services.Interfaces
{
    public interface IUserListsService
    {
        Task<HomeViewModel> GetUserMSListsHome(string userId);
        Task<ToWatchViewModel> GetUserMSLists(string userId);
    }
}
