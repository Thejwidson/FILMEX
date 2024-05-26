using FILMEX.Models.Entities;

namespace FILMEX.Repos.Interfaces
{
    public interface IMovieCategoryController
    {
        List<MovieCategory> GetAllCategories();
        MovieCategory GetCategoryById(int id);
        void AddCategory(MovieCategory category);
        void UpdateCategory(MovieCategory category);
        void AddMovieToCategory(Movie movie, int categoryId);
        void DeleteMovieFromCategory(int movieId, int categoryId);
        List<MovieCategory> GetAllMovieCategoriesByMovieID(int movieId);
        void DeleteCategory(int id);
        void DeleteMovieFromAllCategories(int movieId);
    }
}
