using System.ComponentModel.DataAnnotations;

namespace mvc.Models
{
    public class LoginViewModel
    {
        [Required]
        public string? Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password required.")]
        public string? Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}