using LogicaNegocio.Entidades.Enums;
using LogicaNegocio.Excepciones;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LogicaNegocio.Entidades
{
    public class Empleado : Usuario
    {

        [Required]
        public required string Cargo { get; set; }
        public required string Color { get; set; } = "#000000"; // Color por defecto

        public override string Rol => "Empleado";
        // Sucursal principal del empleado
        public int? SucursalId { get; set; }
        [JsonIgnore]
        public Sucursal? Sucursal { get; set; }

        // Relación con Habilidades (muchos a muchos)
        public List<Habilidad> Habilidades { get; set; } = [];

        // Relación con Turnos (uno a muchos)
        public List<Turno> TurnosAsignados { get; set; } = new();

        // Relación con Sectores (muchos a muchos, si aplica)
        public List<Sector> SectoresAsignados { get; set; } = new();

        // Relación con Periodos laborales
        public List<PeriodoLaboral> PeriodosLaborales { get; set; } = new();

        public void EsValidoEmpleado()
        {
            if (string.IsNullOrWhiteSpace(Cargo))
                throw new EmpleadoException("El cargo no puede estar vacío.");

        }
        public bool EstaDisponible(DateTime inicio, DateTime fin)
        {
            // 1. Verificar solapamiento con otros turnos asignados (que no estén cancelados)
            foreach (var turno in TurnosAsignados.Where(t => t.Estado != EstadoTurno.Cancelado))
            {
                var turnoInicio = turno.FechaHora;
                var turnoFin = turno.FechaHora.AddMinutes(turno.DuracionTotal());
                if (inicio < turnoFin && fin > turnoInicio)
                    return false;
            }

            // 2. Verificar que el turno esté dentro de algún horario habitual
            var horarios = PeriodosLaborales
                .Where(p => p.Tipo == TipoPeriodoLaboral.HorarioHabitual)
                .ToList();

            bool enHorario = horarios.Any(h =>
                h.DiaSemana == inicio.DayOfWeek &&
                h.HoraInicio <= inicio.TimeOfDay &&
                h.HoraFin >= fin.TimeOfDay);

            if (!enHorario)
                return false;

            // 3. Verificar que no se solape con ninguna licencia
            foreach (var periodo in PeriodosLaborales.Where(p => p.Tipo == TipoPeriodoLaboral.Licencia))
            {
                if (periodo.SeSuperpone(inicio, fin))
                    return false;
            }

            return true;
        }

    }
}


