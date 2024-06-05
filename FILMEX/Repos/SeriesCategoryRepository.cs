using FILMEX.Data;
using FILMEX.Models.Entities;
using FILMEX.Repos.Interfaces;

namespace FILMEX.Repos.Repositories
{
    public class SeriesCategoryRepository : ISeriesCategoryController
    {
        private readonly ApplicationDbContext _context;

        public SeriesCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SeriesCategory> GetAllCategories()
        {
            return _context.SeriesCategories.ToList();
        }

        public SeriesCategory GetCategoryById(int id)
        {
            return _context.SeriesCategories.FirstOrDefault(c => c.Id == id);
        }

        public void AddCategory(SeriesCategory category)
        {
            _context.SeriesCategories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(SeriesCategory category)
        {
            _context.SeriesCategories.Update(category);
            _context.SaveChanges();
        }

        public void AddSeriesToCategory(Series series, int categoryId)
        {
            _context.SeriesCategories.FirstOrDefault(c => c.Id == categoryId).Series.Add(series);
        }

        public void DeleteSeriesSeriesFromAllCategories(int seriesId)
        {
            var categories = _context.SeriesCategories.Where(c => c.Series.Any(m => m.Id == seriesId)).ToList();
            foreach (var category in categories)
            {
                var series = category.Series.FirstOrDefault(m => m.Id == seriesId);
                if (series != null)
                {
                    category.Series.Remove(series);
                }
            }
            _context.SaveChanges();
        }

        public void DeleteSeriesFromCategory(int seriesId, int categoryId)
        {
            var category = _context.SeriesCategories.FirstOrDefault(c => c.Id == categoryId);
            var series = category.Series.FirstOrDefault(m => m.Id == seriesId);
            if (series != null)
            {
                category.Series.Remove(series);
                _context.SaveChanges();
            }
        }

        public List<SeriesCategory> GetAllSeriesCategoriesBySeriesID(int seriesId)
        {
            return _context.SeriesCategories.Where(c => c.Series.Any(m => m.Id == seriesId)).ToList();
        }

        public void DeleteCategory(int id)
        {
            var category = _context.SeriesCategories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _context.SeriesCategories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}