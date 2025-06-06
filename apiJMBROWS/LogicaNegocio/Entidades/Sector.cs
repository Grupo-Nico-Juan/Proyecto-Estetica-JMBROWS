using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LogicaNegocio.Entidades
{
    public class Sector
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public required string Nombre { get; set; }

        // FK a Sucursal (sector pertenece a una sola sucursal)
        [Required]
        [ForeignKey("Sucursal")]
        public int SucursalId { get; set; }

        [JsonIgnore]
        public Sucursal? Sucursal { get; set; }

        // Relación con Servicios (muchos a muchos)
        public List<Servicio> Servicios { get; set; } = new();

        public void EsValido()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || Nombre.Length < 3)
                throw new Exception("El nombre del sector debe tener al menos 3 caracteres.");
        }
    }
}
