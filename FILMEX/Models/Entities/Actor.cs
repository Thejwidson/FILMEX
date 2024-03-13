namespace FILMEX.Models.Entities
{
    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
        
        public string LastName { get; set; } = default!;

        public int? Age { get; set; } 

        public DateTime? DateOfBirth { get; set; }
        
        public DateTime? DateOfDeath { get; set;}

        public List<Series> Series { get; set; } = new List<Series>();

        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
