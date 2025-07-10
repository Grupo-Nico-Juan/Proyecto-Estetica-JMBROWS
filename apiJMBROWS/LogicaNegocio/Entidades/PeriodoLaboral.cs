using LogicaNegocio.Entidades.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LogicaNegocio.Entidades
{
    public class PeriodoLaboral
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmpleadaId { get; set; }

        [JsonIgnore]
        public Empleado? Empleada { get; set; }

        // Para ambos tipos
        public TipoPeriodoLaboral Tipo { get; set; }

        // Para HorarioHabitual
        public DayOfWeek? DiaSemana { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }

        // Para Licencia
        public DateTimeOffset? Desde { get; set; }
        public DateTimeOffset? Hasta { get; set; }
        public string? Motivo { get; set; }

        public void EsValido()
        {
            if (Tipo == TipoPeriodoLaboral.HorarioHabitual)
            {
                if (DiaSemana == null || HoraInicio == null || HoraFin == null)
                    throw new Exception("El horario habitual debe tener día y horas definidas.");
                if (HoraInicio >= HoraFin)
                    throw new Exception("La hora de inicio debe ser menor a la de fin.");
            }
            else if (Tipo == TipoPeriodoLaboral.Licencia)
            {
                if (Desde == null || Hasta == null)
                    throw new Exception("La licencia debe tener fechas definidas.");
                if (Desde >= Hasta)
                    throw new Exception("El periodo debe tener una duración válida.");
                if (Motivo != null && Motivo.Length > 100)
                    throw new Exception("El motivo es demasiado largo.");
            }
        }

        public bool SeSuperpone(DateTimeOffset inicio, DateTimeOffset fin)
        {
            if (Tipo == TipoPeriodoLaboral.Licencia && Desde != null && Hasta != null)
            {
                return !(fin <= Desde || inicio >= Hasta);
            }
            else if (Tipo == TipoPeriodoLaboral.HorarioHabitual && DiaSemana != null && HoraInicio != null && HoraFin != null)
            {
                // Valida si el turno está dentro del horario habitual
                var diaTurno = inicio.DayOfWeek;
                var horaInicioTurno = inicio.TimeOfDay;
                var horaFinTurno = fin.TimeOfDay;
                return diaTurno == DiaSemana &&
                       horaInicioTurno >= HoraInicio &&
                       horaFinTurno <= HoraFin;
            }
            return false;
        }
    }
}