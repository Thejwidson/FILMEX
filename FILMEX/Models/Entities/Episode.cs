namespace FILMEX.Models.Entities
{
    public class Episode
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int SeriesId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public DateTime PublishDate { get; set; }
        public int Rating { get; set; }
    }
}
