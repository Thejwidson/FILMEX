using FILMEX.Models.Entities;

namespace FILMEX.Repos.Interfaces
{
    public interface IHomeController
    {
        List<Movie> GetAllMovies();
        List<Series> GetAllSeries();
        List<Movie> SearchMovies(string searchPhrase);
        List<Series> SearchSeries(string searchPhrase);
        List<Movie> SortMoviesByCategory(string selectedCategory);
        List<Series> SortSeriesByCategory(string selectedCategory);
    }
}
