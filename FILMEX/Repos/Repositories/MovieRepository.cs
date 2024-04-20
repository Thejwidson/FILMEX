using FILMEX.Data;
using FILMEX.Models.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Linq;

namespace FILMEX.Repos.Repositories
{
    public class MovieRepository : Interfaces.IMovieController
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironemt;

        // Movie
        public MovieRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironemt)
        {
            _context = context;
            _webHostEnvironemt = webHostEnvironemt;
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetMoviesWithCommentsAsync(int? id)
        {
            return await _context.Movies.Include(m => m.Comments).ThenInclude(c => c.Author).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie> GetMoviesWithReviewsAsync(int? id)
        {
            return await _context.Movies.Include(m => m.Reviews).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie> FindMoviesAsync(int? id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public void AddMovie(Movie movie)
        {
            _context.Add(movie);
            _context.SaveChangesAsync();
        }

        public void RemoveMovie(Movie movie)
        {
            _context.Remove(movie); //_context.Movies.Remove(movie);
            _context.SaveChangesAsync();
        }

        public void UpdateMovie(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChangesAsync();
        }

        // User
        public async Task<User> FindUserAsync(string? userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        // ReviewsMovie
        public async Task<ReviewMovie> FindReviewByMovieIdAndUserIdAsync(int movieId, string userId)
        {
            return await _context.ReviewsMovie.FirstOrDefaultAsync(r => r.Movie.Id == movieId && r.User.Id == userId);
        }

        public void AddReview(Movie movie, ReviewMovie review)
        {
            movie.Reviews.Add(review);
            _context.ReviewsMovie.Add(review);
            _context.SaveChangesAsync();
        }

        public void UpdateReview(ReviewMovie review)
        {
            _context.ReviewsMovie.Update(review);
            _context.SaveChangesAsync();
        }

        // Comment
        public async Task<Comment> FindCommentByIdAsync(int commentId)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        }

        public void LoadCommentRelations(Comment comment)
        {
            _context.Entry(comment).Reference(c => c.Author).Load();
            _context.Entry(comment).Reference(c => c.Movie).Load();
        }
        public void AddComment(Movie movie, Comment comment)
        {
            movie.Comments.Add(comment);
            _context.SaveChangesAsync();
        }

        public async Task RemoveComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
