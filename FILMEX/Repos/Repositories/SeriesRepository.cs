using FILMEX.Data;
using FILMEX.Models;
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

        public Series FindByIdWithSeasons(int? id)
        {
            return _context.Series.Include(s => s.Seasons).FirstOrDefault(s => s.Id == id);
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

        public bool Any(int? id)
        {
            return _context.Series.Any(e => e.Id == id);
        }

        public Series FindWithComments(int? id)
        {
            return _context.Series.Include(m => m.Comments).FirstOrDefault(m => m.Id == id);
        }

        public async Task<Series> FindByCommentIdAsync(int? commentId)
        {
            return await _context.Series.Include(m => m.Comments).FirstOrDefaultAsync(m => m.Comments.Any(c => c.Id == commentId));
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

        public void AddReview(Series series, ReviewSeries review)
        {
            series.Reviews.Add(review);
            _context.ReviewsSeries.Add(review);
        }

        public void UpdateReview(ReviewSeries review)
        {
            _context.ReviewsSeries.Update(review);
        }

        public ReviewSeries FindReviewByIdAndUId(int seriesId, string userId)
        {
            return _context.ReviewsSeries.FirstOrDefault(r => r.Series.Id == seriesId && r.User.Id == userId);
        }

        public async Task Add(Episode episode)
        {
            _context.Add(episode);
            await _context.SaveChangesAsync();
        }

        public async Task Add(Season season)
        {
            _context.Add(season);
            await _context.SaveChangesAsync();
        }

        // comments
        public void LoadCommentRelations(Comment comment)
        {
            _context.Entry(comment).Reference(c => c.Author).Load();
        }

        public async Task<Comment> FindCommentByIdAsync(int commentId)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        }

        public async Task Add(Series series, Comment comment)
        {
            series.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Series series, Comment comment)
        {
            series.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
