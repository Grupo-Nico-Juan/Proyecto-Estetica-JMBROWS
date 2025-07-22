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
        public int SucursalId { get; set; }
        public required string SucursalNombre { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }

        public required string EmpleadaNombre { get; set; }
        public required string EmpleadaColor { get; set; }
        public int EmpleadaId { get; set; }

        public required string ClienteNombre { get; set; }
        public required string ClienteApellido { get; set; }
        public required string ClienteTelefono { get; set; }

        public required List<string> Servicios { get; set; }
        public required List<string> Extras { get; set; }

        public EstadoTurno Estado { get; set; }
    }

}
