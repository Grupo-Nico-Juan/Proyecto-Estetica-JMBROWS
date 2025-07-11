using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Promocion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public required string Nombre { get; set; }

        [Required]
        public DateTimeOffset FechaInicio { get; set; }

        [Required]
        public DateTimeOffset FechaFin { get; set; }

        [Required]
        [Range(1, 100)]
        public int PorcentajeDescuento { get; set; }

        public List<Servicio> ServiciosIncluidos { get; set; } = new();

        public string? Descripcion { get; set; }

        public bool Activa { get; set; } = true;

        [JsonIgnore]
        public bool Eliminada { get; set; } = false;

        public void EsValido()
        {
            if (FechaInicio >= FechaFin)
                throw new Exception("La fecha de inicio debe ser anterior a la fecha de fin.");

            if (PorcentajeDescuento <= 0 || PorcentajeDescuento > 100)
                throw new Exception("El descuento debe ser entre 1% y 100%.");

            if (string.IsNullOrWhiteSpace(Nombre))
                throw new Exception("Debe especificarse un nombre para la promoción.");
        }

        public bool EstaVigente()
        {
            var hoy = DateTimeOffset.UtcNow;
            return Activa && hoy >= FechaInicio && hoy <= FechaFin;
        }
    }
}
