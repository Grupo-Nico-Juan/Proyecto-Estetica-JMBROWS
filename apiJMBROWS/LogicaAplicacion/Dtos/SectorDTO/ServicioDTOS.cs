using LogicaAplicacion.Dtos.ExtraServicioDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.SectorDTO
{
    public class ServicioDTOS
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public int DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
        public List<ServiciosExtrasDTO> Extras { get; set; } = new();
    }
}
