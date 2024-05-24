using FILMEX.Models.Entities;

namespace FILMEX.Models
{
    public class SearchViewModel
    {
        public List<Models.Entities.Movie> Movies { get; set; } = new List<Entities.Movie>();
        public List<Series> Series { get; set; } = new List<Series>();
    }
}
