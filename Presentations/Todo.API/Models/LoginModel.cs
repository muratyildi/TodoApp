using System.ComponentModel.DataAnnotations;

namespace Todo.API.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "required")]
        [EmailAddress(ErrorMessage = "invalid-format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "required")]
        public string Password { get; set; }
    }
}
