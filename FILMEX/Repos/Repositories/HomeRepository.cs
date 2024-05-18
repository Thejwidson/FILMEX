using FILMEX.Data;
using FILMEX.Models.Entities;
using FILMEX.Repos.Interfaces;

namespace FILMEX.Repos.Repositories
{
    public class HomeRepository : IHomeController
    {
        private readonly ApplicationDbContext _context;
        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Movie> GetAllMovies()
        {
            return _context.Movies.ToList();
        }

        public List<Series> GetAllSeries()
        {
            return _context.Series.ToList();
        }

        public List<Actor> SearchActors(string searchPhrase)
        {
            return _context.Actors
                .Where(a => a.Name.Contains(searchPhrase) || a.LastName.Contains(searchPhrase))
                .ToList();
        }

        public List<Movie> SearchMovies(string searchPhrase)
        {
            return _context.Movies
                .Where(m => m.Title.Contains(searchPhrase))
                .ToList();
        }

        public List<Movie> SortMoviesByCategory(string selectedCategory)
        {
            return _context.Movies
                .Where(m => m.Categories.Any(c => c.CategoryName == selectedCategory))
                .ToList();
        }

        public List<Series> SearchSeries(string searchPhrase)
        {
            return _context.Series
                .Where(s => s.Title.Contains(searchPhrase))
                .ToList();
        }

        public List<Series> SortSeriesByCategory(string selectedCategory)
        {
            return _context.Series
                .Where(s => s.Categories.Any(c => c.CategoryName == selectedCategory))
                .ToList();
        }
    }
}
