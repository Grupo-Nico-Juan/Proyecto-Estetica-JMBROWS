using System;
using System.Collections.Generic;

namespace LogicaAplicacion.Dtos.TurnoDTO
{
    public class TurnoDTO
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public int EmpleadaId { get; set; }
        public int ClienteId { get; set; }
        public int? SucursalId { get; set; }
        public int? SectorId { get; set; }
        public bool Realizado { get; set; }
        public List<DetalleTurnoDTO> Detalles { get; set; } = [];
    }
}