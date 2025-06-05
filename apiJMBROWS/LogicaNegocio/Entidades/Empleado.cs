using LogicaNegocio.Excepciones;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LogicaNegocio.Entidades
{
    public class Empleado : Usuario
    {

        [Required]
        public required string Cargo { get; set; }

        public override string Rol => "Empleado";

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
    }
}


