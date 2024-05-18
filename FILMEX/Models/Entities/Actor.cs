using System.ComponentModel;

namespace FILMEX.Models.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string Name { get; set; } = default!;
        
        [DisplayName("Last Name")]
        public string LastName { get; set; } = default!;

        public int? Age { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Date of Death")]
        public DateTime? DateOfDeath { get; set;}

        public List<Series> Series { get; set; } = new List<Series>();

        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
