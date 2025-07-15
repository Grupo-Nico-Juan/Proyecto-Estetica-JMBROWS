using System;
using System.Collections.Generic;
using LogicaNegocio.Entidades.Enums;

namespace LogicaAplicacion.Dtos.TurnoDTO
{
    public class ActualizarTurnoDTO
    {
        public int Id { get; set; }
        public DateTime? FechaHora { get; set; }       // ← nullable
        public int? EmpleadaId { get; set; }
        public int? ClienteId { get; set; }
        public EstadoTurno? Estado { get; set; }
        public int? SucursalId { get; set; }
        public List<DetalleTurnoDTO>? Detalles { get; set; }
    }
}