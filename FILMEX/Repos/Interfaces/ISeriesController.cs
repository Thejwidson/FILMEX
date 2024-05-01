using FILMEX.Models.Entities;

namespace FILMEX.Repos.Interfaces
{
    public interface ISeriesController
    {
        // series
        Task<List<Series>> GetAllAsync();
        Task<Series> FindById(int? id);
        Series FindByIdWithSeasons(int? id);
        Task Add(Series series);
        Task Update(Series series);
        Task Remove(Series series);
        bool Any(int? id);
        Series FindWithComments(int? id);

        // User
        Task<User> FindUserAsync(string? id);

        // ReviewsMovie
        ReviewSeries FindReview(int seriesId, string userId);
        public void AddReview(Series movie, ReviewSeries review);
        public void UpdateReview(ReviewSeries review);
        ReviewSeries FindReviewByIdAndUId(int seriesId, string userId);

        // Epispode
        Task Add(Episode episode);

        // Season
        Task Add(Season season);

        // Comment
        void LoadCommentRelations(Comment comment);
        Task Add(Series series, Comment comment);
    }
}
