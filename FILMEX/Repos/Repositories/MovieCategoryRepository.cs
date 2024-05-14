using System.Collections.Generic;
using System.Linq;
using FILMEX.Data;
using FILMEX.Models.Entities;
using FILMEX.Repos.Interfaces;

namespace FILMEX.Repos.Repositories
{
    public class MovieCategoryRepository : IMovieCategoryController
    {
        private readonly ApplicationDbContext _context;

        public MovieCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<MovieCategory> GetAllCategories()
        {
            return _context.MoviesCategories.ToList();
        }

        public MovieCategory GetCategoryById(int id)
        {
            return _context.MoviesCategories.FirstOrDefault(c => c.Id == id);
        }

        public void AddCategory(MovieCategory category)
        {
            _context.MoviesCategories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(MovieCategory category)
        {
            _context.MoviesCategories.Update(category);
            _context.SaveChanges();
        }

        public void AddMovieToCategory(Movie movie, int categoryId)
        {
            _context.MoviesCategories.FirstOrDefault(c => c.Id == categoryId).Movies.Add(movie);
        }

        public void DeleteCategory(int id)
        {
            var category = _context.MoviesCategories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _context.MoviesCategories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}