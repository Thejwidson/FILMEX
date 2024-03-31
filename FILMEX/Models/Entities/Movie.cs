using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FILMEX.Models.Entities
{
    public class Movie : Production
    {
        public int? Length { get; set; }
        public string AttachmentSource { get; set; }
        public List<MovieCategory> Categories { get; set; } = new List<MovieCategory>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
