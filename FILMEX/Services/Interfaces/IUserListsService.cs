using FILMEX.Models;
using FILMEX.Models.ViewModels;

namespace FILMEX.Services.Interfaces
{
    public interface IUserListsService
    {
        Task<HomeViewModel> GetUserMSListsHome(string userId);
        Task<ToWatchViewModel> GetUserMSLists(string userId);
    }
}
