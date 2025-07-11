
using LogicaNegocio.Entidades.Enums;


namespace LogicaAplicacion.Dtos.PeriodoLaboralDTO
{
    public class AltaPeriodoLaboralDTO
    {
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
