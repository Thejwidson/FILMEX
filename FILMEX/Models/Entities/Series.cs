namespace FILMEX.Models.Entities
{
    public class Series : Production
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = String.Empty;
        public DateTime PublishDate { get; set; }
        public string AttachmentSource { get; set; }
        public List<Season> Seasons { get; set; } = new List<Season>();
        public List<MovieCategory> Categories { get; set; } = new List<MovieCategory>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
