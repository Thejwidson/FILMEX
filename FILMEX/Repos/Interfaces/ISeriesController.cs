using FILMEX.Models.Entities;

namespace FILMEX.Repos.Interfaces
{
    public interface ISeriesController
    {
        // series
        Task<List<Series>> GetAllAsync();
        Task<Series> FindById(int? id);
        Task Add(Series series);
        Task Update(Series series);
        Task Remove(Series series);
        // User
        Task<User> FindUserAsync(string? id);

        // ReviewsMovie
        ReviewSeries FindReview(int seriesId, string userId);
        public void AddReview(Movie movie, ReviewMovie review);
        public void UpdateReview(ReviewMovie review);


    }
}
