using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace LogicaNegocio.Entidades
{
    public class Sucursal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public required string Nombre { get; set; }

        [Required]
        [MinLength(5)]
        public required string Direccion { get; set; }

        [Required]
        [Phone]
        public required string Telefono { get; set; }


        public void EsValido()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || Nombre.Length < 3)
                throw new Exception("El nombre de la sucursal debe tener al menos 3 caracteres.");

            if (string.IsNullOrWhiteSpace(Direccion) || Direccion.Length < 5)
                throw new Exception("La dirección debe tener al menos 5 caracteres.");

            if (string.IsNullOrWhiteSpace(Telefono) || !Regex.IsMatch(Telefono, @"^\+?\d{6,15}$"))
                throw new Exception("El teléfono debe tener entre 6 y 15 dígitos, y puede incluir '+' al inicio.");
        }
    }
}
