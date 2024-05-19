using FILMEX.Models.Entities;
using System.Collections.Generic;

namespace FILMEX.Models
{
    public class ToWatchViewModel
    {
        public List<MovieViewModel> Movies { get; set; }
        public List<SeriesViewModel> Series { get; set; }
    }

    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TimeUntilRelease { get; set; }
    }

    public class SeriesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TimeUntilRelease { get; set; }
    }
}