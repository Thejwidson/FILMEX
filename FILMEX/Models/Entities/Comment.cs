using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FILMEX.Models.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
