using System.ComponentModel.DataAnnotations;

namespace webbappemma.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
        public string Username { get; set; }

        public string Rol { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}