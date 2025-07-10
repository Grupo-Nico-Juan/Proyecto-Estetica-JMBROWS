using System;
using System.Collections.Generic;
using LogicaNegocio.Entidades.Enums;

namespace LogicaAplicacion.Dtos.TurnoDTO
{
    public class TurnoDTO
    {
        public int Id { get; set; }
        public DateTimeOffset FechaHora { get; set; }
        public int EmpleadaId { get; set; }
        public int ClienteId { get; set; }
        public int SucursalId { get; set; }
        public EstadoTurno Estado { get; set; }
        public List<DetalleTurnoDTO> Detalles { get; set; } = [];
    }
}