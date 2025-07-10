using LogicaNegocio.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.TurnoDTO
{
    public class TurnoCalendarioDTO
    {
        public int Id { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }

        public string EmpleadaNombre { get; set; }
        public string EmpleadaColor { get; set; }

        public string ClienteNombre { get; set; }
        public string ClienteApellido { get; set; }
        public string ClienteTelefono { get; set; }

        public List<string> Servicios { get; set; }
        public List<string> Extras { get; set; }

        public EstadoTurno Estado { get; set; }
    }

}
