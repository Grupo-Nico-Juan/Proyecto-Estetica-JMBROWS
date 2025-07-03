using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.TurnoDTO
{
    public class HorariosDisponiblesFiltroDTO
    {
        public int SucursalId { get; set; }
        public DateTime Fecha { get; set; }
        public List<int> ServicioIds { get; set; } = new();
        public List<int>? ExtraIds { get; set; }
    }
}
