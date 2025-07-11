using LogicaNegocio.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.TurnoDTO
{
    public class TurnoFiltroDTO
    {
        public int? EmpleadaId { get; set; }
        public EstadoTurno? Estado { get; set; }
        public DateTimeOffset? FechaInicio { get; set; }
        public DateTimeOffset? FechaFin { get; set; }
    }
}
