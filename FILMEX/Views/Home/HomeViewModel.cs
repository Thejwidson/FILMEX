using FILMEX.Models.Entities;
using System.Collections.Generic;

namespace FILMEX.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<FILMEX.Models.Entities.Movie> Movies { get; set; }
        public List<Series> Series { get; set; }
    }
}
