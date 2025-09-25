using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class NewUserDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "\"Password must be at least 8 characters.") ]
        public string Password { get; set; }
    }
}
