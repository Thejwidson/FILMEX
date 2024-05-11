using FILMEX.Models.Entities;

namespace FILMEX.Repos.Interfaces
{
    public interface IUserListsController
    {
        List<Movie> GetAllMovies();
        List<Series> GetAllSeries();
        Task<User> FindUserWithMovies(string? id);
        Task<User> FindUserWithSeries(string? id);
    }
}
