using System.ComponentModel.DataAnnotations;

namespace FILMEX.Models.Entities
{
    public class ReviewMovie : Review
    {
        public int ReviewMovieId { get; set; }
        public Movie Movie { get; set; }
    }
}