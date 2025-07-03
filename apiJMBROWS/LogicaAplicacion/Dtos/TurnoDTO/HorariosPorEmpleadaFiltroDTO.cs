using System;
using System.Collections.Generic;

namespace LogicaAplicacion.Dtos.TurnoDTO
{
    public class HorariosPorEmpleadaFiltroDTO
    {
        public int EmpleadaId { get; set; }
        public int SucursalId { get; set; }
        public DateTime Fecha { get; set; }
        public List<int> ServicioIds { get; set; } = new();
        public List<int>? ExtraIds { get; set; }
    }
}
