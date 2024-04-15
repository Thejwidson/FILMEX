﻿using FILMEX.Models.Entities;
namespace FILMEX.Models

{
    public class SeriesModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = String.Empty;
        public DateTime PublishDate { get; set; }
        public int Rating { get; set; }
        public IFormFile CoverImage { get; set; }
        public List<Season> Seasons { get; set; } = new List<Season>();
        public List<MovieCategory> Categories { get; set; } = new List<MovieCategory>();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
