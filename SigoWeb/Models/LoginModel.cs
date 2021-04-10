using System.ComponentModel.DataAnnotations;

namespace SigoWeb.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}
