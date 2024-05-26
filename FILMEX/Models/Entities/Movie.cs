namespace FILMEX.Models.Entities
{
    public class Movie : Production
    {
        public int Length { get; set; }
        public string AttachmentSource { get; set; }
        public List<MovieCategory> Categories { get; set; } = new List<MovieCategory>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
