using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Servicio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public required string Nombre { get; set; }

        [Required]
        [MinLength(5)]
        public required string Descripcion { get; set; }

        [Required]
        [Range(5, 240)]
        public int DuracionMinutos { get; set; }

        [Required]
        [Range(0.0, 10000.0)]
        public decimal Precio { get; set; }

        // Relaciones

        public List<Sector> Sectores { get; set; } = new();
        public List<Habilidad> Habilidades { get; set; } = new();

        [JsonIgnore]
        public bool Eliminado { get; set; } = false;

        public void EsValido()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || Nombre.Length < 3)
                throw new Exception("El nombre del servicio debe tener al menos 3 caracteres.");

            if (DuracionMinutos < 5 || DuracionMinutos > 240)
                throw new Exception("La duración del servicio debe estar entre 5 y 240 minutos.");

            if (Precio < 0)
                throw new Exception("El precio no puede ser negativo.");
        }
    }
}
