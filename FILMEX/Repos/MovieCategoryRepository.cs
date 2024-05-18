using System.Collections.Generic;
using System.Linq;
using FILMEX.Data;
using FILMEX.Models.Entities;
using FILMEX.Repos.Interfaces;

namespace FILMEX.Repos
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

        public void DeleteMovieFromCategory(int movieId, int categoryId) 
        { 
            var category = _context.MoviesCategories.FirstOrDefault(c => c.Id == categoryId); 
            var movie = category.Movies.FirstOrDefault(m => m.Id == movieId); 
            if (movie != null)
            {
                category.Movies.Remove(movie); 
                _context.SaveChanges(); 
            } 
        }

        public void DeleteMovieFromAllCategories(int movieId)
        {
            var categories = _context.MoviesCategories.Where(c => c.Movies.Any(m => m.Id == movieId)).ToList();
            foreach (var category in categories)
            {
                var movie = category.Movies.FirstOrDefault(m => m.Id == movieId);
                if (movie != null)
                {
                    category.Movies.Remove(movie);
                }
            }
            _context.SaveChanges();
        }

        public List<MovieCategory> GetAllMovieCategoriesByMovieID(int movieId)
        {
            return _context.MoviesCategories.Where(c => c.Movies.Any(m => m.Id == movieId)).ToList();
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