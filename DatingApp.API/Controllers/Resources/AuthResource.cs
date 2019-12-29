using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Controllers.Resources
{
    public class AuthResource
    {
        [Required]
        [StringLength(50)]

        public string Username { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 14 characters")]
        public string Password { get; set; }
    }
}