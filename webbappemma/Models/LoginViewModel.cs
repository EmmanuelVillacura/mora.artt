using System.ComponentModel.DataAnnotations;

namespace webbappemma.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Este campo no puede estar vacio.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio.")]
        public string Password { get; set; }
    }
}
