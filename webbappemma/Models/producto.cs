using System.ComponentModel.DataAnnotations;

namespace webbappemma.Models
{
    public class Producto
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string ImagenUrl { get; set; }

    }
}