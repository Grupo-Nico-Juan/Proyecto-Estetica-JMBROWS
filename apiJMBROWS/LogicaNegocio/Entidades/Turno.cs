using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Turno
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

        [Required]
        public int EmpleadaId { get; set; }

        [JsonIgnore]
        public Empleado? Empleada { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [JsonIgnore]
        public Cliente? Cliente { get; set; }

        public List<DetalleTurno> Detalles { get; set; } = new();

        public bool Realizado { get; set; } = false;

        [JsonIgnore]
        public bool Cancelado { get; set; } = false;

        public void EsValido()
        {
            if (FechaHora < DateTime.Now)
                throw new Exception("No se puede reservar un turno en el pasado.");

            if (Detalles.Count == 0)
                throw new Exception("El turno debe contener al menos un servicio.");

            foreach (var detalle in Detalles)
                detalle.EsValido();
        }

        public int DuracionTotal() => Detalles.Sum(d => d.DuracionMinutos);

        public decimal PrecioTotal() => Detalles.Sum(d => d.Precio);
    }
}
