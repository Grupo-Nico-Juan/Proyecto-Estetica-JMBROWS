using System;
using System.Collections.Generic;

namespace LogicaAplicacion.Dtos.TurnoDTO
{
    public class ActualizarTurnoDTO
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public int EmpleadaId { get; set; }
        public int ClienteId { get; set; }
        public bool Realizado { get; set; }
        public int? SucursalId { get; set; }
        public int? SectorId { get; set; }
        public List<ActualizarDetalleTurnoDTO> Detalles { get; set; } = [];
    }
}