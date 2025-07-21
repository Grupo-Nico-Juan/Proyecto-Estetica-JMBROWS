using System.Collections.Generic;

namespace LogicaAplicacion.Dtos.PeriodoLaboralDTO
{
    public class AltaLicenciaSucursalDTO
    {
        public List<int> SucursalIds { get; set; } = new();
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public string? Motivo { get; set; }
    }
}
