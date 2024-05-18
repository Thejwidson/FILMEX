using System.ComponentModel.DataAnnotations;

namespace FILMEX.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }

        [Range(0,10)]
        public float Rating { get; set; }

        public User User { get; set; }
    }
}
