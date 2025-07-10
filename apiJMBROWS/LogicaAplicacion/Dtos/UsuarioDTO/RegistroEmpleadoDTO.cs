using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.DtoUsuario
{
    public class RegistroEmpleadoDTO
    {
        public required string TipoUsuario { get; set; } // "Empleado"
        [Required]
        public string Cargo { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public string? Color { get; set; } 
        public int? SucursalId { get; set; }                  // Sucursal a la que pertenece
        public List<int>? HabilidadIds { get; set; }          // Habilidades asignadas
        public List<int>? TurnoIds { get; set; } = new();     // Para testing o carga inicial 
    }
}
