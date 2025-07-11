using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.EmpleadoDTO.EmpleadoDispibleDTO
{
    public class ConsultaEmpleadasDisponiblesDTO
    {
        public DateTimeOffset FechaHoraInicio { get; set; }
        public List<int> ServiciosSeleccionados { get; set; } = [];
    }

}
