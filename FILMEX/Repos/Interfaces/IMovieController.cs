using FILMEX.Models.Entities;

namespace FILMEX.Repos.Interfaces
{
    public interface IMovieController
    {
        Task<List<Movie>> GetAllMoviesAsync();
    }
}
