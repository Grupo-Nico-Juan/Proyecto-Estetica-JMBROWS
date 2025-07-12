using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.TurnoDTO
{
    public class HorarioDisponibleDTO
    {
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public List<EmpleadoTurnoDTO> EmpleadasDisponibles { get; set; } = new();
    }
}
