namespace FILMEX.Models.Entities
{
    public class MovieCategory
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
