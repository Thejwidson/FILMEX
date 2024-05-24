
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace FILMEX.Models.Entities
{
    public abstract class Production
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public string Description { get; set; } = String.Empty;

        public DateTime PublishDate { get; set; }

        public string Director { get; set; } = String.Empty;
        public string Screenwriter { get; set; } = String.Empty;

        public string Location { get; set; } = String.Empty;
    }
}
