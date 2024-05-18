using System.ComponentModel.DataAnnotations;

namespace FILMEX.Models.Entities
{
    public class ReviewSeries : Review
    {
        public int ReviewSeriesId { get; set; }
        public Series Series { get; set; }
    }
}