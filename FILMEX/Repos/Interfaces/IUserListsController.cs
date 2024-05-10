using FILMEX.Models.Entities;

namespace FILMEX.Repos.Interfaces
{
    public interface IUserListsController
    {
        Task<User> FindUserWithMovies(string? id);
        Task<User> FindUserWithSeries(string? id);
    }
}
