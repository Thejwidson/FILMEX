namespace FILMEX.Models.Entities
{
    public class Season
    {
        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public int SeriesId { get; set; }
        public string Title { get; set; }
        public List<Episode> Episodes { get; set; } = new List<Episode>();
    }
}
