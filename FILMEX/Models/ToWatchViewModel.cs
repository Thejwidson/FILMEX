using FILMEX.Models.Entities;

namespace FILMEX.Models
{
    public class ToWatchViewModel
    {
        public List<Entities.Movie> Movies { get; set; }
        public List<Series> Series { get; set; }
    }
}
