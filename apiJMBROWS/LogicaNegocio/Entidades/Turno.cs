using Libreria.LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
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
                throw new TurnoException("No se puede reservar un turno en el pasado.");
            if (FechaHora > DateTime.Now.AddMonths(1))
                throw new TurnoException("No se puede reservar un turno con mas de un de un mes antelacion.");

            if (Detalles.Count == 0)
                throw new TurnoException("El turno debe contener al menos un servicio.");
            

            foreach (var detalle in Detalles)
                detalle.EsValido();

            // Validar que los detalles no se solapan
            var horaActual = FechaHora;
            foreach (var detalle in Detalles.OrderBy(d => d.Id)) // O el orden que corresponda
            {
                detalle.HoraInicio = horaActual;
                var extraDuracion = detalle.Extras.Sum(e => e.DuracionMinutos);
                detalle.HoraFin = horaActual.AddMinutes(detalle.Servicio.DuracionMinutos + extraDuracion);
                horaActual = detalle.HoraFin;
            }

            for (int i = 0; i < Detalles.Count - 1; i++)
            {
                if (Detalles[i].HoraFin > Detalles[i + 1].HoraInicio)
                    throw new TurnoException("Los servicios del turno se solapan en el tiempo.");
            }
        }

        public int DuracionTotal()
        {
            // Suma la duración de todos los servicios de los detalles del turno
            return Detalles.Sum(d =>
                (d.Servicio?.DuracionMinutos ?? 0) + d.Extras.Sum(e => e.DuracionMinutos));
        }

        public decimal PrecioTotal()
        {
            // Suma el precio de todos los servicios de los detalles del turno
            return Detalles.Sum(d =>
                (d.Servicio?.Precio ?? 0) + d.Extras.Sum(e => e.Precio));
        }

        public void AgregarDetalle(DetalleTurno detalle)
        {
            if (Realizado || Cancelado)
                throw new TurnoException("No se pueden modificar los servicios de un turno realizado o cancelado.");

            if (Detalles.Any(d => d.ServicioId == detalle.ServicioId))
                throw new TurnoException("El servicio ya está agregado al turno.");

            detalle.TurnoId = this.Id;
            detalle.EsValido();
            Detalles.Add(detalle);
            EsValido();
        }

        public void QuitarDetalle(int detalleId)
        {
            if (Realizado || Cancelado)
                throw new Exception("No se pueden modificar los servicios de un turno realizado o cancelado.");

            var detalle = Detalles.FirstOrDefault(d => d.Id == detalleId);
            if (detalle != null)
            {
                if (Detalles.Count == 1)
                    throw new Exception("El turno debe contener al menos un servicio.");

                Detalles.Remove(detalle);
                EsValido();
            }
        }

    }
}
