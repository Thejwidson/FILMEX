using FILMEX.Models.Entities;

namespace FILMEX.Repos.Interfaces
{
    public interface IMovieController
    {
        //Movie
        Task<List<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMoviesWithCommentsAsync(int? id);
        Task<Movie> GetMoviesWithReviewsAsync(int? id);
        Task<Movie> FindMoviesAsync(int? id);
        Task<Movie> FindForDelete(int? id);
        Task<Movie> FindMovieWithCommentByIdAsync(int? id);
        Task AddMovie(Movie movie);
        Task RemoveMovie(Movie movie);
        Task UpdateMovie(Movie movie);

        // User
        Task<User> FindUserAsync(string? id);

        // ReviewsMovie
        Task<ReviewMovie> FindReviewByMovieIdAndUserIdAsync(int movieId, string userId);
        public void AddReview(Movie movie, ReviewMovie review);
        public void UpdateReview(ReviewMovie review);

        // Comment
        Task<Comment> FindCommentByIdAsync(int commentId);
        public void LoadCommentRelations(Comment comment);
        Task AddComment(Movie movie, Comment comment);
        Task RemoveComment(Comment comment);

        // MoviesToWatch
        Task AddMovieToWatch(Movie movie, User user);
        Task RemoveMovieToWatch(Movie movie, User user);
    }
}