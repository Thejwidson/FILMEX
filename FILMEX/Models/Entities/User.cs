using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FILMEX.Models.Entities
{
    public class User : IdentityUser
    {
        [Required]  
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        public List<Movie> MoviesToWatch { get; set; } = new List<Movie>();

        public List<Series> SeriesToWatch { get; set; } = new List<Series>();
        

        public string AttachmentSource { get; set; }

        public User()
        {
            AttachmentSource = @"users/profileImage/defaultProfileImage.jfif";
        }
    }
}
