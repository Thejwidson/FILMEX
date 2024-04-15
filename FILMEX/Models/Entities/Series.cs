namespace FILMEX.Models.Entities
{
    public class Series
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = String.Empty;
        public DateTime PublishDate { get; set; }
        public int Rating { get; set; }
        public string AttachmentSource { get; set; }
        public List<Season> Seasons { get; set; } = new List<Season>();
        public List<MovieCategory> Categories { get; set; } = new List<MovieCategory>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
