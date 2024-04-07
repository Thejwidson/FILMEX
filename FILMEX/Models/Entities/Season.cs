namespace FILMEX.Models.Entities
{
    public class Season
    {
        public int Id { get; set; }
        public int seriesId { get; set; }
        public string Title { get; set; }
        public List<Episode> Episodes { get; set; } = new List<Episode>();
    }
}
