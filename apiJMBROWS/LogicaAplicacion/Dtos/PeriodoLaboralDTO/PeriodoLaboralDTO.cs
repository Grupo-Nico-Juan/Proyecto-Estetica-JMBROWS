using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.PeriodoLaboralDTO
{
    public class PeriodoLaboralDTO
    {
        public int? Id { get; set; }
        public int EmpleadaId { get; set; }
        public TipoPeriodoLaboral Tipo { get; set; }
        public DayOfWeek? DiaSemana { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public DateTimeOffset? Desde { get; set; }
        public DateTimeOffset? Hasta { get; set; }
        public string? Motivo { get; set; }
    }
}
