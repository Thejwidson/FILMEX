
namespace FILMEX.Models.Entities
{
    public abstract class Production
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public string Description { get; set; } = String.Empty;

        public DateTime PublishDate { get; set; }

        public int Rating { get; set; }

        public List<Actor> Actors { get; set; } = new List<Actor>();

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
