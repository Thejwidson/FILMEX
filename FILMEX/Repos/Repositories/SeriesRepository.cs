using FILMEX.Data;
using FILMEX.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FILMEX.Repos.Repositories
{
    public class SeriesRepository : Interfaces.ISeriesController
    {
        private readonly ApplicationDbContext _context;
        public SeriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Series>> GetAllAsync()
        {
            return await _context.Series.ToListAsync();
        }

        public async Task<Series> FindById(int? id)
        {
            return await _context.Series.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Add(Series series)
        {
            _context.Add(series);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Series series)
        {
            _context.Update(series);
            await _context.SaveChangesAsync();
        }
        
        public async Task Remove(Series series)
        {
            _context.Remove(series);
            await _context.SaveChangesAsync();
        }

        // User
        public async Task<User> FindUserAsync(string? id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        // Review
        public ReviewSeries FindReview(int seriesId, string userId)
        {
            return _context.ReviewsSeries.FirstOrDefault(r => r.Series.Id == seriesId && r.User.Id == userId);
        }
    }
}
