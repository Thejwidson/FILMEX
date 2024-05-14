using FILMEX.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FILMEX.Repos.Interfaces
{
    public interface IMovieCategoryController
    {
        List<MovieCategory> GetAllCategories();
        MovieCategory GetCategoryById(int id);
        void AddCategory(MovieCategory category);
        void UpdateCategory(MovieCategory category);
        void AddMovieToCategory(Movie movie, int categoryId);
        void DeleteCategory(int id);
    }
}
