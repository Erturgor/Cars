using System.ComponentModel.DataAnnotations;

namespace Cars.API
{
    public class RegisterDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Password too simple")]
        public string Password { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
