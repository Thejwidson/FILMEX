using FILMEX.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FILMEX.Models
{
    public class Movie
    {
        public IFormFile CoverImage { get; set; }
    }
}
