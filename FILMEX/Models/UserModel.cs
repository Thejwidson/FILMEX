using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FILMEX.Models
{
    public class UserModel : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        public IFormFile ProfileImage { get; set; }
    }
}
