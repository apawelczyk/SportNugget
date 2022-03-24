using System.ComponentModel.DataAnnotations;

namespace SportNugget.ViewModels.Login
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
