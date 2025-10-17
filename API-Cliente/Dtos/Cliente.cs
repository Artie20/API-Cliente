using System.ComponentModel.DataAnnotations;

namespace API_Cliente.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }

    public class CreaClienteDto
    {
        [Required(ErrorMessage = "El nombre debe estar especificado.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Los apellidos debe estar especificados.")]
        public string Apellidos { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El correo no es correcto")]
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
