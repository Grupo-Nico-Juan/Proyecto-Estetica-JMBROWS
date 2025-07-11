using System;
using System.Collections.Generic;

namespace LogicaAplicacion.Dtos.TurnoDTO
{
    public class AltaTurnoDTO
    {
        public DateTimeOffset FechaHora { get; set; }
        public int EmpleadaId { get; set; }
        public int ClienteId { get; set; }
        public int SucursalId { get; set; }
        public List<AltaDetalleTurnoDTO> Detalles { get; set; } = [];
    }
}