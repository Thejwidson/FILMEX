using System.Collections.Generic;
using System.Linq;
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