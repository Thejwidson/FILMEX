using FILMEX.Models.Entities;

namespace FILMEX.Repos.Interfaces
{
    public interface IUserListsController
    {
        Task<User> FindUser(string? id);
    }
}
