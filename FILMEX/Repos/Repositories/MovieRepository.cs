using FILMEX.Data;
using FILMEX.Models.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FILMEX.Repos.Repositories
{
    public class MovieRepository : Interfaces.IMovieController
    {
        private readonly ApplicationDbContext _context;

        // Movie
        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<Movie> FindForDelete(int? id)
        {
            return await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie> FindMovieWithCommentByIdAsync(int? commentId)
        {
            return await _context.Movies.Include(m => m.Comments).FirstOrDefaultAsync(m => m.Comments.Any(c => c.Id == commentId));
        }

        public async Task AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveMovie(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovie(Movie movie)
        {
            _context.Update(movie);
            await _context.SaveChangesAsync();
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
        public async Task AddComment(Movie movie, Comment comment)
        {
            movie.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}