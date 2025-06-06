using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.SectorDTO
{
    public class SectorCrearDTO
    {
        public required string Nombre { get; set; }
        public int SucursalId { get; set; }
    }

}
