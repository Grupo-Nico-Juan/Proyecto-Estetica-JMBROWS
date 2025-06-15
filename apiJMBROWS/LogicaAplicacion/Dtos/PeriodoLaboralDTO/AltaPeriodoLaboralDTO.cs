using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.PeriodoLaboralDTO
{
    public class AltaPeriodoLaboralDTO
    {
        public int EmpleadaId { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public string? Motivo { get; set; }
        public bool EsLicencia { get; set; }
    }
}
