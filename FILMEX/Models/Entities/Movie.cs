using System.ComponentModel.DataAnnotations.Schema;

namespace FILMEX.Models.Entities
{
    public class Movie : Production
    {
        public int? Length { get; set; }
        public List<MovieCategory> Categories { get; set; } = new List<MovieCategory>();
    }
}
