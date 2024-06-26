﻿namespace FILMEX.Models.Entities
{
    public class Series : Production
    {
        public string AttachmentSource { get; set; }
        public List<Season> Seasons { get; set; } = new List<Season>();
        public List<SeriesCategory> Categories { get; set; } = new List<SeriesCategory>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
