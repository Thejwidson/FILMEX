using FILMEX.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace FILMEX.Models
{
    public class Movie
    {
        public int Length { get; set; }

        [Display(Name = "Cover Image")]
        public IFormFile CoverImage { get; set; }

        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public string Description { get; set; } = String.Empty;

        public DateTime PublishDate { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<MovieCategory> Categories { get; set; } = new List<MovieCategory>();
        public string Director { get; set; } = String.Empty;
        public string Screenwriter { get; set; } = String.Empty;
        public string Location { get; set; } = String.Empty;
    }
}
