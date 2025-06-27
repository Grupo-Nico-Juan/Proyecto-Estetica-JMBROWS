using System.Collections.Generic;

namespace LogicaAplicacion.Dtos.TurnoDTO
{
    public class AltaDetalleTurnoDTO
    {
        public int TurnoId { get; set; }
        public int ServicioId { get; set; }
        public List<int>? ExtrasIds { get; set; }
    }
}