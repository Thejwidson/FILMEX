using System.ComponentModel.DataAnnotations;

namespace FILMEX.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }

        [Range(0,10)]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
