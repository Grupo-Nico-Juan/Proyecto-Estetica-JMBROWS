using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.SectorDTO
{
    public class SectorDTOSuc
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int SucursalId { get; internal set; }
        public List<ServicioDTOS> Servicios { get; set; } = [];

    }
}
