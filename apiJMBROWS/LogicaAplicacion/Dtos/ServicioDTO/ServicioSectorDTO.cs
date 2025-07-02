using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.ServicioDTO
{
    public class ServicioSectorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int SucursalId { get; set; }
    }
}
