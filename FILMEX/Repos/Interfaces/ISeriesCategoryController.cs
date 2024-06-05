using FILMEX.Models.Entities;

namespace FILMEX.Repos.Interfaces
{
    public interface ISeriesCategoryController
    {
        List<SeriesCategory> GetAllCategories();
        SeriesCategory GetCategoryById(int id);
        void AddCategory(SeriesCategory category);
        void UpdateCategory(SeriesCategory category);
        void AddSeriesToCategory(Series series, int categoryId);
        void DeleteSeriesFromCategory(int seriesId, int categoryId);
        List<SeriesCategory> GetAllSeriesCategoriesBySeriesID(int seriesId);
        void DeleteCategory(int id);
    }
}
