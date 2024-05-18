namespace FILMEX.Models.Entities
{
    public class SeriesCategory
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public List<Series> Series { get; set; } = new List<Series>();
    }
}
