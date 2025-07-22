using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.ExtraServicioDTO
{
    public class ServiciosExtrasDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public int DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
        public int ServicioId { get; set; }
    }
}
