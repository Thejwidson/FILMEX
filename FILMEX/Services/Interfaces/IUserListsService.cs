using FILMEX.Models;

namespace FILMEX.Services.Interfaces
{
    public interface IUserListsService
    {
        Task<ToWatchViewModel> GetUserMSLists(string userId);
        string GetRemainingTime(DateTime releaseDate);
        Task<int> GetItemsReleasingTodayCountAsync(string userId);
    }
}