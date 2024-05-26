namespace FILMEX.Models.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Production Production { get; set; }
        public User Author { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
