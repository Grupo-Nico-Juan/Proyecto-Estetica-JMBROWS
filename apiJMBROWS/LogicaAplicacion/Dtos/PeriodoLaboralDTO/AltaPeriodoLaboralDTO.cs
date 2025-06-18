
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
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public string? Motivo { get; set; }
    }   
}
