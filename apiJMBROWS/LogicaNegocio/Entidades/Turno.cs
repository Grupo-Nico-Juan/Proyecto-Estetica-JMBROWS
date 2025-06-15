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

            // Validar que los detalles no se solapan
            var horaActual = FechaHora;
            foreach (var detalle in Detalles.OrderBy(d => d.Id)) // O el orden que corresponda
            {
                detalle.HoraInicio = horaActual;
                detalle.HoraFin = horaActual.AddMinutes(detalle.DuracionMinutos);
                horaActual = detalle.HoraFin;
            }

            for (int i = 0; i < Detalles.Count - 1; i++)
            {
                if (Detalles[i].HoraFin > Detalles[i + 1].HoraInicio)
                    throw new Exception("Los servicios del turno se solapan en el tiempo.");
            }
        }

        public int DuracionTotal() => Detalles.Sum(d => d.DuracionMinutos);

        public decimal PrecioTotal() => Detalles.Sum(d => d.Precio);

        public void AgregarDetalle(DetalleTurno detalle)
        {
            detalle.EsValido();
            Detalles.Add(detalle);
            EsValido();
        }

        public void QuitarDetalle(int detalleId)
        {
            var detalle = Detalles.FirstOrDefault(d => d.Id == detalleId);
            if (detalle != null)
            {
                Detalles.Remove(detalle);
                EsValido();
            }
        }

        public void ModificarDetalle(DetalleTurno detalleActualizado)
        {
            var detalle = Detalles.FirstOrDefault(d => d.Id == detalleActualizado.Id);
            if (detalle == null)
                throw new Exception("Detalle no encontrado");

            detalle.ServicioId = detalleActualizado.ServicioId;
            detalle.DuracionMinutos = detalleActualizado.DuracionMinutos;
            detalle.Precio = detalleActualizado.Precio;
            detalle.EsValido();
            EsValido();
        }
    }
}
